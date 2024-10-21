namespace Pep.Ioc;

public class ContainerLocatorMisuseException : Exception
{
    public ContainerLocatorMisuseException() : base("Attempt on accessing the ContainerLocator")
    {
    }
}