#  Proyecto Final Unity - Escape Room 3D

![Unity](https://img.shields.io/badge/Unity-2022%2B-black?style=flat&logo=unity)
![C#](https://img.shields.io/badge/C%23-Programming-blue?style=flat&logo=c-sharp)

Un juego 3D interactivo desarrollado en Unity donde el jugador tiene el objetivo de escapar de un escenario cerrado antes de que acabe el tiempo. Para lograrlo, deberá explorar el entorno, recolectar pistas, interactuar con NPCs y descifrar la contraseña de una cerradura digital.

---

##  Características Principales (Core Features)

###  Flujo de Juego Completo (Core Loop)
Transiciones fluidas entre escenas. Comienza con una cinemática de introducción, Menú Principal, Gameplay y desenlaces de Victoria o Derrota.

###  Movimiento Físico del Jugador
Sistema de movimiento controlando un Rigidbody usando el nuevo Input System de Unity. Incluye mecánicas para caminar, correr (sprint) y saltar.

###  Sistema de Tiempo (Game Timer)
Un temporizador regresivo añade tensión al juego. Si el tiempo llega a cero, el jugador pierde y es enviado a la escena de Derrota.

###  Mecánicas de Interacción
- Exploración de pistas en el entorno  
- Puertas interactivas con sistema de rotación fluida (`Quaternion.Slerp`)  
- Teclado numérico 3D funcional (`CodeLock3D`) para introducir la contraseña  
- Objetos interactivos como escritorios y elementos que reaccionan al jugador  

###  NPCs e Inteligencia Artificial
- Helpers interactivos que giran hacia el jugador y proveen diálogos de misiones  
- NPCs con patrullaje o movimiento autónomo mediante `NavMeshAgent`  

---

##  Controles

La configuración de controles utiliza el nuevo Input System de Unity:

| Acción                         | Tecla / Control        |
|--------------------------------|------------------------|
| Moverse                        | W, A, S, D            |
| Mirar / Cámara                | Ratón (Mouse)         |
| Saltar                        | Espacio / Q           |
| Correr (Sprint)               | Shift (Mantener)      |
| Interactuar (Puertas/Teclado) | E / Clic Izquierdo    |
| Ver Pistas                    | X                     |

---

##  Arquitectura del Proyecto

El código está estructurado de manera modular en `Assets/_Scripts`:

###  Jugador
- `Player.cs` (Movimiento y físicas)  
- `PlayerLook.cs` (Control de cámara)  

###  Lógica Base
- `GameTimer.cs` (Gestión del tiempo y derrota)  
- `CodeLock3D.cs`, `KeypadButton.cs` (Sistema de contraseñas)  
- `VolverAJugar.cs`, `JugarAhora.cs` (Gestión de escenas)  

###  Entorno Interactivo
- `InteractiveDoor.cs` (Puertas)  
- `Clue.cs` (Pistas)  
- `EscritorioInteractivo.cs` (Muebles interactivos)  

###  NPCs
- `Helper.cs` (Misiones y rotación dinámica)  
- `Npc.cs` (Navegación inteligente)  

###  UI / Cinemáticas
- `IntroVideo.cs`  
- `MensajeInicial.cs`  

---

##  Estructura de Escenas

1. **IntroScene**: Cinemática de inicio del juego  
2. **PrincipalScene**: Menú principal con opciones para jugar y ajustes  
3. **All (Game Scene)**: Nivel principal con geometría, colliders, iluminación y mecánicas  
4. **WinScene**: Se carga al descifrar el código y escapar a tiempo  
5. **LoseScene**: Se carga si se agota el temporizador  

---

##  Instalación y Uso

1. Clona el repositorio:
   ```bash
   git clone https://github.com/ValeriaMartinezSoria/ProyectoFinalUnity.git
