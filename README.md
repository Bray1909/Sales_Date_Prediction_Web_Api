# Sales_Date_Prediction_Web_Api

Para iniciar este proyecto es importante seguir los pasos que estan en la carpeta documentacion, muy importante tener la conexion a la base de datos configurada


Desarrollé una aplicación full stack compuesta por un backend en .NET 8 y un frontend en Angular. En el backend, estructuré el proyecto en capas (Controllers, Services, DAL y Models), utilizando ASP.NET Core. Implementé consultas SQL directas para la comunicación con la base de datos, a través de la biblioteca Microsoft.Data.SqlClient, integradas mediante inyección de dependencias en la capa de acceso a datos (DAL). Utilicé DTOs para la transferencia de información entre capas, y desarrollé middleware personalizado para validaciones y manejo global de errores. Además, apliqué principios SOLID y patrones como Clean Architecture para asegurar un código limpio, mantenible y escalable.

En el frontend, desarrollé una SPA (Single Page Application) con Angular, donde creé servicios que consumen las APIs RESTful del backend, presentan la información de forma dinámica. Usé Angular Material para el diseño de la interfaz de usuario, y estructuré el código siguiendo buenas prácticas de modularidad y reutilización de componentes. Documenté las APIs utilizando Swagger, y administré todo el proyecto con Git para el control de versiones.