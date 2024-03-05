using System;
using System.Runtime.CompilerServices;

namespace api.multitracks.com.Models
{
    public class ServiceError
    {
        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ErrorDetail { get; set; }

        public ServiceError()
        {
        }

        public ServiceError(Exception ex)
        {
            Exception ex2 = ex;
            if (ex.InnerException != null)
            {
                ex2 = ex.InnerException;
            }

            ErrorMessage = ex2.Message;
            ErrorDetail = ex2.ToString();
            ErrorCode = "";
        }

        public ServiceError(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorCode = "N/A";
        }

        public ServiceError(string errorCode, string errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public override string ToString()
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 3);
            defaultInterpolatedStringHandler.AppendLiteral("Error Code: ");
            defaultInterpolatedStringHandler.AppendFormatted(ErrorCode);
            defaultInterpolatedStringHandler.AppendLiteral("\nError Message: ");
            defaultInterpolatedStringHandler.AppendFormatted(ErrorMessage);
            defaultInterpolatedStringHandler.AppendLiteral("\nError Detail: ");
            defaultInterpolatedStringHandler.AppendFormatted(ErrorDetail);
            return defaultInterpolatedStringHandler.ToStringAndClear();
        }

        public static ServiceError Copy(object sr)
        {
            if (sr == null)
            {
                return null;
            }

            object value = sr.GetType().GetProperty("Error")!.GetValue(sr);
            if (value == null)
            {
                return null;
            }

            string errorCode = value.GetType().GetProperty("ErrorCode")!.GetValue(value)?.ToString();
            string errorMessage = value.GetType().GetProperty("ErrorMessage")!.GetValue(value)?.ToString();
            string errorDetail = value.GetType().GetProperty("ErrorDetail")!.GetValue(value)?.ToString();
            return new ServiceError
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                ErrorDetail = errorDetail
            };
        }
    }
}
