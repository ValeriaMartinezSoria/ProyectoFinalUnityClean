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
   ```

2. Abre **Unity Hub** y agrega el proyecto:
   - Click en *Open* / *Add project from disk*
   - Selecciona la carpeta `ProyectoFinalUnity`
   - Asegúrate de tener instalado **Unity 2022.3 LTS** o superior

3. Espera a que Unity importe los assets (la primera vez puede tardar varios minutos).

4. En el editor, abre la escena inicial:
   ```
   Assets/_Scenes/IntroScene.unity
   ```

5. Pulsa **Play** ▶ para iniciar el juego.

> **Nota:** Si al abrir el proyecto aparecen errores de paquetes, ve a *Window → Package Manager* y verifica que estén instalados:
> - Input System
> - TextMesh Pro
> - AI Navigation (NavMesh)
> - Universal RP (URP)

---

##  Minijuegos Incluidos

El gameplay se sustenta sobre **5 puzzles principales** que el jugador debe resolver dentro del tiempo límite:

###  1. Cerradura Digital (CodeLock3D)
Teclado numérico 3D donde se introduce el código de **4 dígitos (2580)**. Los botones `C` (Clear) y `E` (Enter) controlan el flujo. Al acertar abre la puerta principal y desbloquea la siguiente zona.

###  2. Recolección de Anomalías
Hay **10 objetos anómalos** distribuidos por el aula. Al hacer clic en todos, una pared desaparece para dar paso al siguiente nivel. Cada anomalía recoge actualiza el contador en pantalla.

###  3. Organizar Oficina
El jugador hace clic en **5 objetos desordenados** dentro de la oficina. Al completarlos, la pared se desbloquea y aparece el modelo de oficina ya organizada.

###  4. Conexión de Cables
Mini puzzle de conectar cables del **mismo color** (rojo, azul, amarillo). El sistema dibuja una línea animada con `LineRenderer` entre los nodos correctamente conectados. Requiere 3 conexiones correctas.

###  5. Juegos de Letras de la UPB
- **Juego DTI:** ordenar las letras `D → T → I` en el orden correcto (sigla de la sala de tecnología).
- **Juego UPB:** estilo *Simon dice* con 3 niveles. La secuencia comienza con 2 letras y crece hasta 4. Si fallas, repites el nivel.

---

##  NPCs e Inteligencia Artificial

El juego cuenta con **4 tipos de NPCs**, todos heredan de la clase abstracta `NpcBase.cs`:

| Tipo NPC | Comportamiento | Tecnología |
|----------|----------------|------------|
| `Helper` | Da diálogos de misión, gira hacia el jugador con `Quaternion.Slerp` | Animator + Audio |
| `NpcDance` | Máquina de estados: caminar → calentar → bailar | NavMeshAgent |
| `NpcHurried` | Alterna entre correr (5 m/s) y hablar por teléfono (1.5 m/s) | NavMeshAgent |
| `NpcTalkAndWalk` | Pasea y conversa con otros NPCs cuando se cruzan | NavMeshAgent + OverlapSphere |

---

##  Lista Completa de Scripts

###  Player (`Assets/_Scripts/Player`)
- `Player.cs` — Movimiento con Rigidbody e Input System
- `PlayerLook.cs` — Cámara en primera persona y raycast de highlight
- `HighlightOnLook.cs` — Cambia el color del material al ser mirado
- `Focus.cs` — Trigger que enfoca la cámara en un punto específico

###  Flujo de Juego (`Assets/_Scripts/Flujo`)
- `GameTimer.cs` — Temporizador regresivo de 180 s
- `IntroVideo.cs` — Reproduce video de intro y carga menú
- `IntroHistoria.cs` — Texto tipo "máquina de escribir"
- `MensajeInicial.cs` — Mensaje "EL TIEMPO CORRE..."
- `MensajeMisionCafe.cs` — Mensaje al desbloquear cafetería
- `MensajeMisionColiseo.cs` — Mensaje al desbloquear coliseo
- `JugarAhora.cs` — Botón del menú principal
- `VolverAJugar.cs` — Botón de reinicio en Win/Lose
- `Pausemanager.cs` — Pausa con `Esc` y `Time.timeScale`
- `FinalButton.cs` — Botón final que detiene el timer
- `TrophyInteract.cs` — Trofeo que activa el video final
- `FinalSequenceManager.cs` — Cinemática final (estante + proyector)

###  NPCs (`Assets/_Scripts/NPCs`)
- `NpcBase.cs` — Clase abstracta padre
- `Helper.cs` — NPC que da diálogos de misión
- `NpcDance.cs` — NPC con máquina de estados Walk/Warm/Dance
- `NpcHurried.cs` — NPC apurado con teléfono
- `NpcTalkAndWalk.cs` — NPC que conversa con otros NPCs

###  Minijuego DTI (`Assets/_Scripts/JuegoDTI`)
- `OrderLettersGame.cs` — Lógica de ordenar D-T-I
- `LetterButton.cs` — Letra individual clicable
- `OrderLettersTrigger.cs` — Trigger que activa el panel

###  Minijuego UPB (`Assets/_Scripts/JuegoUPB`)
- `MemoryGame.cs` — Juego "Simon dice" con 3 niveles
- `MemoryGameTrigger.cs` — Trigger que activa el juego
- `MemoryLetter.cs` — Letra que se enciende/apaga con emisión

###  Interacción (`Assets/_Scripts`)
- `AnomalyManager.cs` — Singleton que cuenta las 10 anomalías
- `AnomalyInteract.cs` — Anomalía individual clicable
- `CodeLock3D.cs` — Cerradura digital con código 2580
- `KeypadButton.cs` — Botón individual del teclado
- `InteractiveDoor.cs` — Puerta con apertura suave (Slerp)
- `Clue.cs` — Sistema de pistas con tecla X
- `EscritorioInteractivo.cs` — Escritorio que gira 180°
- `OrganizeOfficeManager.cs` — Manager del puzzle de oficina
- `OrganizeObjectClick.cs` — Objeto a ordenar
- `CableManager.cs` — Manager del minijuego de cables
- `CableNode.cs` — Nodo de cable individual

---

##  Patrones de Diseño Utilizados

- **Singleton:** `AnomalyManager`, `OrganizeOfficeManager`, `CableManager`, `Pausemanager`, `FinalSequenceManager`
- **Herencia / Polimorfismo:** `NpcBase` (abstracta) → `Helper`, `NpcDance`, `NpcHurried`, `NpcTalkAndWalk`
- **Máquinas de Estados (FSM):** `NpcDance` (Walking/WarmingUp/Dancing), `NpcHurried` (Running/PhoneTalking)
- **Eventos:** `VideoPlayer.loopPointReached` para detectar fin de videos
- **Corrutinas:** efectos de tipeo, animaciones de cables, secuencias del juego de memoria, cinemática final
- **Single Responsibility Principle:** cada script tiene una sola responsabilidad bien definida

---

##  Tecnologías y APIs de Unity

- **Unity Input System** (nuevo) – acciones configurables (W/A/S/D, E, X, Esc, Mouse)
- **Rigidbody / Physics** – movimiento del jugador, colisiones, triggers
- **NavMesh + NavMeshAgent** – pathfinding automático de NPCs
- **Animator** – parámetros: `Speed`, `IsTalking`, `IsDancing`, `IsWarmingUp`, `IsPhoneTalking`, `IsFocusing`
- **TextMesh Pro** – todos los textos UI y 3D
- **AudioSource / AudioClip** – sonidos 2D y 3D, `PlayClipAtPoint` para sonidos puntuales
- **VideoPlayer** – cinemáticas de intro, victoria y derrota
- **LineRenderer** – cables visuales animados
- **SceneManager** – transiciones entre escenas
- **Quaternion.Slerp / Vector3.Lerp / MoveTowards** – interpolaciones suaves
- **Raycast** – detección de objetos bajo el cursor
- **URP (Universal Render Pipeline)** – iluminación y materiales

---

##  Audio del Proyecto

| Categoría | Audios incluidos |
|-----------|------------------|
| Música ambiental | `fondo_ambiental.mp3`, `absolutesound-suspense-tension.mp3` |
| Acciones | `boton_numero.mp3`, `tecla.mp3`, `correcto.mp3`, `incorrecto.mp3` |
| NPCs | `Baile1.mp3`, `Baile2.mp3`, `Llamada.mp3`, `dialogo.mp3` |
| Puzzles | `orden.mp3`, `mioficinaordenada.mp3`, `cuadros.mp3`, `vayacuadros.mp3` |
| Recoger / interactuar | `sonido_recoger.mp3`, `puerta metalica.mp3`, `tesoro_oculto.mp3` |
| Otros | `beep.mp3`, `oohmioficina.mp3`, `purple....mp3` |

---

##  Estructura del Proyecto

```
ProyectoFinalUnity/
├── Assets/
│   ├── _Scenes/              # Todas las escenas del juego
│   │   ├── IntroScene.unity
│   │   ├── PrincipalScene.unity
│   │   ├── All.unity
│   │   ├── Cafeteria.unity
│   │   ├── Nivel3_Coliseo.unity
│   │   ├── WinScene.unity
│   │   └── LoseScene.unity
│   ├── _Scripts/             # Lógica del juego en C#
│   │   ├── Player/
│   │   ├── Flujo/
│   │   ├── NPCs/
│   │   ├── JuegoDTI/
│   │   └── JuegoUPB/
│   ├── _Prefabs/             # Prefabs reutilizables
│   │   ├── Furniture/
│   │   └── People/
│   ├── _Materials/           # Materiales propios
│   ├── _Audio/               # Música y efectos de sonido
│   ├── _Animator/            # Animator Controllers
│   ├── _Images/              # Texturas e imágenes UI
│   ├── Brick Project Studio/ # Asset pack: ambientación
│   ├── DenysAlmaral/         # Asset pack: NPCs animados
│   ├── Food Pack-Demo/       # Asset pack: cafetería
│   ├── LeartesStudios/       # Asset pack: ambientes
│   ├── LowPolyOfficeProps_LITE/ # Asset pack: oficina
│   └── school/               # Modelo principal de la escuela
├── ProjectSettings/
├── Packages/
└── README.md
```

---

##  Flujo de Juego (Progresión)

1. **IntroScene** – Cinemática de inicio con video y texto narrativo
2. **PrincipalScene** – Menú principal, botón "Jugar Ahora"
3. **All** – Escena principal de gameplay (inicia el timer de 180 s)
4. El jugador explora, recoge **pistas** (`X`), habla con **Helpers** (`E`)
5. Resuelve los puzzles: anomalías → cerradura `2580` → oficina → cables → letras DTI → memoria UPB
6. Encuentra el **botón final** que sube el estante
7. Hace clic en el **trofeo (búho)** → se reproduce el video del proyector
8. **WinScene** si terminó a tiempo / **LoseScene** si se acabó el timer

---

##  Equipo y Créditos

**Universidad Privada Boliviana – UPB**
Asignatura: Interacción y Diseño de Experiencia / Desarrollo de Videojuegos

Desarrollado por estudiantes de la UPB como Proyecto Final.

### Asset Packs utilizados (terceros)
- Brick Project Studio
- Denys Almaral (NPCs)
- Food Pack-Demo
- Horse Statue
- Leartes Studios
- LowPoly Office Props LITE
- nappin
- school (modelo base)
- TextMesh Pro (Unity)

---

##  Licencia

Proyecto académico desarrollado con fines educativos para la Universidad Privada Boliviana.
Los assets de terceros están sujetos a sus respectivas licencias originales.

---

##  Contacto

Repositorio del proyecto: [github.com/ValeriaMartinezSoria/ProyectoFinalUnity](https://github.com/ValeriaMartinezSoria/ProyectoFinalUnity)
