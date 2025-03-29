# Sistema de Gestión de Usuarios

Este proyecto es una aplicación web para la gestión de usuarios construida con .NET 6 en el backend y Vue 3 en el frontend. La aplicación permite realizar operaciones CRUD, clasificación de usuarios según su último acceso, y cálculo de puntaje en base a criterios definidos.

## Tecnologías utilizadas

- .NET 6 (C#)
- Entity Framework Core
- Vue 3
- Bootstrap 5
- Axios
- SQL Server

## Estructura del Proyecto

/backend     → API REST desarrollada en .NET 6  
/frontend    → Aplicación SPA desarrollada con Vue 3

## Configuración del entorno

### Backend (.NET)

1. Asegúrese de tener SQL Server instalado y en funcionamiento.
2. Configure la cadena de conexión en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=LIZCANO;Database=NombreDeTuBD;Trusted_Connection=True;"
}
Ejecute las migraciones con:
- dotnet ef database update
Inicie el servidor:
- dotnet run
  -La API estará disponible en http://localhost:5020.

Frontend (Vue 3)
Instale las dependencias:


- npm install
- Ejecute el servidor de desarrollo:


- npm run dev
- La aplicación estará disponible en http://localhost:8080.

Funcionalidades principales
Autenticación por correo .

- Registro, edición y eliminación de usuarios.

- Clasificación automática según última fecha de acceso:

- Menos de 12 horas: Hechicero

- Menos de 48 horas: Luchador

- Hasta 7 días: Explorador

- Más de 7 días: Olvidado

Cálculo de puntaje basado en:

- Longitud del nombre completo

- Dominio del correo electrónico

- Interfaz responsiva con diseño moderno

- Filtros y acciones desde tablas y modales


El código está organizado bajo el patrón MVC para mantener la separación de responsabilidades.