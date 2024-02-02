extends Node

var save_path = "user://save_game.save"

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
signal weapon_image_changed
signal player_armor_changed
signal xp_gained
signal gold_gained


# Variables
var current_floor: int = 1
var current_enemy: int = 1
var total_enemy_count: int = 0
var player_level: int = 1
var armor_multiplier: int = 1
var current_xp: int = 0
var xp_to_next_level: int = 100
var player_damage: int = 100000
var player_money: int = 0
var player_weapon = {
	"quality": "Poor",
	"name": "Starter Weapon",
	"damage": 100,
	"value": 1,
	"image": "res://assets/weapons/starter.png"}
var player_armor = {
	"name": "Starter Armor",
	"required_level": 1,
	"price:": 0,
	"armor_multiplier": 1,
	"image": "res://assets/armors/level1.png"
}

# Group: Save/Load
# This group contains functions related to saving and loading the game

# Function: save_game
# This function saves the game
func save_game():
	var file = FileAccess.open(save_path, FileAccess.WRITE)
	file.store_var(current_floor)
	file.store_var(total_enemy_count)
	file.store_var(player_level)
	file.store_var(armor_multiplier)
	file.store_var(current_xp)
	file.store_var(xp_to_next_level)
	file.store_var(player_damage)
	file.store_var(player_money)
	file.store_var(player_weapon)
	file.store_var(player_armor)
	file.close()

# Function: load_game
# This function loads the game
func load_game():
	if FileAccess.file_exists(save_path):
		var file = FileAccess.open(save_path, FileAccess.READ)
		current_floor = file.get_var()
		total_enemy_count = file.get_var()
		player_level = file.get_var()
		armor_multiplier = file.get_var()
		current_xp = file.get_var()
		xp_to_next_level = file.get_var()
		player_damage = file.get_var()
		player_money = file.get_var()
		player_weapon = file.get_var()
		player_armor = file.get_var()
		file.close()
		emit_signal("floor_changed", current_floor)
		emit_signal("enemy_changed", total_enemy_count)
		emit_signal("player_level_changed", player_level)
		emit_signal("armor_multiplier_changed", armor_multiplier)
		emit_signal("current_xp_changed", current_xp)
		emit_signal("xp_to_next_level_changed", xp_to_next_level)
		emit_signal("player_damage_changed", player_damage)
		emit_signal("player_money_changed", player_money)
		emit_signal("player_weapon_changed", player_weapon)
		emit_signal("weapon_image_changed", player_weapon.image)
		emit_signal("player_armor_changed", player_armor)
	else:
		print("No save file found")

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
	total_enemy_count += 1
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
	emit_signal("xp_gained", xp_gain)
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
	save_game()

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
	emit_signal("gold_gained", amount)

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
	emit_signal("weapon_image_changed", player_weapon.image)
	change_player_damage(player_weapon.damage * armor_multiplier)

# Function: get_next_armor
# This function gets the next armor in the list
func get_next_armor():
	player_armor = ArmorData.level_10_armor
	emit_signal("player_armor_changed", player_armor)
	change_armor_multiplier(player_armor.armor_multiplier)
	change_player_damage(player_weapon.damage * armor_multiplier)
