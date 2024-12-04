namespace AscendTheTower.Services;

public class Armor
{
    public required string Name { get; init; }
    public required int Multiplier { get; init; }
    public required string Image { get; init; }
    public required int Price { get; init; }
    public required int RequiredFloor { get; init; }
}

public class ArmorService(PlayerService playerService)
{
    public event Action? OnChange;

    private readonly List<Armor> _armors =
    [
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
        }
    ];


    public Armor? GetNextPurchasableArmor()
    {
        var purchasableArmor = _armors.FirstOrDefault(armor => 
            IsArmorAvailable(armor) && 
            IsWithinFloorRange(armor) && 
            CanAffordArmor(armor) && 
            IsArmorUpgrade(armor));
            
        if (purchasableArmor != null)
        {
            OnChange?.Invoke();
        }
        
        return purchasableArmor;
    }
    
    private bool IsArmorAvailable(Armor armor) => 
        playerService.CurrentFloor >= armor.RequiredFloor;
        
    private bool IsWithinFloorRange(Armor armor) => 
        playerService.CurrentFloor <= armor.RequiredFloor + 9;
        
    private bool CanAffordArmor(Armor armor) => 
        playerService.PlayerMoney > armor.Price;
        
    private bool IsArmorUpgrade(Armor armor) => 
        armor.Multiplier > playerService.ArmorMultiplier;
}
