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
                "Password=xx;Persist Security Info=True;User ID=xx;Initial Catalog=Dapper;Data Source=xxxxxxxx;"));
            services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = s.GetRequiredService<SqlConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });
        }
    }
}
