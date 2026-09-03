extends CharacterBody2D

@export var speed: float = 80.0
@export var health: float = 50.0
@export var damage: float = 10.0
@export var detection_range: float = 200.0
@export var attack_range: float = 30.0

var player: CharacterBody2D = null
var is_chasing = false
var attack_cooldown = 0.0

@onready var sprite = $Sprite2D
@onready var detection_area = $DetectionArea
@onready var attack_area = $AttackArea

func _ready():
	detection_area.body_entered.connect(_on_detection_body_entered)
	detection_area.body_exited.connect(_on_detection_body_exited)
	attack_area.body_entered.connect(_on_attack_body_entered)

func _physics_process(delta):
	if attack_cooldown > 0:
		attack_cooldown -= delta

	if is_chasing and player:
		var direction = (player.global_position - global_position).normalized()
		velocity = direction * speed

		sprite.flip_h = direction.x < 0

		# Атака
		if global_position.distance_to(player.global_position) < attack_range:
			if attack_cooldown <= 0:
				attack()
				attack_cooldown = 1.0
	else:
		velocity = Vector2.ZERO

	move_and_slide()

func attack():
	if player and player.has_method("take_damage"):
		player.take_damage(damage)

func take_damage(amount: float):
	health -= amount
	if health <= 0:
		die()

func die():
	queue_free()

func _on_detection_body_entered(body):
	if body.is_in_group("player"):
		player = body
		is_chasing = true

func _on_detection_body_exited(body):
	if body == player:
		player = null
		is_chasing = false

func _on_attack_body_entered(body):
	pass
