using InsuranceWebAPI.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using InsuranceWebAPI.DataLayer;
using log4net;

namespace InsuranceWebAPI.BusinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// User service constructor
        /// </summary>
        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreateUser(UserDTO userEntity)
        {
            try
            {
                Log.Error("register ");
                var salt = Common.GenerateSalt(5);
                var user = new User
                {
                    Salt = salt,
                    Password = Common.GetHashPassword(userEntity.Password, salt),
                    UserName = userEntity.UserName
                };
                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();
                return user.UserID;
            }
            catch (Exception ex)
            {
                Log.Error("register Error : " + ex);
                return 0;
            }
           
        }

        public bool FindUser(string UserName, string Password)
        {
            var user = _unitOfWork.UserRepository.GetByCondition(u => u.UserName == UserName);
            if (user != null)
            {
                if (user.Password == Common.GetHashPassword(Password, user.Salt))
                {
                    return true;
                }
            }
            return false;
        }
    }
}