using DapprD.Models;
using DapprD.Repository.UserRepository.Interfaces;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DapprD.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private SqlConnection _sqlConnection;
        private IDbTransaction _dbTransaction;

        public UserRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) { 
            _sqlConnection = sqlConnection;
            _dbTransaction = dbTransaction;
        }

        public bool Credit(string userId, double amount)
        {
            var sql = "UPDATE Users SET CurrentBalance = CurrentBalance + @amount WHERE Id = @userId";
            return _sqlConnection.Execute(sql, new { userId = userId, amount = amount }, transaction: _dbTransaction) > 0;
        }

        public bool Debit(string userId, double amount)
        {
            var sql = "UPDATE Users SET CurrentBalance = CurrentBalance - @amount WHERE Id = @userId";
            return _sqlConnection.Execute(sql, new { userId = userId, amount = amount }, transaction: _dbTransaction) > 0;
        }

        User IUserRepository.GetUserDetails(string userId)
        {
            var sql = "SELECT * FROM Users WHERE Id=@userId";
            return _sqlConnection.QueryFirst<User>(sql, new { userId = userId }, transaction: _dbTransaction);            
        }
    }
}
