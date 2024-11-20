namespace Pep.Ioc;

public static class ExceptionExtensions
{
    public static Exception GetRootException(this Exception exception) => 
        exception.InnerException == null 
            ? exception 
            : exception.InnerException.GetRootException();
}