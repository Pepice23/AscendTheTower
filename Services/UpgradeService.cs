using System.Text.Json;

namespace AscendTheTower.Services;

public class Upgrade
{
    public string? Name { get; init; }
    public int CurrentRank { get; set; }
    public int MaxRank { get; set; }
    public int Price { get; set; }
    public string? Effect { get; set; }
    public Action<Upgrade>? UpgradeMethod { get; set; }
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
        SaveUpgrades();
        _playerService.SavePlayerData();
        OnChange?.Invoke();
    }

    private void UpgradeGoldGain(Upgrade upgrade)
    {
        _playerService.RemoveGold(upgrade.Price);
        upgrade.CurrentRank++;
        upgrade.Price += 2000;
        _playerService.UpgradeGoldGain();
        SaveUpgrades();
        _playerService.SavePlayerData();
        OnChange?.Invoke();
    }

    private void UpgradeTimer(Upgrade upgrade)
    {
        _playerService.RemoveGold(upgrade.Price);
        upgrade.CurrentRank++;
        upgrade.Price += 5000;
        _playerService.UpgradeBattleSpeed();
        SaveUpgrades();
        _playerService.SavePlayerData();
        OnChange?.Invoke();
    }

    private void SaveUpgrades()
    {
        var upgradesWithoutMethods = Upgrades
            .Select(x => new
            {
                x.Name,
                x.CurrentRank,
                x.MaxRank,
                x.Price
            })
            .ToList();
        File.WriteAllText("Upgrades.json", JsonSerializer.Serialize(upgradesWithoutMethods));
    }

    public void LoadUpgrades()
    {
        if (File.Exists("Upgrades.json"))
        {
            var upgrades = JsonSerializer.Deserialize<List<Upgrade>>(File.ReadAllText("Upgrades.json"));
            if (upgrades != null)
                foreach (var upgrade in upgrades)
                {
                    var originalUpgrade = Upgrades.FirstOrDefault(u => u.Name == upgrade.Name);
                    if (originalUpgrade != null)
                    {
                        originalUpgrade.CurrentRank = upgrade.CurrentRank;
                        originalUpgrade.MaxRank = upgrade.MaxRank;
                        originalUpgrade.Price = upgrade.Price;
                    }
                }
        }
    }
}
