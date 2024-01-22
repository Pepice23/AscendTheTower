extends Node2D

@onready var player = $Player
@onready var weapon = $Weapon
@onready var enemy = $Enemy

# Called when the node enters the scene tree for the first time.
func _ready():
	PlayerData.connect("weapon_image_changed", set_weapon_image)

func set_player_image(image):
	player.texture = load(image)

func set_weapon_image(image):
	weapon.texture = load(image)

func set_enemy_image(image):
	enemy.texture = load(image)
