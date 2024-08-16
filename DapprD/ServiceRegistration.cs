using DapprD.Repository.UserRepository.Interfaces;
using DapprD.Repository.UserRepository;
using System.Data.SqlClient;
using System.Data;
using DapprD.Repository.TransactionRepository.Interfaces;
using DapprD.Repository.TransactionRepository;
using DapprD.Repository.UnitOfWork;

namespace DapprD
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddScoped((s) => new SqlConnection(
                "Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=Dapper;Data Source=10.66.201.22;"));
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });
        }
    }
}
