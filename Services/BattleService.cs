using System;
using System.Threading;

namespace AscendTheTower.Services;

public class BattleService
{
    private readonly PlayerService _playerService;
    private readonly EnemyService _enemyService;

    private PeriodicTimer _autoAttackTimer;
    public BattleService(PlayerService playerService, EnemyService enemyService)
    {
        _playerService = playerService;
        _enemyService = enemyService;
    }

    public event Action OnChange;

    public void StartBattle()
    {
        if (_playerService.CurrentEnemy == 15)
        {
            _enemyService.SetBossTime();
            StartBossBattle();
        }
        else
        {
            StartNormalBattle();
        }

    }

    private async void StartNormalBattle()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(0.5));
        while (await _autoAttackTimer.WaitForNextTickAsync())
        {
            _enemyService.AutoAttack();
            OnChange?.Invoke();
            if (_enemyService.EnemyCurrentHp <= 0)
            {
                _autoAttackTimer.Dispose();
                _enemyService.SetCurrentHpToNull();
                break;
            }
        }
        _playerService.AddXpMinMax(5,8);
        _playerService.AddEnemy();
        _enemyService.SetEnemyHp();
        StartBattle();
    }


    private async void StartBossBattle()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await _autoAttackTimer.WaitForNextTickAsync())
        {
            _enemyService.BossAutoAttack();
            OnChange?.Invoke();
            if (_enemyService.EnemyCurrentHp <= 0 && _enemyService.CurrentBossTime > 0) //Player won
            {
                _autoAttackTimer.Dispose();
                _enemyService.SetCurrentHpToNull();
                _playerService.AddXp(20);
                _playerService.AddFloor();
                _playerService.SetBackgroundImage();
                break;
            }
            if (_enemyService.CurrentBossTime <= 0 ) //Player lost
            {
                _autoAttackTimer.Dispose();
                _enemyService.SetCurrentHpToNull();
                _playerService.ResetFloor();
                break;
            }
        }
        _enemyService.SetEnemyHp();
        StartBattle();
    }






}
