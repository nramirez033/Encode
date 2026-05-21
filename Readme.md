# 📌 Task Manager Fullstack (Angular + .NET 8 + PostgreSQL)

Proyecto fullstack con backend en **.NET 8**, frontend en **Angular** y base de datos **PostgreSQL**, todo orquestado con Docker.

---

# 🚀 Cómo correr el proyecto con Docker

## 📦 Requisitos previos
- Docker instalado y corriendo
- Docker Compose

---

## ▶️ Levantar todo el sistema

Desde la raíz del proyecto (donde está `docker-compose.yml`):

```bash
docker compose up --build




🌐 Servicios
💻 Frontend Angular
http://localhost:4200

🔌 API .NET (Swagger)
http://localhost:5000/swagger

🐘 PostgreSQL
localhost:5432


🛑 Detener el proyecto
    docker compose down

🧨 Borrar base de datos (reset completo)
    docker compose down -v


👤 Usuario de prueba (SEED)
Admin - Equipo1
    Email: admin@test.com
    Password: 123456

Admin 2 - Equipo2
    Email: "admin2@test.com",
    Password: 123456,


Endpoints principales   
 Auth
    POST /auth/register
    POST /auth/login
    GET /auth/me (requiere JWT)
 Tasks
    GET /tasks
    POST /tasks
    PUT /tasks/{id}/status
    DELETE /tasks/{id} (solo creador)


Decisiones
 Arquitectura en capas (API, Application, Domain, Infrastructure) para separar responsabilidades y mantener una arquitectura limpia.
 Simplificación del modelo de usuarios usando Equipo como string en lugar de una entidad separada, reduciendo complejidad y acelerando el MVP.