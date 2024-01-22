extends Node

# Signals
signal floor_changed
signal enemy_changed
signal player_level_changed
signal armor_multiplier_changed
signal current_xp_changed
signal xp_to_next_level_changed
signal player_damage_changed
signal player_money_changed
signal show_boss_timer
signal hide_boss_timer
signal player_weapon_changed


# Variables
var current_floor: int = 1
var current_enemy: int = 1
var total_enemy_count: int = 0
var player_level: int = 1
var armor_multiplier: int = 1
var current_xp: int = 0
var xp_to_next_level: int = 100
var player_damage: int = 10
var player_money: int = 0
var player_weapon = null

# Called when the node enters the scene tree for the first time.
func _ready():
	pass  # Replace with function body.

# Group: Game Progression
# This group contains functions related to the progression of the game

# Function: on_floor_changed
# This function increments the current floor and emits a signal
func on_floor_changed():
	current_floor = current_floor + 1
	emit_signal("floor_changed", current_floor)

# Function: change_monster_count
# This function increments the current monster count and emits a signal
func change_monster_count():
	current_enemy += 1
	emit_signal("enemy_changed", current_enemy)
	boss_timer_toggle()

# Function: reset_monster_count
# This function resets the current monster count and emits a signal
func reset_enemy_count():
	current_enemy = 1
	emit_signal("enemy_changed", current_enemy)
	boss_timer_toggle()

# Group: Player Stats
# This group contains functions related to the player's stats

# Function: change_armor_multiplier
# This function changes the armor multiplier and emits a signal
func change_armor_multiplier(multiplier):
	armor_multiplier = multiplier
	emit_signal("armor_multiplier_changed", armor_multiplier)

# Function: gain_random_xp
# This function increases the player's XP by a random amount and checks if the player should level up
func gain_random_xp(min_percentage, max_percentage):
	var random_percentage = randi_range(min_percentage, max_percentage)
	var xp_gain: int = (xp_to_next_level * (random_percentage / 100.0))
	print(xp_gain)
	current_xp += xp_gain
	emit_signal("current_xp_changed", current_xp)
	if current_xp >= xp_to_next_level:
		level_up()

# Function: increase_xp_to_level_up
# This function increases the XP needed to level up
func increase_xp_to_level_up(increase_percentage):
	var xp_increase = round(xp_to_next_level * (increase_percentage / 100.0))
	xp_to_next_level += xp_increase
	emit_signal("xp_to_next_level_changed", xp_to_next_level)

# Function: level_up
# This function levels up the player and adjusts the XP accordingly
func level_up():
	player_level += 1
	var xp_remainder: int = current_xp - xp_to_next_level
	current_xp = xp_remainder
	increase_xp_to_level_up(10)
	emit_signal("player_level_changed", player_level)
	emit_signal("current_xp_changed", current_xp)
	emit_signal("xp_to_next_level_changed", xp_to_next_level)

# Function: change_player_damage
# This function changes the player's damage and emits a signal
func change_player_damage(damage):
	player_damage = damage
	emit_signal("player_damage_changed", player_damage)

# Function: change_player_money
# This function changes the player's money and emits a signal
func change_player_money(amount):
	player_money += amount
	emit_signal("player_money_changed", player_money)

# Function: toggle_boss_timer
# This function toggles the boss timer
func boss_timer_toggle():
	if current_enemy == 15:
		emit_signal("show_boss_timer")
	else:
		emit_signal("hide_boss_timer")

# Function: add_new_weapon
# This function adds a new weapon to the player's inventory
func add_new_weapon():
	player_weapon = WeaponCreator.create_weapon()
	emit_signal("player_weapon_changed", player_weapon)
	emit_signal("player_damage_changed", player_weapon.damage)
