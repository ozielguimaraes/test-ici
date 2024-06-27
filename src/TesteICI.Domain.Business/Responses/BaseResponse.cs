using FluentValidation.Results;

namespace TesteICI.Domain.Business.Responses
{
    public abstract class BaseResponse : BaseMessage
    {
        protected BaseResponse() { }
        protected BaseResponse(ValidationResult validationResult)
        {
            SetValidationResult(validationResult);
        }

        private ValidationResult _validationResult;

        public ValidationResult GetValidationResult()
        {
            return _validationResult;
        }

        public IEnumerable<ValidationFailure> GetValidationFailures()
        {
            return _validationResult.Errors;
        }

        public void SetValidationResult(ValidationResult value)
        {
            _validationResult = value;
        }

        public bool IsValid() => GetValidationResult()?.IsValid ?? true;
    }
}
