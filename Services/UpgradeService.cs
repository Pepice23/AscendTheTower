using System;

namespace AscendTheTower.Services;

public class Upgrade
{
    public string Name { get; set; }
    public int CurrentRank { get; set; }
    public int MaxRank { get; set; }
    public int Price { get; set; }
    public string Effect { get; set; }
}

public class UpgradeService
{
    private readonly PlayerService _playerService;

    public event Action OnChange;

    public UpgradeService(PlayerService playerService)
    {
        _playerService = playerService;
    }

    public readonly Upgrade CriticalStrikeChanceUpgrade = new()
    {
        Name = "Critical Strike Chance",
        CurrentRank = 0,
        MaxRank = 15,
        Price = 1000,
        Effect = "Critical strike chance increased by 1% per rank"
    };

    public void UpgradeCriticalStrikeChance()
    {
        _playerService.RemoveGold(CriticalStrikeChanceUpgrade.Price);
        CriticalStrikeChanceUpgrade.CurrentRank++;
        CriticalStrikeChanceUpgrade.Price += 1000;
        _playerService.UpgradeCriticalChance();
        OnChange?.Invoke();
    }
}
