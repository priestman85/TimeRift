extends Node

var current_chapter: int = 0
var player_position: Vector2 = Vector2.ZERO
var inventory: Array = []
var story_flags: Dictionary = {}

func _ready():
	load_game()

func save_game():
	var save_data = {
		"chapter": current_chapter,
		"position": {"x": player_position.x, "y": player_position.y},
		"inventory": inventory,
		"flags": story_flags
	}
	var file = FileAccess.open("user://savegame.json", FileAccess.WRITE)
	file.store_string(JSON.stringify(save_data))
	file.close()

func load_game():
	if not FileAccess.file_exists("user://savegame.json"):
		return

	var file = FileAccess.open("user://savegame.json", FileAccess.READ)
	var data = JSON.parse_string(file.get_as_text())
	file.close()

	if data:
		current_chapter = data.get("chapter", 0)
		player_position = Vector2(data.get("position", {}).get("x", 0), data.get("position", {}).get("y", 0))
		inventory = data.get("inventory", [])
		story_flags = data.get("flags", {})

func set_flag(flag_name: String, value: bool):
	story_flags[flag_name] = value

func get_flag(flag_name: String) -> bool:
	return story_flags.get(flag_name, false)

func add_item(item_name: String):
	inventory.append(item_name)

func remove_item(item_name: String):
	inventory.erase(item_name)

func has_item(item_name: String) -> bool:
	return item_name in inventory

func change_chapter(new_chapter: int):
	current_chapter = new_chapter
	save_game()
