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
    public int PlayerMoney { get; private set; } = 25000;
    public string PlayerWeaponName { get; private set; } = "Starter Weapon";
    public string PlayerWeaponImage { get; private set; } = "images/weapons/poor/p1.png";
    public long PlayerWeaponDamage { get; private set; } = 10;
    public string BackgroundImage { get; private set; }
    public string PlayerArmorName { get; private set; } = "Starter Armor";
    public string PlayerArmorImage { get; private set; } = "images/armors/starter.png";
    public int ArmorMultiplier { get; private set; } = 1;
    public int CriticalChance { get; private set; } = 5;

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
        TotalDamage = PlayerWeaponDamage * ArmorMultiplier;
        OnChange?.Invoke();
    }

    public void UpdatePlayerArmor(string name, string image, int multiplier)
    {
        PlayerArmorName = name;
        PlayerArmorImage = image;
        ArmorMultiplier = multiplier;
        TotalDamage = PlayerWeaponDamage * ArmorMultiplier;
        OnChange?.Invoke();
    }

    public void AddGold(int gold)
    {
        PlayerMoney += gold;
        OnChange?.Invoke();
    }

    public void RemoveGold(int gold)
    {
        PlayerMoney -= gold;
        OnChange?.Invoke();
    }

    public void UpgradeCriticalChance()
    {
        CriticalChance++;
        OnChange?.Invoke();
    }
}
