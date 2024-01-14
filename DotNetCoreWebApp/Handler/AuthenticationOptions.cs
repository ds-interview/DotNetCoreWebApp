using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Handler
{
    public class AuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "MyAuthenticationScheme";
        public string TokenHeaderName { get; set; } = "token";
    }
}
