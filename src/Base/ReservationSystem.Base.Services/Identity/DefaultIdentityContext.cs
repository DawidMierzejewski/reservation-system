using System;

namespace ReservationSystem.Base.Services.Identity
{
    public class DefaultIdentityContext : IIdentityContext
    {
        public Guid UserId { get; }
        public UserRoles Roles { get; }
        public bool IsAuthenticated { get; }
        public bool IsAdmin => Roles.HasFlag(UserRoles.Admin);

        public DefaultIdentityContext()
        {
            UserId = Guid.NewGuid();
            Roles = UserRoles.Customer | UserRoles.Admin;
            IsAuthenticated = true;
        }
    }
}
