using System.Collections.Generic;

namespace AscendTheTower.Configuration;

public class GameConfig
{
    public WeaponConfig Weapons { get; set; } = new();
    public BattleConfig Battle { get; set; } = new();
    public RewardConfig Rewards { get; set; } = new();
}

public class WeaponConfig
{
    public int BaseDamage { get; set; } = 10;
    public Dictionary<string, double> RarityMultipliers { get; set; } = new()
    {
        { "Poor", 1.0 },
        { "Uncommon", 1.5 },
        { "Rare", 2.0 },
        { "Epic", 2.5 },
        { "Legendary", 3.0 }
    };
    public Dictionary<string, double> LevelScalingFactors { get; set; } = new()
    {
        { "1-10", 1.22 },
        { "11-20", 1.233 },
        { "21-30", 1.25 },
        { "31-40", 1.27 },
        { "41-50", 1.288 },
        { "51-60", 1.297 },
        { "61-70", 1.304 },
        { "71-80", 1.31 },
        { "81-90", 1.315 },
        { "91-100", 1.32 }
    };
}

public class BattleConfig
{
    public int BossEnemyThreshold { get; set; } = 15;
    public double DefaultBattleSpeed { get; set; } = 1.0;
}

public class RewardConfig
{
    public int MinXpReward { get; set; } = 5;
    public int MaxXpReward { get; set; } = 8;
    public double GoldMultiplier { get; set; } = 10.0;
}
