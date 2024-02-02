namespace R54_M8_Class_08_Work_01.Repositories.Intefaces
{
    public interface IUnitOfWork
    {
        IGenericRepo<T> GetRepo<T>() where T : class, new();
        Task<bool> SaveAsync();
    }
}
