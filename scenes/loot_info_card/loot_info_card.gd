extends HBoxContainer

@onready var gold_gain_text = $LootInfoContainer/GoldGainTextLabel
@onready var xp_gain_text = $LootInfoContainer/XPGainTextLabel


# Called when the node enters the scene tree for the first time.
func _ready():
	PlayerData.connect("xp_gained", change_xp_gain_text)
	PlayerData.connect("gold_gained", change_gold_gain_text)


func change_gold_gain_text(gold):
	gold_gain_text.text = "You gained " + Utils.format_number(gold) + " gold!"


func change_xp_gain_text(xp):
	xp_gain_text.text = "You gained " + Utils.format_number(xp) + " XP!"
