using System;

namespace TranslationManagement.Api.Common;

public class ApiLayerException : Exception
{
    public ApiLayerException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}