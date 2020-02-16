using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.Data;
using Entity.Entity;
using Entity.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Service.Interface;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Task<UserVM> GetUser(LoginVM loginVM)
        {
            if (loginVM.Account == "admin" && loginVM.Password == "admin")
            {
                return Task.FromResult(new UserVM() { UserId = Guid.NewGuid(), Account = "admin", Name = "admin" });
            }
            return Task.FromResult(default(UserVM));
        }
    }
}
