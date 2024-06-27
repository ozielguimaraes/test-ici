using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace TesteICI.Infra.CrossCutting.Security.Shared
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
