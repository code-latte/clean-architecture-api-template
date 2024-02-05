using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api
{
    public class BaseController : ControllerBase
    {
        protected string AccountId
        {
            get
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    try
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        if (claims != null && claims.Where(x => x.Type == "Id").Any())
                            return claims.Where(x => x.Type == "Id").FirstOrDefault().Value;
                    }
                    catch
                    {
                        return null;
                    }
                }

                return null;
            }
        }
    }
}
