# ProyectoCeremonial

Juego 3D hecho en Unity donde controlas al jugador para recolectar **Red Pills** y **Blue Pills** mientras evitas a un enemigo que te persigue.

## Características

- Movimiento del jugador con el **Input System** de Unity.
- Cámara con seguimiento al jugador.
- Enemigo con navegación usando **NavMeshAgent**.
- Coleccionables rotatorios (Red/Blue Pills).
- UI con contadores y mensajes de estado.
- Fin de juego por colisión con enemigo o al recolectar 8 píldoras.

## Controles

- **Teclado:** `W/A/S/D` y flechas.
- **Gamepad:** stick izquierdo.

## Requisitos

- **Unity 2022.3.23f1** (LTS).

## Cómo ejecutar

1. Abrir el proyecto desde Unity Hub.
2. Usar Unity `2022.3.23f1`.
3. Abrir la escena principal en:
   - `Assets/Scenes/NivelCero.unity`
4. Presionar **Play** en el Editor.

## Estructura principal

- `Assets/Scripts/PlayerController.cs`: movimiento, recolección y lógica de fin de juego.
- `Assets/Scripts/EnemyMovement.cs`: persecución del jugador con NavMesh.
- `Assets/Scripts/CameraController.cs`: seguimiento de cámara.
- `Assets/Scripts/Rotator.cs`: rotación de coleccionables.
- `Assets/Prefabs/`: prefabs de píldoras y obstáculos.
- `Assets/Scenes/`: escenas del juego.

## Tecnologías

- Unity (URP)
- C#
- Unity Input System
- Unity AI Navigation (NavMesh)
