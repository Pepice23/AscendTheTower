extends Node

signal enemy_current_hp_changed
signal enemy_max_hp_changed
signal attack_button_disabled


var enemy_level = PlayerData.current_floor
var enemy_max_hp = 0
var enemy_current_hp = 0



# Called when the node enters the scene tree for the first time.
func _ready():
	pass  # Replace with function body.

func armor_multiplier(level):
	if level < 10:
		return 1
	elif level < 20:
		return 2
	elif level < 30:
		return 4
	elif level < 40:
		return 6
	elif level < 50:
		return 8
	elif level < 60:
		return 10
	elif level < 70:
		return 12
	elif level < 80:
		return 14
	elif level < 90:
		return 16
	else:
		return 18

func calculate_enemy_hp():
	var base_hp = 100
	var level_multiplier = 1 + ((enemy_level - 1) * 0.29) ** 7 + PlayerData.total_enemy_count
	var enemy_hp = base_hp * level_multiplier * 28 * armor_multiplier(PlayerData.player_level)
	enemy_max_hp = enemy_hp
	enemy_current_hp = enemy_hp
	emit_signal("enemy_max_hp_changed", enemy_max_hp)
	emit_signal("enemy_current_hp_changed", enemy_current_hp)

func enemy_defeat():
	enemy_current_hp = 0
	PlayerData.total_enemy_count += 1
	emit_signal("enemy_current_hp_changed", enemy_current_hp)

func manual_attack():
	if enemy_current_hp > PlayerData.player_damage:
		enemy_current_hp -= PlayerData.player_damage
		emit_signal("enemy_current_hp_changed", enemy_current_hp)
		if enemy_current_hp <= PlayerData.player_damage:
			emit_signal("attack_button_disabled")

