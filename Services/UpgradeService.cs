namespace AscendTheTower.Services;

public class Upgrade
{
    public string? Name { get; set; }
    public int CurrentRank { get; set; }
    public int MaxRank { get; init; }
    public int Price { get; set; }
    public string? Effect { get; init; }
}

public class UpgradeService(PlayerService playerService)
{
    public event Action? OnChange;

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
        playerService.RemoveGold(CriticalStrikeChanceUpgrade.Price);
        CriticalStrikeChanceUpgrade.CurrentRank++;
        CriticalStrikeChanceUpgrade.Price += 1000;
        playerService.UpgradeCriticalChance();
        OnChange?.Invoke();
    }
}
