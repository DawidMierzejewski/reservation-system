using System;

namespace ReservationSystem.Base.Services.Identity
{
    [Flags]
    public enum UserRoles
    {
        Customer = 1,
        Admin = 2
    }
}
