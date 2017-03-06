using InsuranceWebAPI.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using InsuranceWebAPI.DataLayer;
using AutoMapper;

namespace InsuranceWebAPI.BusinessLayer.Service
{
    public class UserService : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// User service constructor
        /// </summary>
        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreateUser(UserDTO userEntity)
        {
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