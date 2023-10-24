namespace TechnicalTestMasiv;
public interface IDataRepository
{
    Task<Data> AddAsync(Data entity);
    Task<Data> GetAsync(int id);
    Task<Data> UpdateAsync(Data entity);
    Task<IEnumerable<Data>> GetAllAsync();
    Task<int> GetCurrentFloorAsync();
    Task UpdateCurrentFloorAsync(int floor);
    Task<bool> DeleteAsync(Guid id);
    Task<string> GetElevatorStatusAsync();
    Task SetElevatorStatusAsync(string status);
    
}
