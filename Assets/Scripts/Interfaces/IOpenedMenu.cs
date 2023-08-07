using System;

public interface IOpenedMenu
{
    public event Action OnOpened;
    public event Action OnClosed;
}
