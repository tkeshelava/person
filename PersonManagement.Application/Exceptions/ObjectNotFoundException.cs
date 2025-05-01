namespace PersonManagment.Application.Exceptions;

public class ObjectNotFoundException(string message) : Exception(message)
{
}