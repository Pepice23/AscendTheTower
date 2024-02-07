extends HBoxContainer

@onready var floor_progressbar = $FloorProgress
@onready var enemy_progressbar = $EnemyProgress
@onready var floor_progressbar_label = $FloorProgress/FloorProgressLabel
@onready var enemy_progressbar_label = $EnemyProgress/EnemyProgressLabel


# Called when the node enters the scene tree for the first time.
func _ready():
	PlayerData.connect("floor_changed", _on_floor_changed)
	PlayerData.connect("enemy_changed", _on_enemy_count_changed)
	set_defaults()


func set_defaults():
	set_progress_fill_color()
	floor_progressbar.value = PlayerData.current_floor
	enemy_progressbar.value = PlayerData.current_enemy
	update_floor_progressbar_text()
	update_enemy_progressbar_text()


func set_progress_fill_color():
	var progress_fill_style = StyleBoxFlat.new()
	progress_fill_style.bg_color = Color(Color.BLUE)
	floor_progressbar.add_theme_stylebox_override("fill", progress_fill_style)
	enemy_progressbar.add_theme_stylebox_override("fill", progress_fill_style)


func update_floor_progressbar_text():
	floor_progressbar_label.text = "Floor: " + str(floor_progressbar.value) + " / " + "100"


func update_enemy_progressbar_text():
	enemy_progressbar_label.text = "Enemy: " + str(enemy_progressbar.value) + " / " + "15"


func _on_floor_changed(tower_floor):
	floor_progressbar.value = tower_floor
	update_floor_progressbar_text()


func _on_enemy_count_changed(enemy_count):
	enemy_progressbar.value = enemy_count
	update_enemy_progressbar_text()
