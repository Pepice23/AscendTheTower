using System;
namespace AscendTheTower.Services;

public class PlayerService
{
    public event Action OnChange;

    public int CurrentFloor { get; private set; } = 1;
    public int CurrentEnemy { get; private set; } = 1;
    public int PlayerLevel { get; private set; } = 1;
    public long CurrentXp { get; private set; } = 0;
    public long MaxXp { get; private set; } = 400;
    public long PlayerDamage { get; private set; } = 1;
    public int PlayerMoney { get; private set; } = 0;

    public void AddFloor()
    {
        CurrentFloor++;
        CurrentEnemy = 1; //reset enemy
        OnChange?.Invoke();
    }

    public void AddEnemy()
    {
        CurrentEnemy++;
        OnChange?.Invoke();
    }

    public void ResetEnemy()
    {
        CurrentEnemy = 1;
        OnChange?.Invoke();
    }
    
}
