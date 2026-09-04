# Настройка сцены в Unity

## Пошаговая инструкция

### 1. Создание новой сцены
1. File → New Scene
2. Выберите **Basic (Built-in)**
3. File → Save As → `Assets/Scenes/MainScene.unity`

### 2. Создание игрока
1. Right-click в Hierarchy → 3D Object → Capsule
2. Переименуйте в "Player"
3. Добавьте компоненты:
   - `Character Controller` (Component → Physics)
   - `PlayerController` (Scripts → Player)
   - `PlayerHealth` (Scripts → Player)
4. Настройте `Character Controller`:
   - Height: 2
   - Center: (0, 1, 0)
   - Radius: 0.3

### 3. Создание камеры
1. Выберите Main Camera
2. Добавьте скрипт `ThirdPersonCamera` (Scripts → Player)
3. Настройте:
   - Target: перетащите Player
   - Offset: (0, 2, -4)
   - Mouse Sensitivity: 3

### 4. Создание земли
1. Right-click → 3D Object → Plane
2. Переименуйте в "Ground"
3. Масштаб: (10, 1, 10)
4. Создайте материал:
   - Right-click → Create → Material
   - Назовите "GroundMaterial"
   - Цвет: тёмно-коричневый
   - Перетащите на Plane

### 5. Создание врага
1. Right-click → 3D Object → Capsule
2. Переименуйте в "Enemy"
3. Цвет: красный (Material → Color)
4. Добавьте компоненты:
   - `Character Controller`
   - `EnemyAI` (Scripts → Enemies)
   - `EnemyHealth` (Scripts → Enemies)
5. Настройте `EnemyAI`:
   - Detection Range: 15
   - Attack Range: 2
   - Move Speed: 3

### 6. Создание оружия
1. Right-click на Player → Create Empty
2. Переименуйте в "WeaponHolder"
3. Right-click на WeaponHolder → 3D Object → Cube
4. Переименуйте в "AK47"
5. Масштаб: (0.1, 0.1, 0.8)
6. Позиция: (0.5, 1.2, 0.8)
7. Добавьте скрипт `WeaponSystem` (Scripts → Weapons)
8. Создайте пустой объект "MuzzlePoint" на конце ствола

### 7. Создание освещения
1. Выберите Directional Light
2. Настройте:
   - Rotation: (50, -30, 0)
   - Intensity: 1
   - Color: тёплый жёлтый

### 8. Создание временной аномалии
1. Right-click → 3D Object → Sphere
2. Переименуйте в "TimeAnomaly"
3. Масштаб: (3, 3, 3)
4. Цвет: фиолетовый
5. Добавьте компоненты:
   - `Sphere Collider` (Is Trigger: true)
   - `Light` (Type: Point, Range: 10, Color: фиолетовый)
   - `TimeAnomaly` (Scripts → Systems)
6. Создайте материал:
   - Shader: Standard
   - Rendering Mode: Transparent
   - Albedo: фиолетовый
   - Metallic: 0.5
   - Smoothness: 0.8

### 9. Настройка тегов
1. Edit → Project Settings → Tags and Layers
2. Добавьте тег:
   - "Player"
   - "Enemy"
   - "Anomaly"

### 10. Настройка физики
1. Edit → Project Settings → Physics
2. Gravity: (0, -9.81, 0)
3. Настройте Layer Collision Matrix:
   - Player vs Ground: ✓
   - Player vs Enemy: ✓
   - Enemy vs Ground: ✓

## Готовая структура сцены

```
MainScene
├── Directional Light
├── Ground (Plane)
├── Player (Capsule)
│   ├── Camera (Main Camera)
│   ├── WeaponHolder
│   │   └── AK47 (Cube)
│   │       └── MuzzlePoint (Empty)
│   └── GroundCheck (Empty)
├── Enemy (Capsule)
└── TimeAnomaly (Sphere)
```

## Клавиши управления

| Клавиша | Действие |
|---------|----------|
| WASD | Движение |
| Space | Прыжок |
| Left Shift | Бег |
| Mouse | Обзор |
| Left Click | Стрельба |
| R | Перезарядка |
| Escape | Пауза/разблокировка курсора |
