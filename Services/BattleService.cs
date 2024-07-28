namespace AscendTheTower.Services;

public class BattleService(
    PlayerService playerService,
    EnemyService enemyService,
    WeaponService weaponService,
    ArmorService armorService)
{
    private PeriodicTimer? _autoAttackTimer;

    public event Action? OnChange;
    public Armor? PurchasableArmor { get; private set; }

    public void StartBattle()
    {
        if (playerService.CurrentEnemy == 15)
        {
            enemyService.SetBossTime();
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
        playerService.AddEnemy();
        enemyService.SetEnemyHp();
        StartBattle();
    }

    private async void StartBossBattle()
    {
        await BossBattleTimer();
        enemyService.SetEnemyHp();
        StartBattle();
    }

    private async Task NormalBattleTimer()
    {
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
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
        _autoAttackTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
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
        playerService.AddXpMinMax(5, 8);
        playerService.AddGold(playerService.CurrentFloor * playerService.GoldMultiplier * 10);
        RollDiceForWeapon();
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
