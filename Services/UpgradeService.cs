namespace AscendTheTower.Services;

public class Upgrade
{
    public string? Name { get; init; }
    public int CurrentRank { get; set; }
    public int MaxRank { get; init; }
    public int Price { get; set; }
    public string? Effect { get; init; }
    public Action<Upgrade>? UpgradeMethod { get; init; }
}

public class UpgradeService
{
    private readonly PlayerService _playerService;
    public List<Upgrade> Upgrades { get; set; }

    public UpgradeService(PlayerService playerService)
    {
        _playerService = playerService;
        Upgrades = new List<Upgrade>()
        {
            new()
            {
                Name = "Critical Strike Chance",
                CurrentRank = 0,
                MaxRank = 15,
                Price = 1000,
                Effect = "Critical strike chance increased by 1% per rank",
                UpgradeMethod = UpgradeCriticalStrikeChance
            },
            new()
            {
                Name = "Gold Gain",
                CurrentRank = 1,
                MaxRank = 4,
                Price = 2000,
                Effect = "Gold gain multiplied by 1x per rank",
                UpgradeMethod = UpgradeGoldGain
            },
            new()
            {
                Name = "Speed Up Timer",
                CurrentRank = 0,
                MaxRank = 5,
                Price = 5000,
                Effect = "Speed up timer by 10% per rank",
                UpgradeMethod = UpgradeTimer
            }
        };
    }

    public event Action? OnChange;


    private void UpgradeCriticalStrikeChance(Upgrade upgrade)
    {
        _playerService.RemoveGold(upgrade.Price);
        upgrade.CurrentRank++;
        upgrade.Price += 1000;
        _playerService.UpgradeCriticalChance();
        OnChange?.Invoke();
    }

    private void UpgradeGoldGain(Upgrade upgrade)
    {
        _playerService.RemoveGold(upgrade.Price);
        upgrade.CurrentRank++;
        upgrade.Price += 2000;
        _playerService.UpgradeGoldGain();
        OnChange?.Invoke();
    }

    private void UpgradeTimer(Upgrade upgrade)
    {
        _playerService.RemoveGold(upgrade.Price);
        upgrade.CurrentRank++;
        upgrade.Price += 5000;
        _playerService.UpgradeBattleSpeed();
        OnChange?.Invoke();
    }
}
