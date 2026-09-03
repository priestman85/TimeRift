extends CharacterBody2D

const SPEED = 150.0
const JUMP_VELOCITY = -300.0
const HEALTH_MAX = 100
const HUNGER_MAX = 100
const THIRST_MAX = 100

var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
var health = HEALTH_MAX
var hunger = HUNGER_MAX
var thirst = THIRST_MAX
var is_injured = false
var injury_timer = 0.0

@onready var sprite = $Sprite2D
@onready var health_bar = $UI/HealthBar
@onready var hunger_bar = $UI/HungerBar
@onready var thirst_bar = $UI/ThirstBar

func _ready():
	update_ui()

func _physics_process(delta):
	# Гравитация
	if not is_on_floor():
		velocity.y += gravity * delta

	# Прыжок
	if Input.is_action_just_pressed("jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Движение
	var direction = Input.get_axis("move_left", "move_right")
	if direction:
		velocity.x = direction * SPEED
		sprite.flip_h = direction < 0
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)

	move_and_slide()

	# Голод и жажда
	hunger -= delta * 2
	thirst -= delta * 3
	hunger = clamp(hunger, 0, HUNGER_MAX)
	thirst = clamp(thirst, 0, THIRST_MAX)

	# Урон от голода/жажды
	if hunger <= 0 or thirst <= 0:
		take_damage(delta * 10)

	# Травма
	if is_injured:
		injury_timer -= delta
		if injury_timer <= 0:
			is_injured = false

	update_ui()

func take_damage(amount: float):
	health -= amount
	health = clamp(health, 0, HEALTH_MAX)
	if health <= 0:
		die()

func heal(amount: float):
	health += amount
	health = clamp(health, 0, HEALTH_MAX)

func eat(amount: float):
	hunger += amount
	hunger = clamp(hunger, 0, HUNGER_MAX)

func drink(amount: float):
	thirst += amount
	thirst = clamp(thirst, 0, THIRST_MAX)

func apply_injury(duration: float):
	is_injured = true
	injury_timer = duration

func die():
	# TODO: Game Over
	print("Game Over")
	get_tree().reload_current_scene()

func update_ui():
	if health_bar:
		health_bar.value = health
	if hunger_bar:
		hunger_bar.value = hunger
	if thirst_bar:
		thirst_bar.value = thirst
