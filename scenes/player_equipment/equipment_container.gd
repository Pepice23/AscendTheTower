extends VBoxContainer

# Weapon
@onready var weapon_picture = $WeaponContainer/WeaponImage
@onready var weapon_name_text = $WeaponContainer/WeaponStatContainer/WeaponNameLabel
@onready var weapon_damage_text = $WeaponContainer/WeaponStatContainer/DamageTextContainer/DamageTextLabel

# Armor
@onready var armor_picture = $ArmorContainer/ArmorImage
@onready var armor_name_text = $ArmorContainer/ArmorStatContainer/ArmorNameLabel
@onready var armor_multiplier_text = $ArmorContainer/ArmorStatContainer/MultiplierTextContainer/MultiplierTextLabel


# Called when the node enters the scene tree for the first time.
func _ready():
	PlayerData.connect("player_weapon_changed", get_weapon_from_signal)

# Weapon
func get_weapon_from_signal(weapon):
	change_weapon_image(weapon.image)
	change_weapon_name(weapon.name)
	change_weapon_damage(weapon.damage)

func change_weapon_image(weapon_image):
	weapon_picture.texture = load(weapon_image)

func change_weapon_name(weapon_name):
	weapon_name_text.text = weapon_name

func change_weapon_damage(weapon_damage):
	weapon_damage_text.text = Utils.format_number(weapon_damage)

# Armor
func change_armor_image(armor_image):
	armor_image.texture = load(armor_image)

func change_armor_name(armor_name):
	armor_name_text.text = armor_name

func change_armor_multiplier(armor_multiplier):
	armor_multiplier_text.text = str(armor_multiplier) + "x"
