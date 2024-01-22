extends Node

var weapon_qualities: Dictionary = {
	"Poor": 1,
	"Uncommon": 2,
	"Rare": 3,
	"Epic": 4,
	"Legendary": 5
}


# Function to load weapon images from the assets directory
func load_weapon_images():
				# Initialize an empty dictionary to store weapon images
				var weapon_images: Dictionary = {}
				# Array of weapon qualities
				var qualities: Array = ["Poor", "Uncommon", "Rare", "Epic", "Legendary"]
				for quality in qualities:
								# Construct the path to the directory for this quality
								var path = "res://assets/weapons/" + quality.to_lower() + "/"
								# Open the directory
								var dir = DirAccess.open(path)
								if dir != null:
												# Begin listing files in the directory
												dir.list_dir_begin()
												# Initialize an empty array to store image file paths
												var image_files: Array = []
												# Get the first file in the directory
												var file_name = dir.get_next()
												while file_name != "":
																# If the file is a PNG image, add it to the array
																if file_name.ends_with(".png"):
																				image_files.append(path + file_name)
																# Get the next file in the directory
																file_name = dir.get_next()
												# Store the array of image file paths in the dictionary
												weapon_images[quality] = image_files
				# Return the dictionary of weapon images
				return weapon_images

var base_damage: int = 100

# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# A function that creates a new weapon with random stats
func create_weapon():

	# Get the player's level and armor multiplier from PlayerData
	var level: int = PlayerData.player_level

	# Calculate the level multiplier based on the player's level
	var level_multiplier = 1 + ((level - 1) * 0.42) ** 6

	# Get the keys (qualities) from the weapon_qualities dictionary
	var qualities: Array = weapon_qualities.keys()

	# Generate a random index to select a random quality
	var random_index = randi() % qualities.size()
	var weapon_quality = qualities[random_index]

	# Calculate the weapon damage based on the selected quality, base damage, level multiplier, and armor multiplier
	var weapon_damage: int = weapon_qualities[weapon_quality] * base_damage * level_multiplier

	# Load the weapon images
	var weapon_images: Dictionary = load_weapon_images()

	# Get the images for the selected quality
	var images: Array = weapon_images[weapon_quality]

	# Select a random image from the images array
	var random_image_index = randi() % images.size()
	var weapon_image = images[random_image_index]

	# Create a dictionary for the weapon with its quality, name, damage, value, and image
	var weapon = {
					"quality": weapon_quality,
					"name": weapon_quality + " Weapon",
					"damage": weapon_damage,
					"value": weapon_qualities[weapon_quality] * 20,
					"image": weapon_image
				}

	# Print the weapon dictionary and return it
	return weapon

