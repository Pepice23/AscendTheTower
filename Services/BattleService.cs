using System;
using System.Threading;
using System.Threading.Tasks;

namespace AscendTheTower.Services;

public class BattleService
{
    private readonly PlayerService _playerService;
    private readonly EnemyService _enemyService;
    private readonly WeaponService _weaponService;

    private PeriodicTimer _autoAttackTimer;

    public BattleService(PlayerService playerService, EnemyService enemyService, WeaponService weaponService)
    {
        _playerService = playerService;
        _enemyService = enemyService;
        _weaponService = weaponService;
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
        await NormalBattleTimer();
        GiveRewardNormal();
        _playerService.AddEnemy();
        _enemyService.SetEnemyHp();
        StartBattle();
    }

    private async void StartBossBattle()
    {
        await BossBattleTimer();
        _enemyService.SetEnemyHp();
        StartBattle();
    }

    private async Task NormalBattleTimer()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
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
    }


    private async Task BossBattleTimer()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await _autoAttackTimer.WaitForNextTickAsync())
        {
            _enemyService.BossAutoAttack();
            OnChange?.Invoke();
            if (_enemyService.EnemyCurrentHp <= 0 && _enemyService.CurrentBossTime > 0)
            {
                //Player won
                _autoAttackTimer.Dispose();
                _enemyService.SetCurrentHpToNull();
                GiveRewardBoss();
                _playerService.AddFloor();
                _playerService.SetBackgroundImage();
                break;
            }

            if (_enemyService.CurrentBossTime <= 0)
            {
                //Player lost
                _autoAttackTimer.Dispose();
                _enemyService.SetCurrentHpToNull();
                _playerService.ResetFloor();
                break;
            }
        }
    }

    private void RollDiceForWeapon()
    {
        var roll = new Random().Next(1, 101);
        if (roll <= 10)
        {
            _weaponService.CreateRandomWeapon();
        }
    }

    private void GiveRewardNormal()
    {
        _playerService.AddXpMinMax(5, 8);
        _playerService.AddGold(_playerService.CurrentFloor * 10);
        RollDiceForWeapon();
    }

    private void GiveRewardBoss()
    {
        _playerService.AddXp(20);
        _playerService.AddGold(_playerService.CurrentFloor * 10 + 100);
        _weaponService.CreateRandomWeapon();
    }
}
