using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    private readonly Dictionary<WeaponRarity, Tuple<string, double>> _rarityWeights =
        new Dictionary<WeaponRarity, Tuple<string, double>>()
        {
            { WeaponRarity.Poor, new Tuple<string, double>("Poor Weapon", 1) },
            { WeaponRarity.Uncommon, new Tuple<string, double>("Uncommon Weapon", 1.2) },
            { WeaponRarity.Rare, new Tuple<string, double>("Rare Weapon", 1.4) },
            { WeaponRarity.Epic, new Tuple<string, double>("Epic Weapon", 1.6) },
            { WeaponRarity.Legendary, new Tuple<string, double>("Legendary Weapon", 2) },
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
        Debug.WriteLine("Dice roll: " + diceRoll);
        switch (diceRoll)
        {
            case <= 50:
                _weaponName = _rarityWeights[WeaponRarity.Poor].Item1;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Poor].Item2;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("poor");
                break;
            case > 50 and <= 75:
                _weaponName = _rarityWeights[WeaponRarity.Uncommon].Item1;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Uncommon].Item2;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("uncommon");
                break;
            case > 75 and <= 90:
                _weaponName = _rarityWeights[WeaponRarity.Rare].Item1;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Rare].Item2;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("rare");
                break;
            case > 90 and <= 97:
                _weaponName = _rarityWeights[WeaponRarity.Epic].Item1;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Epic].Item2;
                _weaponImage = HelperFunctions.GetRandomWeaponImage("epic");
                break;
            case > 97:
                _weaponName = _rarityWeights[WeaponRarity.Legendary].Item1;
                _weaponMultiplier = _rarityWeights[WeaponRarity.Legendary].Item2;
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
        Debug.WriteLine("Weapon created: {0} Damage: {1} Image: {2}", _weaponDamage, _weaponDamage, _weaponImage);
        if (_weaponDamage > _playerService.PlayerWeaponDamage)
        {
            _playerService.UpdatePlayerWeapon(_weaponName, _weaponImage, _weaponDamage);
        }
    }
}
