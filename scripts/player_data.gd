extends Node

signal  floor_changed
signal  monster_changed
signal  player_level_changed
signal  armor_multiplier_changed


var current_floor = 1
var current_mosnter = 1
var player_level = 1
var armor_multiplier = 1


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.



func on_floor_changed():
	current_floor = current_floor + 1
	emit_signal("floor_changed", current_floor)

func change_monster_count():
	current_mosnter += 1
	emit_signal("monster_changed", current_mosnter)

func change_player_level():
	player_level += 1
	emit_signal("player_level_changed", player_level)

func change_armor_multiplier(multiplier):
	armor_multiplier = multiplier
	emit_signal("armor_multiplier_changed", armor_multiplier)

