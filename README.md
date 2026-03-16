# SneakRush

Juego copia de un jueguito de friv

### Enlace del video: 
https://youtu.be/qNrtTjbpsJs


## Objetivo 

Tomar todos los objetos antes que el tiempo se acabe

## Controles
- **Mouse** – Click a los objetos que están más arriba en la pila

## PickUps implementados

Se implementaron 5 tipos diferentes de PickUps:
- Moneda
- Rubí
- Diamante
- Gargola
- Esmeralda

Cada uno tiene:
- Representación visual (sprite)
- ScriptableObject propio
- Ícono y nombre
- Peso de aparición dentro del cofre

**Los PickUps aparecen de forma aleatoria dentro del área del cofre usando un sistema de spawn con probabilidad (weight).**


Sistema de PickUps: Cada PickUp funciona con un sistema basado en Eventos y Datos.
- Cuando el jugador hace click sobre un objeto:
- Se dispara un evento OnPickupCollected
- Se reproduce un sonido de recolección
- Se actualiza el Score o Money
- Se agrega el ícono y nombre del objeto al inventario visual
- Los objetos que representan dinero y puntos no se almacenan en el inventario, sino que modifican en el score o el dinero del jugador.


## Tecnologías 
- Unity
- C#

