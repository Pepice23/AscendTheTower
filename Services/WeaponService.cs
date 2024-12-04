using AscendTheTower.Helper;
using AscendTheTower.Configuration;
using AscendTheTower.Services.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;

namespace AscendTheTower.Services;

public class WeaponService(
    PlayerService playerService,
    IOptions<GameConfig> config,
    ILogger<WeaponService> logger)
{
    private readonly PlayerService _playerService = playerService;
    private readonly GameConfig _config = config.Value;
    private readonly ILogger<WeaponService> _logger = logger;

    private string? _weaponName;
    private double _weaponMultiplier;
    private string? _weaponImage;
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

    private void GetScalingFactor()
    {
        try
        {
            var level = _playerService.PlayerLevel;
            if (level <= 0)
            {
                _logger.LogError("Invalid player level: {Level}", level);
                throw new ArgumentException($"Player level must be positive, got {level}");
            }

            var range = level switch
            {
                <= 10 => "1-10",
                <= 20 => "11-20",
                <= 30 => "21-30",
                <= 40 => "31-40",
                <= 50 => "41-50",
                <= 60 => "51-60",
                <= 70 => "61-70",
                <= 80 => "71-80",
                <= 90 => "81-90",
                _ => "91-100"
            };
            
            if (!_config.Weapons.LevelScalingFactors.TryGetValue(range, out var factor))
            {
                _logger.LogError("Missing scaling factor for level range: {Range}", range);
                throw new InvalidOperationException($"No scaling factor defined for level range {range}");
            }
            
            _weaponScalingFactor = factor;
            _logger.LogDebug("Set weapon scaling factor to {Factor} for level {Level}", factor, level);
        }
        catch (Exception ex) when (ex is not ArgumentException && ex is not InvalidOperationException)
        {
            _logger.LogError(ex, "Unexpected error in GetScalingFactor");
            throw;
        }
    }

    private void GetRandomRarity()
    {
        try
        {
            var diceRoll = new Random().Next(1, 101);
            var rarity = diceRoll switch
            {
                <= 50 => WeaponRarity.Poor,
                <= 75 => WeaponRarity.Uncommon,
                <= 90 => WeaponRarity.Rare,
                <= 97 => WeaponRarity.Epic,
                _ => WeaponRarity.Legendary
            };

            if (!_config.Weapons.RarityMultipliers.TryGetValue(rarity.ToString(), out var multiplier))
            {
                _logger.LogError("Missing multiplier for rarity: {Rarity}", rarity);
                throw new InvalidOperationException($"No multiplier defined for rarity {rarity}");
            }

            _weaponName = $"{rarity} Weapon";
            _weaponMultiplier = multiplier;
            _weaponImage = HelperFunctions.GetRandomWeaponImage(rarity.ToString().ToLower());
            
            _logger.LogInformation("Created {Rarity} weapon with multiplier {Multiplier}", rarity, multiplier);
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            _logger.LogError(ex, "Unexpected error in GetRandomRarity");
            throw;
        }
    }

    private long CalculateWeaponDamage()
    {
        try
        {
            if (_config.Weapons.BaseDamage <= 0)
            {
                _logger.LogError("Invalid base damage: {BaseDamage}", _config.Weapons.BaseDamage);
                throw new InvalidOperationException($"Base damage must be positive, got {_config.Weapons.BaseDamage}");
            }

            var calculatedDamage =
                _config.Weapons.BaseDamage * _weaponMultiplier * Math.Pow(_weaponScalingFactor, _playerService.PlayerLevel);
            
            _logger.LogDebug("Calculated weapon damage: {Damage} (Base: {Base}, Multiplier: {Mult}, Scale: {Scale}, Level: {Level})",
                calculatedDamage, _config.Weapons.BaseDamage, _weaponMultiplier, _weaponScalingFactor, _playerService.PlayerLevel);
            
            return (long)calculatedDamage;
        }
        catch (Exception ex) when (ex is not InvalidOperationException)
        {
            _logger.LogError(ex, "Error calculating weapon damage");
            throw;
        }
    }

    public Weapon? CreateRandomWeapon()
    {
        try
        {
            _logger.LogInformation("Starting weapon creation");
            GetRandomRarity();
            GetScalingFactor();
            _weaponDamage = CalculateWeaponDamage();

            var weapon = new Weapon
            {
                Name = _weaponName ?? "Unknown Weapon",
                Damage = _weaponDamage,
                Rarity = _weaponName?.Split()[0] ?? "Poor",
                Image = _weaponImage ?? "default_weapon.png"
            };

            if (_weaponDamage > _playerService.PlayerWeaponDamage)
            {
                _logger.LogInformation("Found better weapon! Old damage: {OldDamage}, New damage: {NewDamage}",
                    _playerService.PlayerWeaponDamage, _weaponDamage);
                _playerService.UpdatePlayerWeapon(weapon.Name, weapon.Image, weapon.Damage);
                return weapon;
            }
            else
            {
                _logger.LogDebug("Generated weapon ({Damage}) not better than current weapon ({CurrentDamage})",
                    _weaponDamage, _playerService.PlayerWeaponDamage);
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create random weapon");
            throw;
        }
    }

    public bool IsWeaponBetter(Weapon weapon)
    {
        return weapon.Damage > _playerService.PlayerWeaponDamage;
    }
}
