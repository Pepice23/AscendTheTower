extends Node2D

@onready var animation_player = $BattleAnimation/AnimationPlayer
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func _on_add_floor_pressed():
	PlayerData.on_floor_changed()


func _on_add_monster_pressed():
	PlayerData.change_monster_count()


func _on_timer_timeout():
	animation_player.play("automatic_attack")


func _on_add_level_pressed():
	PlayerData.change_level()


func _on_add_xp_pressed():
	PlayerData.gain_random_xp(8,22)


func _on_add_damage_pressed():
	pass # Replace with function body.
