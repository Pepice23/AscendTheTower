using System;
using System.Collections.Generic;

namespace AscendTheTower.Services;

public class Armor
{
    public string Name { get; set; }
    public int Multiplier { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public int RequiredFloor { get; set; }
}

public class ArmorService
{
    private readonly PlayerService _playerService;

    public ArmorService(PlayerService playerService)
    {
        _playerService = playerService;
    }

    public event Action OnChange;

    public List<Armor> Armors = new List<Armor>
    {
        new Armor
        {
            Name = "Level 10 Armor", Multiplier = 2, RequiredFloor = 10, Price = 5000,
            Image = "images/armors/level10.png"
        },
        new Armor
        {
            Name = "Level 20 Armor", Multiplier = 4, RequiredFloor = 20, Price = 20000,
            Image = "images/armors/level20.png"
        },
        new Armor
        {
            Name = "Level 30 Armor", Multiplier = 6, RequiredFloor = 30, Price = 35000,
            Image = "images/armors/level30.png"
        },
        new Armor
        {
            Name = "Level 40 Armor", Multiplier = 8, RequiredFloor = 40, Price = 50000,
            Image = "images/armors/level40.png"
        },
        new Armor
        {
            Name = "Level 50 Armor", Multiplier = 10, RequiredFloor = 50, Price = 65000,
            Image = "images/armors/level50.png"
        },
        new Armor
        {
            Name = "Level 60 Armor", Multiplier = 12, RequiredFloor = 60, Price = 80000,
            Image = "images/armors/level60.png"
        },
        new Armor
        {
            Name = "Level 70 Armor", Multiplier = 14, RequiredFloor = 70, Price = 95000,
            Image = "images/armors/level70.png"
        },
        new Armor
        {
            Name = "Level 80 Armor", Multiplier = 16, RequiredFloor = 80, Price = 110000,
            Image = "images/armors/level80.png"
        },
        new Armor
        {
            Name = "Level 90 Armor", Multiplier = 18, RequiredFloor = 90, Price = 125000,
            Image = "images/armors/level90.png"
        },
    };


    public Armor GetNextPurchasableArmor()
    {
        foreach (var armor in Armors)
        {
            if (_playerService.CurrentFloor >= armor.RequiredFloor &&
                _playerService.CurrentFloor <= armor.RequiredFloor + 9 && _playerService.PlayerMoney > armor.Price &&
                armor.Multiplier > _playerService.ArmorMultiplier)
            {
                OnChange?.Invoke();
                return armor;
            }
        }

        return null; // No purchasable armor available
    }
}
