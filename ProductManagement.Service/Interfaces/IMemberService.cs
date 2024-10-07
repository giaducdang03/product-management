using ProductManagement.Service.BussinessModels.AuthenModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.Interfaces
{
    public interface IMemberService
    {
        public Task<AuthenModel> LoginWithEmailPassword(string email, string password);

        public Task<bool> RegisterAsync(SignUpModel model);
    }
}
