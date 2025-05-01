namespace PersonManagment.Application.Exceptions;

public class ValidationException(string message) : Exception(message)
{
}