using api.multitracks.com.Interfaces;
using System.Runtime.CompilerServices;

namespace api.multitracks.com.Models
{
    public class ServiceResponse<T> : IServiceResponse
    {
        public T Data { get; set; }

        public ServiceError Error { get; set; }

        public string Message { get; set; }

        public bool Status => Error == null;

        public void AddError(object p)
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<T> AddError(Exception ex)
        {
            Error = new ServiceError(ex);
            return this;
        }

        public ServiceResponse<T> AddError(string errorMessage)
        {
            Error = new ServiceError(errorMessage);
            return this;
        }

        public ServiceResponse<T> AddError(string errorCode, string errorMessage)
        {
            Error = new ServiceError(errorCode, errorMessage);
            return this;
        }

        public ServiceResponse<T> AddError(ServiceError serviceError)
        {
            Error = serviceError;
            return this;
        }

        public ServiceResponse<T> Attach(ServiceResponse sr)
        {
            if (Error == null && sr.Error != null)
            {
                Error = sr.Error;
            }

            return this;
        }

        public ServiceResponse<T> Attach(object sr)
        {
            if (Error == null)
            {
                Error = ServiceError.Copy(sr);
            }

            return this;
        }

        public override string ToString()
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(23, 3);
            defaultInterpolatedStringHandler.AppendLiteral("Status: ");
            defaultInterpolatedStringHandler.AppendFormatted(Status);
            defaultInterpolatedStringHandler.AppendLiteral("\nData: ");
            T data = Data;
            defaultInterpolatedStringHandler.AppendFormatted((data != null) ? data.ToString() : null);
            defaultInterpolatedStringHandler.AppendLiteral("\nError: ");
            defaultInterpolatedStringHandler.AppendFormatted(Error?.ToString());
            return defaultInterpolatedStringHandler.ToStringAndClear();
        }
    }
}
