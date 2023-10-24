public class ElevatorState
{
    public int Id { get; set; } // Esto puede ser siempre 1, ya que solo necesitas una fila.
    public int CurrentFloor { get; set; }
    public string ElevatorStatus { get; set; }
}