namespace Frontend.ControlsConfigurators;

public interface IControlConfigurator<T>
{
    public void ConfigureControl(T control);
}
