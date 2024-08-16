using DapprD.Repository.TransactionRepository.Interfaces;
using DapprD.Repository.UserRepository.Interfaces;

namespace DapprD.Repository.UnitOfWork
{
    public interface IUnitofWork
    {
        //ITransferRepository TransferRepository { get; }
        IUserRepository UserRepository { get; }
        ITransferRepository TransferRepository { get; }

        void Commit();
    }
}
