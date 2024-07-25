using System;

namespace AscendTheTower.Services;

public class EnemyService
{
    private readonly PlayerService _playerService;
    public EnemyService(PlayerService playerService)
    {
        _playerService = playerService;
    }
    public event Action OnChange;
    public long EnemyCurrentHp { get; private set; } = 100;
    public long EnemyMaxHp { get; private set; } = 100;
    public int CurrentBossTime { get; private set; } = 30;
    public int MaxBossTime { get; private set; } = 30;

    private const int BaseHp = 100;
    private const double GrowthRate  = 0.02;
    private const double EnemyIncrement = 0.02;

    public void SetEnemyHp()
    {
        var calculatedHp = BaseHp * Math.Pow(1 + GrowthRate, _playerService.CurrentFloor) * Math.Pow(1 + EnemyIncrement, _playerService.TotalEnemyCount);
        EnemyMaxHp = (long)calculatedHp;
        EnemyCurrentHp = EnemyMaxHp;
        OnChange?.Invoke();
    }

    public void SetBossTime()
    {
        CurrentBossTime = MaxBossTime;
        OnChange?.Invoke();
    }

    public void AutoAttack()
    {
        EnemyCurrentHp -= _playerService.PlayerDamage;
        OnChange?.Invoke();
    }

    public void SetCurrentHPToNull()
    {
        EnemyCurrentHp = 0;
        OnChange?.Invoke();
    }

}
