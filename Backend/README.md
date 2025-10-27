# Introducción
Este template ha sido desarrollado en .NET 9 y está diseñado específicamente para la empresa **Ofima**. Sigue los principios de la **arquitectura hexagonal**, implementando diversos **patrones de diseño** como:
- **Mediador**
- **Repositorio**
- **DTO (Objeto de Transferencia de Datos)**
- **Fábrica**

## Estructura del Proyecto
La estructura del proyecto está organizada en capas bien definidas:

### Domain
Contiene exclusivamente la lógica de negocio.

### Application
Encapsula los casos de uso del sistema y la orquestación de componentes.

### Infrastructure
Implementa repositorios y gestiona la conexión con bases de datos como **MongoDB** o **PostgreSQL**.

### Presentation
Incluye los **middlewares**, la configuración de la API y los **controladores**.
---
Este template sirve como base para desarrollar microservicios robustos y mantenibles, aplicando buenas prácticas de diseño de software.