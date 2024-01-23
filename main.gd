extends Node2D

@onready var battle_timer = $BattleTimer
@onready var boss_battle_timer = $BossBattleTimer
@onready var animation_player = $BattleAnimation/AnimationPlayer

# Called when the node enters the scene tree for the first time.
func _ready():
	EnemyData.connect("attack_button_disabled", attack_button_disabled)
	EnemyData.connect("stop_auto_attack", stop_battle_timer)
	Battle.connect("start_normal_battle", _on_battle_timer_start)
	Battle.connect("start_boss_battle", _on_boss_battle_timer_start)
	animation_player.connect("animation_finished", _on_animation_player_animation_finished)
	Battle.choose_battle_type()

func _on_attack_button_pressed():
	EnemyData.manual_attack()

func attack_button_disabled():
	$AttackButton.disabled = true


func _on_reset_enemy_pressed():
	PlayerData.reset_enemy_count()


func _on_button_pressed():
	PlayerData.add_new_weapon()


func _on_add_armor_pressed():
	PlayerData.get_next_armor()


func _on_battle_timer_start():
	battle_timer.start()
	$AttackButton.disabled = false

func _on_boss_battle_timer_start():
	boss_battle_timer.start()
	$AttackButton.disabled = false

func _on_battle_timer_timeout():
	animation_player.play("automatic_attack")


func stop_battle_timer():
	battle_timer.stop()
	animation_player.play("monster_death")


func _on_animation_player_animation_finished(anim_name):
	if anim_name == "automatic_attack":
		EnemyData.auto_attack()
	elif anim_name == "monster_death":
		PlayerData.gain_random_xp(5, 8)
		PlayerData.change_player_money(10)
		PlayerData.change_monster_count()
		Battle.choose_battle_type()
	elif anim_name == "boss_automatic_attack":
		EnemyData.auto_attack()
		EnemyData.decrease_bossfight_time()
		print(EnemyData.bossfifight_current_time)
		if EnemyData.bossfifight_current_time == 0 && EnemyData.enemy_current_hp > 0:
			boss_battle_timer.stop()
			print("Player lost")
		elif EnemyData.bossfifight_current_time > 0 && EnemyData.enemy_current_hp <= 0:
			boss_battle_timer.stop()
			print("Player wins")
			PlayerData.gain_random_xp(20, 25)
			PlayerData.change_player_money(50)
			PlayerData.reset_enemy_count()
			PlayerData.on_floor_changed()

func _on_boss_battle_timer_timeout():
	animation_player.play("boss_automatic_attack")