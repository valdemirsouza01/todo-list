using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Cleverti.Todo.API.Auth
{


    /// <summary>
    /// Authentication handler which handles BasicAuthentication header for API methods
    /// </summary>
    /// <seealso cref="AuthenticationHandler{AuthenticationSchemeOptions}" />
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string MissingAuthorizationHeaderError = "Missing Authorization Header";
        private const string InvalidAuthorizationHeaderError = "Invalid Authorization Header";
        private const string InternalAuthorizationError = "An internal authorization error occured. Please contact administrator.";
        private const string InvalidUserNameOrPassword = "Invalid credentials";


        private readonly IConfiguration _configuration;
        private const string CREDENTIALS = "credentialApi";
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
                return AuthenticateResult.Fail(MissingAuthorizationHeaderError);

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[HeaderNames.Authorization]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentialsDecode = Encoding.UTF8.GetString(credentialBytes);

                var credentials = _configuration.GetValue<string>(CREDENTIALS);

                if (credentials == credentialsDecode)
                {


                    var claims = new[] {
                        new Claim(ClaimTypes.NameIdentifier,"01"),
                        new Claim(ClaimTypes.Name, "ClerverTI"),
                    };

                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);

                }
                else
                {

                    return AuthenticateResult.Fail(InvalidUserNameOrPassword);
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "An exception occured when reading authorization header");
                return AuthenticateResult.Fail(InvalidAuthorizationHeaderError);
            }


        }
    }

}
