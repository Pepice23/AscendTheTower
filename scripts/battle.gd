extends Node

signal start_normal_battle

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

func choose_battle_type():
	if PlayerData.current_enemy != 15:
		normal_battle()
		print("normal battle")
	else:
		boss_battle()
		print("boss battle")

func normal_battle():
	EnemyData.pick_random_enemy_picture()
	EnemyData.calculate_enemy_hp()
	emit_signal("start_normal_battle")


func boss_battle():
	pass
