namespace AscendTheTower.Services;

using AscendTheTower.Services.Models;

public class BattleService(
    PlayerService playerService,
    EnemyService enemyService,
    WeaponService weaponService,
    ArmorService armorService)
{
    private PeriodicTimer? _autoAttackTimer;

    public event Action? OnChange;
    public event Action<BattleRewardsData>? OnBattleCompleted;
    public Armor? PurchasableArmor { get; private set; }

    public async Task StartBattle()
    {
        try
        {
            if (playerService.CurrentEnemy == 15)
            {
                enemyService.SetBossTime();
                await StartBossBattle();
            }
            else
            {
                await StartNormalBattle();
            }
        }
        catch (Exception)
        {
            enemyService.SetCurrentHpToNull();
            _autoAttackTimer?.Dispose();
            throw;
        }
    }

    private async Task StartNormalBattle()
    {
        try 
        {
            await NormalBattleTimer();
            GiveRewardNormal();
            playerService.AddEnemy();
            enemyService.SetEnemyHp();
            await StartBattle();
        }
        catch (Exception)
        {
            enemyService.SetCurrentHpToNull();
            _autoAttackTimer?.Dispose();
            throw;
        }
    }

    private async Task StartBossBattle()
    {
        try 
        {
            await BossBattleTimer();
            enemyService.SetEnemyHp();
            await StartBattle();
        }
        catch (Exception)
        {
            enemyService.SetCurrentHpToNull();
            _autoAttackTimer?.Dispose();
            throw;
        }
    }

    private async Task NormalBattleTimer()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(playerService.BattleSpeed));
        while (await _autoAttackTimer.WaitForNextTickAsync())
        {
            enemyService.AutoAttack();
            OnChange?.Invoke();
            if (enemyService.EnemyCurrentHp <= 0)
            {
                _autoAttackTimer.Dispose();
                enemyService.SetCurrentHpToNull();
                break;
            }
        }
    }


    private async Task BossBattleTimer()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(playerService.BattleSpeed));
        while (await _autoAttackTimer.WaitForNextTickAsync())
        {
            enemyService.BossAutoAttack();
            OnChange?.Invoke();
            if (enemyService.EnemyCurrentHp <= 0 && enemyService.CurrentBossTime > 0)
            {
                //Player won
                _autoAttackTimer.Dispose();
                enemyService.SetCurrentHpToNull();
                GiveRewardBoss();
                playerService.AddFloor();
                playerService.SavePlayerData();
                PurchasableArmor = armorService.GetNextPurchasableArmor();
                playerService.SetBackgroundImage();
                break;
            }

            if (enemyService.CurrentBossTime <= 0)
            {
                //Player lost
                _autoAttackTimer.Dispose();
                enemyService.SetCurrentHpToNull();
                playerService.ResetFloor();
                playerService.SavePlayerData();
                break;
            }
        }
    }

    private void RollDiceForWeapon()
    {
        var roll = new Random().Next(1, 101);
        if (roll <= 10) weaponService.CreateRandomWeapon();
    }

    private void GiveRewardNormal()
    {
        var gold = Random.Shared.Next(10, 21);
        var xp = Random.Shared.Next(5, 11);

        playerService.AddGold(gold);
        playerService.AddXp(xp);

        var newWeapon = weaponService.CreateRandomWeapon();
        var isBetterWeapon = false;

        if (newWeapon is not null)
        {
            isBetterWeapon = weaponService.IsWeaponBetter(newWeapon);
        }

        var rewards = new BattleRewardsData
        {
            Gold = gold,
            Xp = xp,
            NewWeapon = newWeapon,
            IsBetterWeapon = isBetterWeapon
        };

        OnBattleCompleted?.Invoke(rewards);
        OnChange?.Invoke();
    }

    private void GiveRewardBoss()
    {
        playerService.AddXp(20);
        playerService.AddGold(playerService.CurrentFloor * playerService.GoldMultiplier * 30 + 100);
        weaponService.CreateRandomWeapon();
    }

    public void PurchaseArmor()
    {
        if (PurchasableArmor != null)
        {
            playerService.RemoveGold(PurchasableArmor.Price);
            playerService.UpdatePlayerArmor(PurchasableArmor.Name, PurchasableArmor.Image, PurchasableArmor.Multiplier);
        }

        PurchasableArmor = null;
        OnChange?.Invoke();
    }
}
