using AscendTheTower.Helper;
using AscendTheTower.Configuration;
using Microsoft.Extensions.Options;

namespace AscendTheTower.Services;

public class WeaponService(PlayerService playerService, IOptions<GameConfig> config)
{
    private readonly PlayerService _playerService = playerService;
    private readonly GameConfig _config = config.Value;

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
        var level = _playerService.PlayerLevel;
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
        
        _weaponScalingFactor = _config.Weapons.LevelScalingFactors[range];
    }

    private void GetRandomRarity()
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

        _weaponName = $"{rarity} Weapon";
        _weaponMultiplier = _config.Weapons.RarityMultipliers[rarity.ToString()];
        _weaponImage = HelperFunctions.GetRandomWeaponImage(rarity.ToString().ToLower());
    }

    private long CalculateWeaponDamage()
    {
        var calculatedDamage =
            _config.Weapons.BaseDamage * _weaponMultiplier * Math.Pow(_weaponScalingFactor, _playerService.PlayerLevel);
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
