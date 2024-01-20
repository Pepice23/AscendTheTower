extends Node

var weapon_qualities: Dictionary = {
	"Poor": 1,
	"Uncommon": 2,
	"Rare": 3,
	"Epic": 4,
	"Legendary": 5
}

var base_damage: int = 100

# Called when the node enters the scene tree for the first time.
func _ready():
	for i in range(0, 10):
		create_weapon()


# A function that creates a new weapon with random stats
func create_weapon():
	var level: int            = PlayerData.player_level
	var armor_multiplier: int   = PlayerData.armor_multiplier
	var level_multiplier = 1 + ((level - 1) * 0.42) ** 6
	var qualities: Array      = weapon_qualities.keys()
	var random_index          = randi() % qualities.size()
	var weapon_quality = qualities[random_index]
	var weapon_damage : int = weapon_qualities[weapon_quality] * base_damage * level_multiplier * armor_multiplier
	var weapon = {
		"quality": weapon_quality,
		"name": weapon_quality + " Weapon",
		"damage": weapon_damage,
		"value": weapon_qualities[weapon_quality] * 20
	}
	return weapon

	
