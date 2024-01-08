extends Node

signal  floor_changed
signal  monster_changed


var current_floor = 1
var current_mosnter = 1


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.



func on_floor_changed():
	current_floor = current_floor + 1
	emit_signal("floor_changed", current_floor)

func change_monster_count():
	current_mosnter += 1
	emit_signal("monster_changed", current_mosnter)