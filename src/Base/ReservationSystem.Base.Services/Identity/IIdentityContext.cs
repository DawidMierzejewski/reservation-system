using System;

namespace ReservationSystem.Base.Services.Identity
{
    public interface IIdentityContext
    {
        Guid UserId { get; }
        UserRoles Roles { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
    }
}
