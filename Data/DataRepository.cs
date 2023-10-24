namespace TechnicalTestMasiv;
using Microsoft.EntityFrameworkCore;

public class MyDataRepository : IDataRepository
{
    private readonly ApplicationDbContext _context;

    public MyDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Data> AddAsync(Data entity)
    {
        try
        {
            _context.MyDataItems.Add(entity);
            Console.Write(_context.MyDataItems);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar el registro: {ex.Message}");
            
            // Lanza la excepción nuevamente si deseas propagarla para un manejo superior.
            throw;
        }
    }

    public async Task<int> GetCurrentFloorAsync()
    {
        var state = await _context.ElevatorStates.FindAsync(1);
        return state?.CurrentFloor ?? 1; // Devuelve 1 como valor predeterminado si no hay datos.
    }

    public async Task UpdateCurrentFloorAsync(int floor)
    {
        var state = await _context.ElevatorStates.FindAsync(1);
        if (state != null)
        {
            state.CurrentFloor = floor;
            await _context.SaveChangesAsync();
        }
        else
        {
            // Lanza una excepción o maneja este caso según lo veas necesario.
            throw new Exception("El registro ElevatorState no fue encontrado.");
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var entity = await _context.MyDataItems.FindAsync(id);

            if (entity == null)
            {
                return false; // El registro no se encontró, puede manejar este caso según tus necesidades.
            }

            _context.MyDataItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true; // Indicar que la eliminación fue exitosa.
        }
        catch (Exception ex)
        {
            // Aquí puedes manejar la excepción de acuerdo a tus necesidades.
            // Por ejemplo, puedes registrar el error en la consola:
            Console.WriteLine($"Error al eliminar el registro: {ex.Message}");
            
            // Lanza la excepción nuevamente si deseas propagarla para un manejo superior.
            throw;
        }
    }


    public async Task<Data> GetAsync(int id)
    {
        return await _context.MyDataItems.FindAsync(id);
    }

    public async Task<IEnumerable<Data>> GetAllAsync()
    {
        return await _context.MyDataItems.ToListAsync();
    }

    public async Task<string> GetElevatorStatusAsync()
    {
        var state = await _context.ElevatorStates.FindAsync(1);
        return state?.ElevatorStatus ?? "Stopped";
    }
    
    public async Task SetElevatorStatusAsync(string status)
    {
        var state = await _context.ElevatorStates.FindAsync(1);
        if (state != null)
        {
            state.ElevatorStatus = status;
            await _context.SaveChangesAsync();
        }
        else
        {
            // Lanza una excepción o maneja este caso según lo veas necesario.
            throw new Exception("El registro ElevatorState no fue encontrado.");
        }
    }
    
    public async Task<Data> UpdateAsync(Data entity)
    {
        try
        {
            var existingEntity = await _context.MyDataItems.FindAsync(entity.Id);

            if (existingEntity == null)
            {
                return null; // El registro no se encontró, puede manejar este caso según tus necesidades.
            }

            // Actualizar las propiedades de la entidad existente con los valores de la entidad proporcionada.
            existingEntity.Id = entity.Id;
            existingEntity.Floor = entity.Floor;

            // Guardar los cambios en la base de datos.
            await _context.SaveChangesAsync();
            
            return existingEntity; // Devolver la entidad actualizada.
        }
        catch (Exception ex)
        {
            // Aquí puedes manejar la excepción de acuerdo a tus necesidades.
            // Por ejemplo, puedes registrar el error en la consola:
            Console.WriteLine($"Error al actualizar el registro: {ex.Message}");
                
            // Lanza la excepción nuevamente si deseas propagarla para un manejo superior.
            throw;
        }
    }

}
