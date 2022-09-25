using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EmployeeCRUD.Core.DTOs.Common
{


    public class ServiceResponse
    {
        public ServiceResponse(ServiceResponseCode responseCode, string responseMessage = null)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }

        public ServiceResponse(IList<ValidationError> validationErrors, string responseMessage = null)
        {
            ResponseCode = ServiceResponseCode.ValidationErrors;
            ResponseMessage = responseMessage;
            ValidationErrors = validationErrors;
        }
        public ServiceResponse(ModelStateDictionary modelState, string responseMessage = null)
        {
            if (!modelState.IsValid)
                this.ResponseCode = ServiceResponseCode.ValidationErrors;

            ResponseMessage = responseMessage;
            ValidationErrors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
        public ServiceResponse(Exception exception, string responseMessage = null)
        {
            ResponseCode = ServiceResponseCode.Failed;
            ResponseMessage = responseMessage ?? "Some thing went wrong";
            Exception = exception.ToString();
        }

        public ServiceResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public IList<ValidationError> ValidationErrors { get; set; }
        public string ResponseCodeText => this.ResponseCode.ToString();
        public bool IsSuccess => ResponseCode == ServiceResponseCode.Success;
        public bool HasValidationErrors => ValidationErrors != null && ValidationErrors.Count > 0;

        public string Exception { get; set; }
        public object ResponseData { get; set; }
    }
    public class ServiceResponse<TResponse> : ServiceResponse
    {

        public ServiceResponse(ServiceResponseCode responseCode, string responseMessage = null) : base(responseCode, responseMessage)
        { }

        public ServiceResponse(IList<ValidationError> validationErrors, string responseMessage = null) : base(validationErrors, responseMessage)
        { }

        public ServiceResponse(ModelStateDictionary modelState, string responseMessage = null) : base(modelState, responseMessage)
        { }
        public ServiceResponse(ServiceResponseCode responseCode, TResponse response, string responseMessage = null) : base(responseCode, responseMessage)
        {
            ResponseData = response;
        }
        public new TResponse ResponseData { get; set; }
    }
}
