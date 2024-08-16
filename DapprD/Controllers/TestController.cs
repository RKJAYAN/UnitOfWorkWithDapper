using DapprD.Models;
using DapprD.Repository.UnitOfWork;
using DapprD.Repository.UserRepository;
using DapprD.Repository.UserRepository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DapprD.Controllers
{

    [ApiController]
    public class TestController : ControllerBase
    {
        private IUnitofWork _unitofWork;

        public TestController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [Route("api/GetUser")]
        [HttpPost]
        public User GetUser(User _user) {
             return _unitofWork.UserRepository.GetUserDetails(_user.Name);
        }

        [Route("api/Debit")]
        [HttpPost]
        public string Debit(string sourceUserId, string targetUserId, double amount)
        {
            bool flag = true;
            try {
                flag &= _unitofWork.UserRepository.Debit(sourceUserId, amount);
                flag &= _unitofWork.UserRepository.Credit(targetUserId, amount);

                Transfer transfer = new Transfer() { 
                    Id = Guid.NewGuid().ToString(),
                    Amount = amount,
                    SourceUserId = sourceUserId,
                    TargetUserId = targetUserId,
                    Timestamp = DateTime.UtcNow
                };             

                flag &= _unitofWork.TransferRepository.AddNew(transfer);


                if (flag) {

                    _unitofWork.Commit();
                } 

            }
            catch (Exception e) {

                flag = false;
            }

            return flag ? "Success" : "Failed";
            
        }

    }
}
