﻿
using System.Text.Json;
using AscendTheTower.Helper;

namespace AscendTheTower.Services;

class PlayerData
{
    public int CurrentFloor { get; set; }
    public int CurrentEnemy { get;  set; }
    public int TotalEnemyCount { get;  set; }
    public int PlayerLevel { get;  set; }
    public long CurrentXp { get;  set; }
    public long MaxXp { get; set; }
    public long TotalDamage { get;  set; }
    public int PlayerMoney { get;  set; }
    public string? PlayerWeaponName { get; set; }
    public string? PlayerWeaponImage { get;  set; }
    public long PlayerWeaponDamage { get;  set; }
    public string? PlayerArmorName { get;  set; }
    public string? PlayerArmorImage { get;  set; }
    public int ArmorMultiplier { get;  set; } = 1;
    public int CriticalChance { get;  set; } = 5;
    public int GoldMultiplier { get;  set; } = 1;
    public float BattleSpeed { get;  set; } = 1;
}


public class PlayerService
{
    public event Action? OnChange;

    public int CurrentFloor { get; private set; } = 1;
    public int CurrentEnemy { get; private set; } = 1;
    public int TotalEnemyCount { get; private set; }
    public int PlayerLevel { get; private set; } = 1;
    public long CurrentXp { get; private set; }
    public long MaxXp { get; private set; } = 400;
    public long TotalDamage { get; private set; } = 10;
    public int PlayerMoney { get; private set; }
    public string? PlayerWeaponName { get; private set; } = "Starter Weapon";
    public string? PlayerWeaponImage { get; private set; } = "images/weapons/poor/p1.png";
    public long PlayerWeaponDamage { get; private set; } = 10;
    public string? BackgroundImage { get; private set; }
    public string? PlayerArmorName { get; private set; } = "Starter Armor";
    public string? PlayerArmorImage { get; private set; } = "images/armors/starter.png";
    public int ArmorMultiplier { get; private set; } = 1;
    public int CriticalChance { get; private set; } = 5;
    public int GoldMultiplier { get; private set; } = 1;
    public float BattleSpeed { get; private set; } = 1;

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

    public void UpdatePlayerWeapon(string? name, string? image, long damage)
    {
        PlayerWeaponName = name;
        PlayerWeaponImage = image;
        PlayerWeaponDamage = damage;
        TotalDamage = PlayerWeaponDamage * ArmorMultiplier;
        OnChange?.Invoke();
    }

    public void UpdatePlayerArmor(string? name, string? image, int multiplier)
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

    public void UpgradeGoldGain()
    {
        GoldMultiplier++;
        OnChange?.Invoke();
    }

    public void UpgradeBattleSpeed()
    {
        BattleSpeed -= 0.1f;
        OnChange?.Invoke();
    }

    public void SavePlayerData()
    {
        var playerData = new PlayerData
        {
            CurrentFloor = CurrentFloor,
            CurrentEnemy = CurrentEnemy,
            TotalEnemyCount = TotalEnemyCount,
            PlayerLevel = PlayerLevel,
            CurrentXp = CurrentXp,
            MaxXp = MaxXp,
            TotalDamage = TotalDamage,
            PlayerMoney = PlayerMoney,
            PlayerWeaponName = PlayerWeaponName,
            PlayerWeaponImage = PlayerWeaponImage,
            PlayerWeaponDamage = PlayerWeaponDamage,
            PlayerArmorName = PlayerArmorName,
            PlayerArmorImage = PlayerArmorImage,
            ArmorMultiplier = ArmorMultiplier,
            CriticalChance = CriticalChance,
            GoldMultiplier = GoldMultiplier,
            BattleSpeed = BattleSpeed
        };

        var playerDataJson = JsonSerializer.Serialize(playerData);

        File.WriteAllText("playerdata.json", playerDataJson);
    }

    public void LoadPlayerData()
    {
        var playerDataJson = File.ReadAllText("playerdata.json");
        var playerData = JsonSerializer.Deserialize<PlayerData>(playerDataJson);
        if (playerData != null)
        {
            CurrentFloor = playerData.CurrentFloor;
            CurrentEnemy = playerData.CurrentEnemy;
            TotalEnemyCount = playerData.TotalEnemyCount;
            PlayerLevel = playerData.PlayerLevel;
            CurrentXp = playerData.CurrentXp;
            MaxXp = playerData.MaxXp;
            TotalDamage = playerData.TotalDamage;
            PlayerMoney = playerData.PlayerMoney;
            PlayerWeaponName = playerData.PlayerWeaponName;
            PlayerWeaponImage = playerData.PlayerWeaponImage;
            PlayerWeaponDamage = playerData.PlayerWeaponDamage;
            PlayerArmorName = playerData.PlayerArmorName;
            PlayerArmorImage = playerData.PlayerArmorImage;
            ArmorMultiplier = playerData.ArmorMultiplier;
            CriticalChance = playerData.CriticalChance;
            GoldMultiplier = playerData.GoldMultiplier;
            BattleSpeed = playerData.BattleSpeed;
        }
    }
}
