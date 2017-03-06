using InsuranceWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceWebAPI.BusinessLayer.Interface
{
    public interface IUserService
    {
        int CreateUser(UserDTO userEntity);
        bool FindUser(string UserName, string Password);
    }
}
