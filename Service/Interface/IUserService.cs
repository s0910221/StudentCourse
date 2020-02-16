using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entity.ViewModel;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<UserVM> GetUser(LoginVM loginVM);
    }
}
