using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.ViewModel
{
    public class UserTokenVM
    {
        public string Token { get; set; }
        public long ExpireIn { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
