extends Node

signal enemy_current_hp_changed
signal enemy_max_hp_changed
signal attack_button_disabled
signal enemy_image_changed
signal stop_auto_attack
signal bossfight_current_time_changed
signal bossfight_max_time_changed

var enemy_level = PlayerData.current_floor
var enemy_max_hp = 0
var enemy_current_hp = 0
var bossfifight_max_time = 30
var bossfifight_current_time = 0


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


func change_enemy_current_hp(hp):
	enemy_current_hp = hp
	emit_signal("enemy_current_hp_changed", enemy_current_hp)


func change_enemy_max_hp(max_hp):
	enemy_max_hp = max_hp
	emit_signal("enemy_max_hp_changed", enemy_max_hp)


func change_enemy_image(image):
	emit_signal("enemy_image_changed", image)


func calculate_enemy_hp():
	var base_hp = 100
	var level_multiplier = 1 + ((enemy_level - 1) * 0.29) ** 7 + PlayerData.total_enemy_count
	var enemy_hp = base_hp * level_multiplier * 28 * armor_multiplier(PlayerData.player_level)
	change_enemy_max_hp(enemy_hp)
	change_enemy_current_hp(enemy_hp)


func manual_attack():
	if enemy_current_hp > PlayerData.player_damage:
		enemy_current_hp -= PlayerData.player_damage
		change_enemy_current_hp(enemy_current_hp)
		if enemy_current_hp <= PlayerData.player_damage:
			emit_signal("attack_button_disabled")


func pick_random_enemy_picture():
	var random_number = randi() % 6 + 1
	var enemy_picture = "res://assets/enemies/enemy" + str(random_number) + ".png"
	change_enemy_image(enemy_picture)


func auto_attack():
	if enemy_current_hp > 0:
		enemy_current_hp -= PlayerData.player_damage
		change_enemy_current_hp(enemy_current_hp)
		if enemy_current_hp <= 0:
			change_enemy_current_hp(0)
			emit_signal("stop_auto_attack")


func boss_auto_attack():
	if enemy_current_hp > 0:
		enemy_current_hp -= PlayerData.player_damage
		change_enemy_current_hp(enemy_current_hp)
		if enemy_current_hp <= 0:
			change_enemy_current_hp(0)


func set_bossfight_time(time):
	bossfifight_max_time = time
	bossfifight_current_time = time
	emit_signal("bossfight_current_time_changed", bossfifight_current_time)
	emit_signal("bossfight_max_time_changed", bossfifight_max_time)


func decrease_bossfight_time():
	bossfifight_current_time -= 1
	emit_signal("bossfight_current_time_changed", bossfifight_current_time)
