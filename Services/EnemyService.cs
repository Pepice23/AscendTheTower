using System;
namespace AscendTheTower.Services;

public class EnemyService
{
    public event Action OnChange;
    public long EnemyCurrentHp { get; private set; } = 100;
    public long EnemyMaxHp { get; private set; } = 100;
    public int CurrentBossTime { get; private set; } = 30;
    public int MaxBossTime { get; private set; } = 30;
}
