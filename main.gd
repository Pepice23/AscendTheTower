extends Node2D


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func _on_add_floor_pressed():
	PlayerData.on_floor_changed()


func _on_add_monster_pressed():
	PlayerData.change_monster_count()
