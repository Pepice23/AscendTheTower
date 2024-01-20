extends Node2D

@onready var animation_player = $BattleAnimation/AnimationPlayer
# Called when the node enters the scene tree for the first time.
func _ready():
	pass  # Replace with function body.

func _on_add_floor_pressed():
	PlayerData.on_floor_changed()


func _on_add_monster_pressed():
	PlayerData.change_monster_count()


func _on_timer_timeout():
	animation_player.play("automatic_attack")


func _on_add_level_pressed():
	PlayerData.change_level()


func _on_add_xp_pressed():
	PlayerData.gain_random_xp(8, 22)


func _on_add_damage_pressed():
	PlayerData.change_player_damage(10)


func _on_add_money_pressed():
	PlayerData.change_player_money(10)



func _on_set_enemy_hp_pressed():
	EnemyData.calculate_enemy_hp()


func _on_defeat_enemy_pressed():
	EnemyData.enemy_defeat()
