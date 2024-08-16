using DapprD.Repository.TransactionRepository.Interfaces;
using DapprD.Repository.UserRepository.Interfaces;
using System.Data;

namespace DapprD.Repository.UnitOfWork
{
    public class UnitofWork : IUnitofWork, IDisposable
    {
        public IUserRepository UserRepository { get; }
        public ITransferRepository TransferRepository { get; }

        IDbTransaction _dbTransaction;

        public UnitofWork(IDbTransaction dbTransaction, IUserRepository userRepository, ITransferRepository transferRepository)
        {
            TransferRepository = transferRepository;
            UserRepository = userRepository;
            _dbTransaction = dbTransaction;
        }

        void IDisposable.Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
        void IUnitofWork.Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }
    }
}
