# GlobalTransLines

Sistema de gestion para una empresa de transporte internacional de cargas, desarrollado como proyecto de tesis.

## Alcance funcional

- Gestion de clientes, cargas, cotizaciones y bookings.
- Asignacion de buques y contenedores.
- Emision de Bill of Lading, manifiestos, facturas y pagos.
- Gestion de proveedores, servicios subcontratados, ordenes de compra y pagos a proveedor.
- Administracion de usuarios, roles, permisos, bitacora y backups.
- Reportes de gestion y generacion de documentos PDF.

## Arquitectura

La solucion esta organizada por capas:

- `GlobalTransLines_UI`: interfaz de usuario WinForms.
- `BE`: entidades de negocio y objetos de dominio.
- `BLL`: reglas de negocio, validaciones y orquestacion de casos de uso.
- `MPP`: persistencia XML.
- `Abstraccion`: contratos comunes para entidades, gestores, repositorios y documentos.
- `Seguridad`: utilidades de cifrado basico de credenciales.
- `Transversales`: servicios compartidos, rutas, numeracion y generacion documental.

## Ejecucion

1. Abrir `GlobalTransLines.sln` con Visual Studio 2022.
2. Compilar en configuracion `Debug` o `Release`.
3. Ejecutar el proyecto `GlobalTransLines_UI`.

El punto de entrada de la aplicacion es `FrmLogin`.

## Acceso inicial

Cuando el sistema no encuentra usuarios cargados, inicializa un usuario administrador semilla:

- Usuario: `admin`
- Clave: `1234`

Este usuario permite realizar la primera configuracion del sistema y administrar roles y permisos.

## Documentacion de tesis

El repositorio incluye material complementario de la tesis:

- `GlobalTransLines V4.1.docx`
- `GlobalTransLines.eap`
- `Diagramas/`

## Verificacion actual

La solucion fue compilada correctamente con MSBuild en configuracion `Debug|Any CPU`.
