using DapprD.Repository.TransactionRepository.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using DapprD.Models;

namespace DapprD.Repository.TransactionRepository
{
    public class TransferRepository : ITransferRepository
    {
        private SqlConnection _sqlConnection;
        private IDbTransaction _dbTransaction;

        public TransferRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction;
            _sqlConnection = sqlConnection;
        }

        public bool AddNew(Transfer transfer)
        {
            var sql = "INSERT INTO Transfers(Id, SourceUserId, TargetUserId, Amount, Timestamp) VALUES (@Id, @SourceUserId, @TargetUserId, @Amount, @Timestamp)";
            return _sqlConnection.Execute(sql, transfer, transaction: _dbTransaction) > 0;
        }
    }
}
