using System;
using System.Collections.Generic;
using AscendTheTower.Helper;

namespace AscendTheTower.Services;

public class WeaponService
{
    private readonly PlayerService _playerService;
    private const int BaseDamage = 10;

    public WeaponService(PlayerService playerService)
    {
        _playerService = playerService;
    }

    private string _weaponName;
    private double _weaponMultiplier;
    private string _weaponImage;
    private long _weaponDamage;
    private double _weaponScalingFactor;

    private enum WeaponRarity
    {
        Poor,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    private readonly Dictionary<WeaponRarity, (string name, double multiplier)> _rarityWeights = new()
    {
        { WeaponRarity.Poor, ("Poor Weapon", 1) },
        { WeaponRarity.Uncommon, ("Uncommon Weapon", 1.5) },
        { WeaponRarity.Rare, ("Rare Weapon", 2) },
        { WeaponRarity.Epic, ("Epic Weapon", 2.5) },
        { WeaponRarity.Legendary, ("Legendary Weapon", 3) }
    };


    private void GetScalingFactor()
    {
        switch (_playerService.PlayerLevel)
        {
            case <= 10:
                _weaponScalingFactor = 1.22;
                break;
            case > 10 and <= 20:
                _weaponScalingFactor = 1.233;
                break;
            case > 20 and <= 30:
                _weaponScalingFactor = 1.25;
                break;
            case > 30 and <= 40:
                _weaponScalingFactor = 1.27;
                break;
            case > 40 and <= 50:
                _weaponScalingFactor = 1.288;
                break;
            case > 50 and <= 60:
                _weaponScalingFactor = 1.297;
                break;
            case > 60 and <= 70:
                _weaponScalingFactor = 1.304;
                break;
            case > 70 and <= 80:
                _weaponScalingFactor = 1.31;
                break;
            case > 80 and <= 90:
                _weaponScalingFactor = 1.315;
                break;
            case > 90 and <= 100:
                _weaponScalingFactor = 1.32;
                break;
        }
    }


    private void GetRandomRarity()
    {
        var diceRoll = new Random().Next(1, 101);
        switch (diceRoll)
        {
            case <= 50:
                _weaponName = _rarityWeights[WeaponRarity.Poor].name;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Poor].multiplier;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("poor");
                break;
            case > 50 and <= 75:
                _weaponName = _rarityWeights[WeaponRarity.Uncommon].name;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Uncommon].multiplier;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("uncommon");
                break;
            case > 75 and <= 90:
                _weaponName = _rarityWeights[WeaponRarity.Rare].name;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Rare].multiplier;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("rare");
                break;
            case > 90 and <= 97:
                _weaponName = _rarityWeights[WeaponRarity.Epic].name;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Epic].multiplier;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("epic");
                break;
            case > 97:
                _weaponName = _rarityWeights[WeaponRarity.Legendary].name;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Legendary].multiplier;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("legendary");
                break;
        }
    }

    private long CalculateWeaponDamage()
    {
        var calculatedDamage =
            BaseDamage * _weaponMultiplier * Math.Pow(_weaponScalingFactor, _playerService.PlayerLevel);
        return (long)calculatedDamage;
    }

    public void CreateRandomWeapon()
    {
        GetRandomRarity();
        GetScalingFactor();
        _weaponDamage = CalculateWeaponDamage();
        if (_weaponDamage > _playerService.PlayerWeaponDamage)
            _playerService.UpdatePlayerWeapon(_weaponName, _weaponImage, _weaponDamage);
    }
}
