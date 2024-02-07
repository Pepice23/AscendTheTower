extends VBoxContainer

@onready var enemy_level_text = $LevelContainer/LevelText
@onready var enemy_hp_progress_bar = $HPContainer/HPProgressBar
@onready var enemy_hp_progress_bar_text = $HPContainer/HPProgressBar/HPTextLabel
@onready var boss_timer_container = $BossTimerContainer
@onready var boss_time_progress_bar = $BossTimerContainer/TimeProgressBar
@onready var boss_time_progress_bar_text = $BossTimerContainer/TimeProgressBar/TimeTextLabel


# Called when the node enters the scene tree for the first time.
func _ready():
	EnemyData.connect("enemy_current_hp_changed", _on_current_hp_changed)
	EnemyData.connect("enemy_max_hp_changed", _on_max_hp_changed)
	EnemyData.connect("bossfight_current_time_changed", _on_current_time_changed)
	EnemyData.connect("bossfight_max_time_changed", _on_max_time_changed)
	PlayerData.connect("show_boss_timer", _on_show_bossfight_timer)
	PlayerData.connect("hide_boss_timer", _on_hide_bossfight_timer)
	PlayerData.connect("floor_changed", _on_level_changed)
	set_defaults()


func set_defaults():
	set_hp_progress_fill_color()
	set_boss_progress_fill_color()
	boss_timer_container.visible = false
	enemy_level_text.text = str(PlayerData.current_floor)


func set_hp_progress_fill_color():
	var progress_fill_style = StyleBoxFlat.new()
	progress_fill_style.bg_color = Color(Color.DARK_RED)
	enemy_hp_progress_bar.add_theme_stylebox_override("fill", progress_fill_style)


func set_boss_progress_fill_color():
	var progress_fill_style = StyleBoxFlat.new()
	progress_fill_style.bg_color = Color(Color.RED)
	boss_time_progress_bar.add_theme_stylebox_override("fill", progress_fill_style)


func update_hp_progressbar_text():
	enemy_hp_progress_bar_text.text = Utils.format_number(enemy_hp_progress_bar.value)


func update_boss_progressbar_text():
	boss_time_progress_bar_text.text = str(boss_time_progress_bar.value) + " seconds left"


func _on_current_hp_changed(current_hp):
	enemy_hp_progress_bar.value = current_hp
	update_hp_progressbar_text()


func _on_max_hp_changed(max_hp):
	enemy_hp_progress_bar.max_value = max_hp


func _on_current_time_changed(current_time):
	boss_time_progress_bar.value = current_time
	update_boss_progressbar_text()


func _on_max_time_changed(max_time):
	boss_time_progress_bar.value = max_time


func _on_level_changed(level):
	enemy_level_text.text = str(level)


func _on_show_bossfight_timer():
	boss_timer_container.visible = true


func _on_hide_bossfight_timer():
	boss_timer_container.visible = false
