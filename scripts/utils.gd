extends Node

func format_number(value: float) -> String:
	var units: PackedStringArray = ["", "K", "M", "B", "T", "P", "E", "Z", "Y"]
	var unit_index: int = 0

	while value >= 1000 and unit_index < units.size() - 1:
		value /= 1000
		unit_index += 1

	if unit_index == 0:  # No unit, it's a small number
		return str(int(value))  # Convert to integer to remove decimal part
	elif value - int(value) == 0:  # If the number is a whole number after division
		return "%d%s" % [int(value), units[unit_index]]  # Use integer format
	else:  # For larger numbers with significant decimal part
		return "%.2f%s" % [value, units[unit_index]]  # Keep two decimal places