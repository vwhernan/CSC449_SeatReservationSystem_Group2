namespace CSC449_SeatReservationSystem.Interfaces
{
    public interface IRepository<TEntity, TModel> 
        where TEntity: class
        where TModel : class
    {
        Task<bool> CreateAsync(TModel form);
        Task<TEntity> GetByIdAsync(int id);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<bool> UpdateAsync(TModel form);
        Task<bool> DeleteAsync(int id);
    }
}
