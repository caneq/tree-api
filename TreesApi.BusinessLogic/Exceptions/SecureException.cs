﻿namespace TreesApi.BusinessLogic.Exceptions;

public class SecureException : Exception
{
    internal SecureException()
    {
    }

    internal SecureException(string message)
        : base(message)
    {
    }

    internal SecureException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
