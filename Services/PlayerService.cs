using System;
using AscendTheTower.Helper;

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
    public string BackgroundImage { get; private set; }

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

    public void AddXpMinMax(int minPercent, int maxPercent)
    {
        var percent = new Random().Next(minPercent, maxPercent);
        CurrentXp += percent * MaxXp / 100;
        CheckLevelUp();
        OnChange?.Invoke();
    }

    public void AddXp(int percent)
    {
        CurrentXp += percent * MaxXp / 100;
        CheckLevelUp();
        OnChange?.Invoke();
    }

    private void CalculateMaxXp()
    {
        var newXp = MaxXp * 1.15;
        MaxXp = (long)newXp;
        OnChange?.Invoke();
    }

    private void CheckLevelUp()
    {
        if (CurrentXp >= MaxXp)
        {
            CurrentXp -= MaxXp;
            PlayerLevel++;
            CalculateMaxXp();
            OnChange?.Invoke();
        }
    }

    public void SetBackgroundImage()
    {
        BackgroundImage = HelperFunctions.GetRandomBackgroundImage();
        OnChange?.Invoke();
    }
}
