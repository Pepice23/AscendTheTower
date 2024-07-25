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
            StartBossBattle();
        }
        else
        {
            StartNormalBattle();
        }

    }

    private async void StartNormalBattle()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await _autoAttackTimer.WaitForNextTickAsync())
        {
            _enemyService.AutoAttack();
            OnChange?.Invoke();
            if (_enemyService.EnemyCurrentHp <= 0)
            {
                _enemyService.SetCurrentHPToNull();
                _autoAttackTimer.Dispose();
                break;
            }
        }
    }


    private void StartBossBattle()
    {
        Console.WriteLine("Starting boss battle");
    }






}
