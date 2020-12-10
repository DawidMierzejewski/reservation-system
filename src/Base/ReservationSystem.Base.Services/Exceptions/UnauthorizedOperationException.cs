using System;

namespace ReservationSystem.Base.Services.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException
    {
        public override string Code => "unauthorized_operation";

        public string OperationName { get; set; }
        public Guid UserId { get; set; }

        public string ObjectId { get; set; }

        public UnauthorizedOperationException(string operationName, Guid userId, string objectId) : base($"OperationName {operationName}, UserId {userId}, ObjectId {objectId}")
        {
            OperationName = operationName;
            UserId = userId;
            ObjectId = objectId;
        }

        public UnauthorizedOperationException(string operationName, Guid userId) : base($"OperationName {operationName}, UserId {userId}")
        {
            OperationName = operationName;
            UserId = userId;
        }
    }
}
