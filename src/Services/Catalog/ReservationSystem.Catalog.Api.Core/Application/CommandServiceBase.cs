using FluentValidation;
using ReservationSystem.Catalog.Core.Application.Services;

namespace ReservationSystem.Catalog.Core.Application
{
    public class CommandServiceBase
    {
        private readonly IValidatorFactory _validationFactory;

        public CommandServiceBase(IValidatorFactory validatorFactory)
        {
            _validationFactory = validatorFactory;
        }

        //It can be implemented as an aspect
        protected void ValidateCommand(CommandBase command)
        {
            var validator = _validationFactory.GetValidator(command.GetType());
            var result = validator?.Validate(command);
            if (result != null && !result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
