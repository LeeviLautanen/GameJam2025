extends ProgressBar
@onready var air_bar: ProgressBar = $"."
@onready var timer: Timer = $Timer

const MAX_AIR = 10
var air = MAX_AIR
var time = 10 #seconds after this player dies

func _ready():
	air_bar.value = air
	timer.start()


#timer to count down the air
func _on_timer_timeout():
	air -= 1 #time seconds
	air_bar.value = air
	if air <= 0:
		print("You got an electric shock")
		timer.stop()
		#game over, redirect to start scene
	
	
#replace the button with 'negative' object to decrease air
#connect the object to active the function
func _on_subs_button_pressed() -> void:
	air -= 2 #time seconds
	air_bar.value = air
	if air <= 0:
		print("You got an electric shock")
		timer.stop()
		#game over, redirect to start scene


#replace the button with airbubble object
#connect the object to active the function
func _on_add_button_pressed() -> void:
	air += 2 #seconds
	air_bar.value = air
	if air > 10:
		air = MAX_AIR
