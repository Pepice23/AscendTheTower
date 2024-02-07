extends Node


# Called when the node enters the scene tree for the first time.
func _ready():
	pass  # Replace with function body.


var level_1_armor = {
	"name": "Starter Armor",
	"required_level": 1,
	"price:": 0,
	"armor_multiplier": 1,
	"image": "res://assets/armors/level1.png"
}

var level_10_armor = {
	"name": "Azure Quartz Armor",
	"required_level": 10,
	"price:": 1000,
	"armor_multiplier": 2,
	"image": "res://assets/armors/level10.png"
}

var level_20_armor = {
	"name": "Crystalline Sapphire Armor",
	"required_level": 20,
	"price:": 2000,
	"armor_multiplier": 4,
	"image": "res://assets/armors/level20.png"
}

var level_30_armor = {
	"name": "Radiant Emerald Armor",
	"required_level": 30,
	"price:": 3000,
	"armor_multiplier": 6,
	"image": "res://assets/armors/level30.png"
}

var level_40_armor = {
	"name": "Luminous Topaz Armor",
	"required_level": 40,
	"price:": 4000,
	"armor_multiplier": 8,
	"image": "res://assets/armors/level40.png"
}

var level_50_armor = {
	"name": "Shadowy Obsidian Armor",
	"required_level": 50,
	"price:": 5000,
	"armor_multiplier": 10,
	"image": "res://assets/armors/level50.png"
}

var level_60_armor = {
	"name": "Nebulous Amethyst Armor",
	"required_level": 60,
	"price:": 6000,
	"armor_multiplier": 12,
	"image": "res://assets/armors/level60.png"
}

var level_70_armor = {
	"name": "Ethereal Starstone Armor",
	"required_level": 70,
	"price:": 7000,
	"armor_multiplier": 14,
	"image": "res://assets/armors/level70.png"
}

var level_80_armor = {
	"name": "Dragon's Breath Ruby Armor",
	"required_level": 80,
	"price:": 8000,
	"armor_multiplier": 16,
	"image": "res://assets/armors/level80.png"
}

var level_90_armor = {
	"name": "Mythical Phoenix Feather Ore",
	"required_level": 90,
	"price:": 9000,
	"armor_multiplier": 18,
	"image": "res://assets/armors/level90.png"
}

var available_armors = [
	level_1_armor,
	level_10_armor,
	level_20_armor,
	level_30_armor,
	level_40_armor,
	level_50_armor,
	level_60_armor,
	level_70_armor,
	level_80_armor,
	level_90_armor
]
