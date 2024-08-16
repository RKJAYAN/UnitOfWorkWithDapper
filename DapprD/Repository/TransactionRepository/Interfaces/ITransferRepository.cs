using DapprD.Models;

namespace DapprD.Repository.TransactionRepository.Interfaces
{
    public interface ITransferRepository
    {
        bool AddNew(Transfer transfer);
    }
}
