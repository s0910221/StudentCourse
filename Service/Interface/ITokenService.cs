using System;
using System.Collections.Generic;
using System.Text;
using Entity.ViewModel;

namespace Service.Interface
{
    public interface ITokenService
    {
        UserTokenVM BuildUserToken(UserVM userVM);
    }
}
