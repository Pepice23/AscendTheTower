extends HBoxContainer

@onready var loot_weapon_image = $WeaponImage
@onready var gold_gain_text = $LootInfoContainer/GoldGainTextLabel
@onready var xp_gain_text = $LootInfoContainer/XPGainTextLabel
@onready var loot_weapon_name = $LootInfoContainer/NewWeaponTextLabel
@onready var loot_compare_text = $LootInfoContainer/WeaponCompareTextLabel

# Called when the node enters the scene tree for the first time.
func _ready():
	PlayerData.connect("xp_gained", change_xp_gain_text)
	PlayerData.connect("gold_gained", change_gold_gain_text)
	PlayerData.connect("weapon_compare", change_loot_compare_text)

func change_weapon_image(picture):
	loot_weapon_image.texture = load(picture)

func change_gold_gain_text(gold):
	gold_gain_text.text = "You gained " + Utils.format_number(gold) + " gold!"

func change_xp_gain_text(xp):
	xp_gain_text.text = "You gained " + Utils.format_number(xp) + " XP!"

func change_loot_weapon_name(weapon_name):
	loot_weapon_name.text = "You found a " + weapon_name + "!"

func change_loot_compare_text(compare):
	loot_compare_text.text = compare