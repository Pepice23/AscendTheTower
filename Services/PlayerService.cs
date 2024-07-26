using System;
using AscendTheTower.Helper;

namespace AscendTheTower.Services;

public class PlayerService
{
    public event Action OnChange;

    public int CurrentFloor { get; private set; } = 1;
    public int CurrentEnemy { get; private set; } = 1;
    public int TotalEnemyCount { get; private set; }
    public int PlayerLevel { get; private set; } = 1;
    public long CurrentXp { get; private set; }
    public long MaxXp { get; private set; } = 400;
    public long TotalDamage { get; private set; } = 10;
    public int PlayerMoney { get; private set; } = 0;
    public string PlayerWeaponName { get; private set; }
    public string PlayerWeaponImage { get; private set; }
    public long PlayerWeaponDamage { get; private set; }
    public string BackgroundImage { get; private set; }
    private int _armorMultiplier = 1;

    public void AddFloor()
    {
        CurrentFloor++;
        CurrentEnemy = 1; //reset enemy
        OnChange?.Invoke();
    }

    public void AddEnemy()
    {
        CurrentEnemy++;
        TotalEnemyCount++;
        OnChange?.Invoke();
    }

    public void ResetFloor()
    {
        CurrentEnemy = 1;
        TotalEnemyCount -= 14;
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

    public void UpdatePlayerWeapon(string name, string image, long damage)
    {
        PlayerWeaponName = name;
        PlayerWeaponImage = image;
        PlayerWeaponDamage = damage;
        TotalDamage = PlayerWeaponDamage * _armorMultiplier;
        OnChange?.Invoke();
    }
}
