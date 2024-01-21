extends HBoxContainer

@onready var floor_progressbar = $FloorProgress
@onready var monster_progressbar = $MonsterProgress
@onready var floor_progressbar_label = $FloorProgress/FloorProgressLabel
@onready var monster_progressbar_label = $MonsterProgress/MonsterProgressLabel

# Called when the node enters the scene tree for the first time.
func _ready():
	set_progress_fill_color()
	PlayerData.connect("floor_changed",_on_floor_changed)
	PlayerData.connect("monster_changed",_on_monster_count_changed)
	

func set_progress_fill_color():
	var progress_fill_style = StyleBoxFlat.new()
	progress_fill_style.bg_color = Color(Color.BLUE)
	floor_progressbar.add_theme_stylebox_override("fill", progress_fill_style)
	monster_progressbar.add_theme_stylebox_override("fill", progress_fill_style)

func update_floor_progressbar_text():
	floor_progressbar_label.text = "Floor: " + str(floor_progressbar.value) + " / " + "100"
		
func update_monster_progressbar_text():
	monster_progressbar_label.text = "Monster: " + str(monster_progressbar.value) + " / " + "15"
	
func _on_floor_changed(tower_floor):
	floor_progressbar.value = tower_floor
	update_floor_progressbar_text()
	
func _on_monster_count_changed(monster_count):
	monster_progressbar.value = monster_count
	update_monster_progressbar_text()