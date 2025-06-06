Instrucciones para probar el repositorio:

NOTA: en caso de que no se instalen los paquetes de manera automática, usar el comando DOTNET BUILD y de esta forma se instalan las dependencias y ejecuta el proyecto automáticamente.


1- Clonar el repositorio con : git clone https://github.com/im-robert/BHD-Ejercicio-Integracon.git

2- En el archivo appsettings.Development.json cambiar el ConnectionString y modificarlo con su servidor en SQLSERVER, ejemplo:  "ConnectionStrings": {
   "DefaultConnection": "Server=[AQUI COLOCAR SERVIDOR DE SU MAQUINA];Database=BhdBankDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"

3- En la consola de administrador de paquetes en VS Code, eescribir los siguientes comandos: add-migration [Comentario de eleccion]
                                                                                             update-database

4- Ejecutar la aplicación para empezar a probar y validar tanto el endpoint de agregar usuario como el de buscar todos los usuarios.

5- Luego de validar que funciona el endpoint, si se desea se puede verificar en la base de datos.


NOTA: 
Se usó .NET 8 y SQL Server para esta prueba.


Foto del endpoint en funcionamiento:
![image](https://github.com/user-attachments/assets/7aad9d79-8564-4e2d-8a11-aeed80347885)


Diagrama de la solución:
![Diagrama de la solucion proyecto-BHD](https://github.com/user-attachments/assets/ec0a34ba-00e4-48a2-ac25-31c52fd180d8)
