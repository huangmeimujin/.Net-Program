using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.AuthenticationCenter.Utility
{
    public interface IJWTService
    {
        string GetToken(string userName);
    }
}
