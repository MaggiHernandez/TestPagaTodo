# MinimalApi
Este proyecto se creo un api vacia en .net 8 / visual studio 2022 y contiene un end point para generar una multiplicación de los valores introducidos
para ejecutarla se selecciona el proyecto como elemento de inicio, se ejecuta y se añade el endpoint y parámetros sobre la url
https://localhost:{puerto}/multiplicar?a=2&b=9

se necesita tener instalado .net 8 para su ejecución


# CRUDExa
Este proyecto es de tipo asp.netcore generado en versión net.8 / visual studio 2022, con patrón repositorio que consta de:
-api
	-contiene los paquetes necesarios, la configuración de la base de datos en el appsettings, control de errores middlewares, controladores y program
-dataaccess
	-contiene el contexto, el contenedor de trabajo, el repositorio, interfases y clases con los métodos para el CRUD
-modelos
	-contiene los modelos creados por el scaffold
-utilidades
	-contiene clases genéricas de utilidad

esta conectado a una base de datos llamada "empleados" realizada en sqlserver 2020

para la ejecución de esta api se configuro el swagger para ejecutar cada uno de los endpoint
para hacer uso de ellos se debe;
-tener instalado sqlserver 2022
-montar el backup de la base de datos "empleado"
-tener instalado .net 8
-cambiar la cadena de conexión a la base de datos en el appsettings dependiendo la configuración del montaje de la base
-seleccionar el proyecto como elemento de inicio
