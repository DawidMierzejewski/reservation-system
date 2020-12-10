using ReservationSystem.Base.Services.MediatR.Behaviors.Attributes;

namespace ReservationSystem.Base.Services.CQRS
{
    [TransactionalRequest]
    [ValidationRequest]
    public class CommandBase
    {
    }
}
