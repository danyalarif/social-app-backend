using System;

namespace social_app_backend.Utils;

public class ServiceException : Exception
{
    public int StatusCode { get; }
    public ServiceException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    public ServiceException(string message, Exception innerException, int statusCode) : base(message, innerException) {
        StatusCode = statusCode;
    }
}
