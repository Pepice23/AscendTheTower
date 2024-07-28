using System;
using AscendTheTower.Helper;

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
    public string EnemyImage { get; private set; }
    public bool PlayerCriticalStrike { get; private set; }

    private const int BaseHp = 100;
    private const double GrowthRate = 0.02;
    private const double EnemyIncrement = 0.02;

    public void SetEnemyHp()
    {
        var calculatedHp = BaseHp * Math.Pow(1 + GrowthRate, _playerService.CurrentFloor) *
                           Math.Pow(1 + EnemyIncrement, _playerService.TotalEnemyCount);
        EnemyMaxHp = (long)calculatedHp;
        EnemyCurrentHp = EnemyMaxHp;
        SetEnemyImage();
        OnChange?.Invoke();
    }

    public void SetBossTime()
    {
        CurrentBossTime = MaxBossTime;
        OnChange?.Invoke();
    }

    public void AutoAttack()
    {
        var roll = new Random().Next(1, 101);
        if (roll <= 5)
        {
            PlayerCriticalStrike = true;
            var criticalDamage = _playerService.TotalDamage * 1.5;
            EnemyCurrentHp -= (long)criticalDamage;
        }
        else
        {
            PlayerCriticalStrike = false;
            EnemyCurrentHp -= _playerService.TotalDamage;
        }

        OnChange?.Invoke();
    }

    public void BossAutoAttack()
    {
        EnemyCurrentHp -= _playerService.TotalDamage;
        CurrentBossTime--;
        OnChange?.Invoke();
    }

    public void ManualAttack()
    {
        if (EnemyCurrentHp > _playerService.TotalDamage)
        {
            EnemyCurrentHp -= _playerService.TotalDamage;
            OnChange?.Invoke();
        }
    }

    public void SetCurrentHpToNull()
    {
        EnemyCurrentHp = 0;
        OnChange?.Invoke();
    }

    public void SetEnemyImage()
    {
        EnemyImage = HelperFunctions.GetRandomEnemyImage();
        OnChange?.Invoke();
    }
}
