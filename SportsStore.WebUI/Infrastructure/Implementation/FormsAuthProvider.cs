using SportsStore.WebUI.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Infrastructure.Implementation
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}