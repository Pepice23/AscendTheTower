extends VBoxContainer

@onready var player_level_text = $LevelContainer/LevelText
@onready var player_xp_progress_bar = $XPContainer/ProgressBar
@onready var player_xp_progress_bar_text = $XPContainer/ProgressBar/ProgressBarText
@onready var player_damage_text = $DamageContainer/DamageText
@onready var player_money_text = $MoneyContainer/MoneyText


# Called when the node enters the scene tree for the first time.
func _ready():
	set_progress_fill_color()
	PlayerData.connect("player_level_changed", _on_level_changed)
	PlayerData.connect("current_xp_changed", _on_current_xp_changed)
	PlayerData.connect("xp_to_next_level_changed", _on_max_xp_changed)
	PlayerData.connect("player_damage_changed", _on_damage_changed)
	PlayerData.connect("player_money_changed", _on_money_changed)
	set_defaults()

func set_defaults():
	player_level_text.text = str(PlayerData.player_level)
	player_xp_progress_bar.value = PlayerData.current_xp
	player_xp_progress_bar.max_value = PlayerData.xp_to_next_level
	update_xp_progressbar_text()
	player_damage_text.text = str(PlayerData.player_damage)
	player_money_text.text = str(PlayerData.player_money) + " Gold"

func set_progress_fill_color():
	var progress_fill_style = StyleBoxFlat.new()
	progress_fill_style.bg_color = Color(Color.PURPLE)
	player_xp_progress_bar.add_theme_stylebox_override("fill", progress_fill_style)

func update_xp_progressbar_text():
	player_xp_progress_bar_text.text = Utils.format_number(player_xp_progress_bar.value) + " / " + Utils.format_number(player_xp_progress_bar.max_value)

func _on_current_xp_changed(current_xp):
	player_xp_progress_bar.value = current_xp
	update_xp_progressbar_text()
	
func _on_max_xp_changed(max_xp):
	player_xp_progress_bar.max_value = max_xp
	update_xp_progressbar_text()

func _on_level_changed(level):
	player_level_text.text = str(level)
	
func _on_damage_changed(damage):
	player_damage_text.text = Utils.format_number(damage)
	
func _on_money_changed(money):
	player_money_text.text = Utils.format_number(money) + " Gold"
