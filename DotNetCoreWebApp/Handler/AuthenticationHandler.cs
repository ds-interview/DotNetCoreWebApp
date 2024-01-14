using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Handler
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationOptions>
    {
        public AuthenticationHandler(IOptionsMonitor<AuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
        { }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //check header first
            if (!Request.Headers
                .ContainsKey(Options.TokenHeaderName))
            {
                return AuthenticateResult.Fail($"Missing header: {Options.TokenHeaderName}");
            }

            //get the header and validate
            string token = Request
                .Headers[Options.TokenHeaderName]!;

            //usually, this is where you decrypt a token and/or lookup a database.
            if (token == null)
            {
                return AuthenticateResult
                    .Fail($"Invalid token.");
            }
            //Success! Add details here that identifies the user
            var claims = new List<Claim>()
        {
            new Claim("FirstName", "Juan")
        };

            var claimsIdentity = new ClaimsIdentity
                (claims, this.Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal
                (claimsIdentity);

            return AuthenticateResult.Success
                (new AuthenticationTicket(claimsPrincipal,
                this.Scheme.Name));
        }
    }
}
