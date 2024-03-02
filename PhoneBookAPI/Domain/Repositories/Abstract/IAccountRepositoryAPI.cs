using PhoneBookAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookAPI.Domain.Repositories.Abstract
{
    public interface IAccountRepositoryAPI
    {
        Task<bool> CheckUserToDB(LoginModel model);

        Task<bool> CreateUser(RegisterModel model);
    }
}
