using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Entity.ViewModel
{
    public class UserVM
    {
        public Guid UserId { get; set; }
        public string Account { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
