using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TesteICI.Infra.CrossCutting.Security.Extensions;
using TesteICI.Infra.CrossCutting.Security.Shared;

namespace TesteICI.Infra.CrossCutting.Security
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Nome { get; set; }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            if (IsAuthenticated)
            {
                _ = Guid.TryParse(_accessor.HttpContext?.User.GetUserId(), out Guid userId);
                return userId;
            }
            return Guid.Empty;
        }

        public string GetUserEmail() => IsAuthenticated ? _accessor.HttpContext.User.GetUserEmail() : string.Empty;
        public bool IsAuthenticated => _accessor.HttpContext.User.Identity.IsAuthenticated;
        public bool IsInRole(string role) => _accessor.HttpContext.User.IsInRole(role);
        public IEnumerable<Claim> GetClaimsIdentity() => _accessor.HttpContext.User.Claims;
    }
}
