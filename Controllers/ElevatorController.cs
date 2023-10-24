using Microsoft.AspNetCore.Mvc;

namespace TechnicalTestMasiv.Controllers;

[ApiController]
[Route("[controller]")]
public class ElevatorController : ControllerBase
{
    private readonly IDataRepository _myDataRepository;

    public ElevatorController(IDataRepository myDataRepository)
    {
        _myDataRepository = myDataRepository;
    }

    [HttpPost("move")]
    public async Task<IActionResult> MoveElevator([FromBody] int targetFloor)
    {
        Data newData = new Data
        {
            Floor = targetFloor,
            Type = "move"
        };
        await _myDataRepository.AddAsync(newData);
        return Ok();
    }

    [HttpPost("call")]
    public async Task<IActionResult> CallElevator([FromBody] int floor)
    {
        Data newData = new Data
        {
            Floor = floor,
            Type = "call"
        };
        await _myDataRepository.AddAsync(newData);
        return Ok();
    }

    [HttpGet("state")]
    public async Task<IActionResult> GetElevatorState()
    {
        int currentFloor = await _myDataRepository.GetCurrentFloorAsync();
        var pendingFloors = (await _myDataRepository.GetAllAsync())
            .OrderByDescending(item => item.Type == "move")
            .ThenBy(item => item.Floor)
            .Select(item => item.Floor)
            .ToList();

        var state = new
        {
            CurrentFloor = currentFloor,
            PendingFloors = pendingFloors
        };
        return Ok(state);
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartElevator()
    {
        var elevatorStatus = await _myDataRepository.GetElevatorStatusAsync();
        var pendingFloors = await _myDataRepository.GetAllAsync();

        if (pendingFloors.Any() && elevatorStatus == "Moving")
        {
            // Elevator is already moving.
            return BadRequest("Elevator is already in motion.");
        }
        await _myDataRepository.SetElevatorStatusAsync("Moving");

        await MoveElevatorAsync();

        return Ok();
    }


    private async Task MoveElevatorAsync()
    {
        var pendingFloors = (await _myDataRepository.GetAllAsync())
            .OrderByDescending(item => item.Type == "move")
            .ThenBy(item => item.Floor)
            .ToList();

        int currentFloor = await _myDataRepository.GetCurrentFloorAsync();

        while (pendingFloors.Count > 0)
        {
            // Obtener el piso más cercano a la posición actual.
            var closestPendingFloor = pendingFloors
                .OrderBy(floor => Math.Abs(currentFloor - floor.Floor))
                .FirstOrDefault();

            if (closestPendingFloor == null) break;  // No hay más pisos pendientes.

            int targetFloor = closestPendingFloor.Floor;
            Guid targetId = closestPendingFloor.Id;

            while (currentFloor != targetFloor)
            {
                if (targetFloor > currentFloor)
                {
                    currentFloor++;
                }
                else
                {
                    currentFloor--;
                }

                // Simula la velocidad del elevador.
                await Task.Delay(TimeSpan.FromSeconds(1));

                // Actualiza el piso actual en la base de datos.
                await _myDataRepository.UpdateCurrentFloorAsync(currentFloor);
            }

            // Elimina el piso alcanzado de la base de datos.
            await _myDataRepository.DeleteAsync(targetId);

            // Elimina de la lista en memoria.
            pendingFloors.Remove(closestPendingFloor);
        }

        // Cuando se han visitado todos los pisos pendientes, detiene el elevador.
        await _myDataRepository.SetElevatorStatusAsync("Stopped");
    }

}
