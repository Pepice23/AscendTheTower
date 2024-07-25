using System;
namespace AscendTheTower.Services;

public class PlayerService
{
    public event Action OnChange;

    public int CurrentFloor { get; set; } = 1;

    public void AddFloor()
    {
        CurrentFloor++;
        OnChange?.Invoke();
    }
    
}
