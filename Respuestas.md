### Bloque A - Arquitectura

**1.** ¿Por qué separás el código en capas? Contá brevemente cómo organizaste el backend y qué ganás con esa separación.

Separo el código en capas para mantener separadas las responsabilidades y tener una arquitectura limpia.
En mi backend lo separe en 4 capas
 - Api: encargada de las requests HTTP y sus respuestas
 - Application: Contiene la lógica de negocio
 - Domain: Define las entidades y reglas
 - Infrastructure: Se comunica con la Base de datos

Cada una de estas tiene una tarea bien definida lo que facilita el mantenimiento y la escalabilidad, además de que cualquier cambio de tecnología lo hace mas transparente.

**2.** ¿Qué es el patrón Repository? ¿Qué ventaja te da y cuándo podría ser sobreingeniería usarlo?

Es una capa entre la lógica de negocio y el acceso de datos, evitando que el negocio dependa de EF o el motor de base de datos usado.
La ventaja que da esto es la facilidad de cambiar la forma de persistencia, la sobreingeniería seria si esta capa no aporta nada nuevo o el proyecto es demasiado chico, en esos casos EF cumple ese rol

### Bloque B — Autenticación

**3.** Explicá cómo funciona JWT: ¿qué contiene un token y por qué no hace falta ir a la base de datos en cada request?

Un token de JWT contiene información cifrada que solo el servidor que la firma puede decodificar, esto hace que no haga falta ir a la bd ya que el mismo servidor lo valida

**4.** ¿Dónde guardás el JWT en el frontend y por qué? ¿Qué riesgo tiene esa elección?

se puede guardar en un localstorage, pero el riesgo de esto seria la vulnerabilidad a XSS

otra opción y mas segura seria guardarlo como una cookie httpOnly, pero estoy requiere protección contra CSRF


### Bloque C — Angular

**7.** ¿Qué es un componente Standalone en Angular 18 y en qué se diferencia del enfoque anterior con NgModules?

es un componente independiente que no necesita estar declarado en un NgModule, la diferencia esta en la simplicidad de la arquitectura al tener componentes standalone, pudiendo ser consumido desde cualquier otro componente

**8.** ¿Qué es un Guard en Angular y cómo lo usaste para proteger las rutas que requieren login?

es un servicio que controla el acceso a las rutas, lo use en mi routes para que valide en cada acceso a estas si el usuario esta correctamente logueado

**9.** ¿Cuál es la diferencia entre un `Service` y un `Component`?
un "Service" maneja lógica y/o comunicacion con APIs el "Component" Renderiza el front y maneja las interacciones con el usuario

### Bloque D — Multi-tenancy

**10.** Investigá qué es una arquitectura multi-tenant. Explicá los distintos enfoques para implementarla (base de datos separada por tenant, esquema separado, discriminador en la misma tabla) y cuáles son las ventajas y desventajas de cada uno.

una arquitectura multi-tenant contiene varios usuarios aislados entre si.
los distintos enfoques serian:

- Base por tenant (o cliente):
Cada tenant tiene su propia BD, esto da muy buena aislación pero dificulta la escalabilidad

- Esquema por tenant:
usando Postgre se separan los tenant, seria una separación lógica. Aunque es complejo de mantener

- Misma tabla con TenantId
todo esta en una misma BD y se diferencian por un TenantId. Es bastante fácil de escalar pero tambien riesgoso de filtrar mal los datos entre clientes

**11.** Si tuviera que aplicar multi-tenancy a la aplicación que construiste, ¿qué cambiarías? ¿Dónde en el código impactaría más ese cambio y por qué?
al ser un ejemplo de task separadas por equipo aplique  un enfoque separándolos por equipoId. Esto impacta mas en el backend ya que en las llamadas valida el id con el token y obtiene los datos relevantes

Si tuvieras que aplicar multi-tenancy a la aplicación que construiste, ¿qué cambiarías? ¿Dónde en el código impactaría más ese cambio y por qué?

Al ser un ejemplo de tareas separadas por equipo, apliqué un enfoque separándolos por equipoId. Esto impacta más en el backend, ya que en las llamadas valida el id con el token y obtiene los datos relevantes.
