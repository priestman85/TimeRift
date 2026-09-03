extends Area2D

@export var damage_per_second: float = 50.0
@export var pulse_speed: float = 2.0

var time_elapsed: float = 0.0
var is_active: bool = true

@onready var sprite = $Sprite2D
@onready var collision = $CollisionShape2D
@onready var particles = $Particles
@onready var audio = $AudioStreamPlayer2D

func _ready():
	body_entered.connect(_on_body_entered)
	body_exited.connect(_on_body_exited)

func _process(delta):
	if not is_active:
		return

	time_elapsed += delta

	# Пульсация
	var scale_factor = 1.0 + sin(time_elapsed * pulse_speed) * 0.1
	sprite.scale = Vector2(scale_factor, scale_factor)

	# Вращение
	sprite.rotation += delta * 0.5

func _on_body_entered(body):
	if body.is_in_group("player"):
		# Начать наносить урон
		print("Внимание! Пузырь времени!")
		# Здесь можно добавить визуальный эффект

func _on_body_exited(body):
	if body.is_in_group("player"):
		print("Покинул зону аномалии")

func activate():
	is_active = true
	sprite.visible = true
	collision.disabled = false
	particles.emitting = true

func deactivate():
	is_active = false
	sprite.visible = false
	collision.disabled = true
	particles.emitting = false
