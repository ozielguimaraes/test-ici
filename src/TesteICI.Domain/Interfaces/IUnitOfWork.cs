namespace TesteICI.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
