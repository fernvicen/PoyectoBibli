create database biblioteca
go

use biblioteca
go

create table autor(
id_autor int not null primary key,
nombre nvarchar(30) not null,
primer_apellido nvarchar(30) not null
)
go

create table libro(
id_libro int not null primary key,
titulo nvarchar(50) not null,
resumen nvarchar(144),
n_pagina int not null,
editorial nvarchar(30) not null,
isbn nvarchar(30) not null,
año_edicion int not null,
categoria nvarchar(30) not null,
idioma nvarchar(30) not null
)
go

create table libro_autor(
id_libro_autor int not null primary key,
id_libro int not null foreign key references libro(id_libro),
id_autor int not null foreign key references autor(id_autor)
)
go

create table direccion(
id_direccion int not null primary key,
direcion1 nvarchar(30) not null,
colonia nvarchar(30) NOT NULL,
ciudad nvarchar(30) NOT NULL,
pais nvarchar(30) NOT NULL,
codigo_postal int not null,
telefono nvarchar(20) 
)
go

create table bibliotecario(
id_bibliotecario int not null primary key,
nombre nvarchar(20) not null,
primer_apellido nvarchar(20) not null,
segundo_apellido nvarchar(20) not null,
id_direccion int not null foreign key references direccion(id_direccion),
email nvarchar(50) not null,
activo int not null,
usuario nvarchar(20) not null,
contraseña nvarchar(16) not null
)
go

create table usuario(
id_cliente int not null primary key,
nombre nvarchar(30) not null,
primer_apellido nvarchar(30) not null,
segundo_apellido nvarchar(30) not null,
id_direccion int not null foreign key references direccion(id_direccion),
email nvarchar(30) not null,
fecha_registro datetime not null
)
go

create table prestamo(
id_prestamo int not null primary key,
fecha_prestamo datetime not null,
entrega nvarchar(50) not null,
id_usuario int not null foreign key references usuario(id_cliente),
id_libro int not null foreign key references libro(id_libro)
)
go

create table inventario(
id_inventario int not null primary key,
fecha datetime not null,
id_libro int not null foreign key references libro(id_libro),
id_prestamo int not null foreign key references prestamo(id_prestamo),
id_usuario int not null foreign key references usuario(id_cliente),
id_bibliotecario int not null foreign key references bibliotecario(id_bibliotecario)
)
go

--INGRESAR BIBLIOTECARIO
CREATE PROC PROCE_BIBLIOTECARIO
@nom nvarchar(20),
@ape1 nvarchar(20),
@ape2 nvarchar(20),
@email nvarchar(20), 
@act int,
@user nvarchar(20), 
@contra nvarchar(16),
@codigo int,
@direccion nvarchar(30)
as
declare @id_direccion int=(select top(1) id_direccion
		from direccion
		where codigo_postal = @codigo and direcion1 = @direccion)
declare @id int=(select top(1)id_bibliotecario
		from bibliotecario
		order by id_bibliotecario desc)+1
IF EXISTS (select id_bibliotecario from bibliotecario where nombre =@nom and primer_apellido = @ape1 and segundo_apellido = @ape2)
BEGIN
SELECT 'Bibliotecario Existente'
END
ELSE
BEGIN
IF EXISTS (select top(1)id_bibliotecario from bibliotecario order by id_bibliotecario desc)
BEGIN
insert into bibliotecario values(@id,@nom,@ape1,@ape2,@id_direccion,@email,@act,@user,@contra)
SELECT 'Bibliotecario Registrado Correctamente'
END
ELSE
BEGIN
insert into bibliotecario values(1,@nom,@ape1,@ape2,@id_direccion,@email,@act,@user,@contra)
SELECT 'Bibliotecario Registrado Correctamente'
END
END
go

--INGRESAR direccion
create proc PROCE_DIRECCION
@direccion nvarchar(30),
@colonia nvarchar(30),
@ciudad nvarchar(30),
@pais nvarchar(30),
@codigo int,
@tele nvarchar(30)
as
declare @id_direccion int = (Select top(1)id_direccion
							from direccion	
							order by id_direccion desc)+1
IF EXISTS (Select codigo_postal from direccion	where codigo_postal = codigo_postal and ciudad=@ciudad and direcion1 =@direccion )
BEGIN
SELECT 'DIRECCION EXISTENTE'
END
ELSE
BEGIN
IF EXISTS (Select top(1)id_direccion from direccion order by id_direccion desc)
BEGIN
insert into direccion values(@id_direccion,@direccion,@colonia,@ciudad,@pais,@codigo,@tele)
END
ELSE
BEGIN
insert into direccion values(1,@direccion,@colonia,@ciudad,@pais,@codigo,@tele)
END
END
go

--INGRESAR CLIENTE
create proc PROCE_CLIENTE
@nombre nvarchar(30),
@apellido1 nvarchar(30),
@apellido2 nvarchar(30),
@email nvarchar(30),
@codigo int,
@direccion nvarchar(30)
as
declare @id_cliente int=(select top(1)id_cliente
		from usuario
		order by id_cliente desc)+1
declare @id_direccion int=(select id_direccion
		from direccion
		where codigo_postal = @codigo and direcion1 = @direccion)
declare @fecha datetime = getdate()
IF EXISTS (select id_cliente from usuario where nombre =@nombre and primer_apellido = @apellido1 and segundo_apellido = @apellido2)
BEGIN
SELECT 'Bibliotecario Existente'
END
ELSE
BEGIN
IF EXISTS (select top(1)id_cliente from usuario order by id_cliente desc)
BEGIN
insert into usuario values(@id_cliente,@nombre,@apellido1,@apellido2,@id_direccion,@email,@fecha)
SELECT 'Usuario Registrado Correctamente'
END
ELSE
BEGIN
insert into usuario values(1,@nombre,@apellido1,@apellido2,@id_direccion,@email,@fecha)
SELECT 'Usuario Registrado Correctamente'
END
END
go

--INGRESAR PRESTAMO
create proc PROCE_PRESTAMO
@title nvarchar(30), 
@isbn nvarchar(30),
@usuario nvarchar(20),
@contra nvarchar(16),
@nom_u nvarchar(30),
@ape_u nvarchar(30),
@ape2_u nvarchar(30),
@fec_dev nvarchar(30)
as
IF EXISTS (select top(1) id_prestamo from prestamo order by id_prestamo desc)
BEGIN
declare @fecha datetime = CONVERT(datetime,GETDATE())
declare @id_p int=(select top(1)id_prestamo from prestamo order by id_prestamo desc)+1
declare @id_inve int=(select top(1)id_inventario from inventario order by id_inventario desc)+1
declare @id_us int = (select top(1) id_cliente from usuario where @nom_u =nombre and @ape_u = primer_apellido and @ape2_u = segundo_apellido)
declare @id_bibli int = (Select top(1) id_bibliotecario from bibliotecario where usuario=@usuario and contraseña=@contra)
declare @id_lib int = (select top(1) id_libro from libro where titulo = @title and isbn= @isbn)
		--agregar prestamo
insert into prestamo values(@id_p,@fecha,@fec_dev,@id_us,@id_lib)
declare @id_prestamo int=(Select top(1) id_prestamo from prestamo where id_libro = @id_lib and id_usuario =@id_us)
insert into inventario values(@id_inve,@fecha,@id_lib,@id_prestamo,@id_us,@id_bibli)
SELECT 'PRESTAMO REGISTRADO'
END
ELSE
BEGIN
declare @fechas datetime = CONVERT(datetime,GETDATE())
declare @id_us1 int = (select id_cliente from usuario where @nom_u =nombre and @ape_u = primer_apellido and @ape2_u = segundo_apellido)
declare @id_bibli1 int = (Select id_bibliotecario from bibliotecario where usuario=@usuario and contraseña=@contra)
declare @id_lib1 int = (select id_libro from libro where titulo = @title and isbn= @isbn)
insert into prestamo values(1,@fechas,@fec_dev,@id_us1,@id_lib1)
insert into inventario values(1,@fechas,@id_lib1,1,@id_us1,@id_bibli1)
SELECT 'PRESTAMO REGISTRADO'
END
go


--ELIMINAR CLIENTE
create PROC ELIMINAR_CLIENTE
@nombre nvarchar(30),
@apellido nvarchar(30),
@email nvarchar(30)
as
declare @id int = (Select id_cliente
					from usuario
					 where @nombre =nombre and @apellido=primer_apellido and @email=email)
delete 
from usuario
where id_cliente = @id
SELECT 'Usuario Eliminado Correctamente'
go

--ELIMINAR BLIBLIOTECARIO
create PROC ELIMINAR_BIBLIOTECARIO
@nombre nvarchar(30),
@usuario nvarchar(20),
@contrasena nvarchar(20)
as
IF EXISTS (Select id_bibliotecario from bibliotecario where nombre=@nombre and usuario=@usuario and contraseña = @contrasena)
BEGIN
declare @id int = (Select id_bibliotecario
					from bibliotecario
					where @nombre =nombre and @contrasena=contraseña and @usuario=usuario)
delete 
from bibliotecario
where id_bibliotecario = @id
SELECT 'Bibliotecario Eliminado Correctamente'
END
ELSE
SELECT 'Bibliotecario NO EXISTE'
go

--ELIMINAR LIBRO
create proc ELIMINAR_LIBRO
@titulo nvarchar(30),
@isnb nvarchar(30)
as
IF EXISTS (select id_libro FROM libro where titulo =@titulo and isbn = @isnb)
BEGIN
declare @id int = (select id_libro FROM libro where titulo =@titulo and isbn = @isnb)
delete from libro where id_libro = @id
delete from libro_autor where id_libro = @id
delete from autor where id_autor in (select id_autor from libro_autor where id_libro=@id)
SELECT 'LIBRO ELIMINADO CORRECTAMENTE'
END
ELSE
SELECT 'LIBRO NO EXISTE'
go

--LOGIN 
CREATE PROC logi
@usu nvarchar(20),
@contra nvarchar(16)
as
IF EXISTS (SELECT usuario FROM bibliotecario WHERE usuario = @usu AND contraseña = @contra)
BEGIN
SELECT usuario FROM bibliotecario WHERE usuario = @usu AND contraseña = @contra
END
ELSE
SELECT 'USUARIO NO EXISTENTE'
go

create proc todos
as
select * from libro
go

create proc TODOS_PRESTAMO
AS
SELECT TOP(20) * FROM prestamo
GO

--INGRESAR LIBRO
create proc PROCE_LIBRO
@titulo nvarchar(50),@res nvarchar(144),
@num int,@edit nvarchar(30),
@isb nvarchar(30),@año int,
@categ nvarchar(30),@idio nvarchar(30),
@nom nvarchar(30), @ape nvarchar(30)
as
IF EXISTS (select top(1) id_libro from libro where @titulo = titulo and @isb = isbn)
BEGIN
SELECT 'Libro Existente'
END
ELSE
BEGIN
IF EXISTS (select top(1)id_libro from libro order by id_libro desc)
BEGIN
declare @id int = (select top(1)id_libro
		from libro
		order by id_libro desc)+1
declare @id_lib int = (select top(1)id_libro_autor
		from libro_autor
		order by id_libro_autor desc)+1
insert into libro values(@id,@titulo,@res,@num,@edit,@isb,@año,@categ,@idio)
declare @id_autor int = (select top(1) id_autor
						from autor
						where nombre=@nom and primer_apellido=@ape)
declare @id_libros int = (select top(1) id_libro
		from libro
		where @titulo = titulo and @isb = isbn)
insert into libro_autor values(@id_lib,@id_libros,@id_autor)
END
ELSE
BEGIN
declare @id_auto int = (select top(1) id_autor
						from autor
						where nombre=@nom and primer_apellido=@ape)
insert into libro values(1,@titulo,@res,@num,@edit,@isb,@año,@categ,@idio)
insert into libro_autor values(1,1,@id_auto)
END
END
go

--INGRESAR AUTOR
create proc PROCE_AUTOR
@nom nvarchar(30), @ape nvarchar(30)
as
IF EXISTS (SELECT id_autor FROM autor WHERE nombre = @nom AND primer_apellido = @ape)
BEGIN
SELECT 'AUTOR EXISTENTE'
END
ELSE
BEGIN
IF EXISTS (select top(1)id_autor from autor order by id_autor desc)
BEGIN
declare @id int=(select top(1)id_autor from autor order by id_autor desc)+1
insert into autor values(@id,@nom,@ape)
END
ELSE
BEGIN
insert into autor values(1,@nom,@ape)
END
END
go

--ACTUALIZAR USUARIO
create PROC ACTUALIZAR_USUARIO
@nom nvarchar(30),@ape1 nvarchar(30),
@ape2 nvarchar(30),@email nvarchar(30)
as
declare @id int = (select id_cliente from usuario where @nom =nombre and @ape1=primer_apellido and @ape2=segundo_apellido)
declare @id_di int=(select id_direccion from direccion where id_direccion in (select id_direccion from usuario where nombre=@nom and @ape1=primer_apellido and segundo_apellido=@ape2))
declare @fecha datetime = getdate()
update usuario 
set id_cliente = @id,
	nombre = @nom,
	primer_apellido = @ape1,
	segundo_apellido = @ape2,
	id_direccion = @id_di,
	email = @email,
	fecha_registro = @fecha
where id_cliente = @id
go

create proc administrador
as
select * from bibliotecario
go

create proc cliente
as
select * from usuario
go

create proc topprestamos
as
select nombre,primer_apellido,segundo_apellido,libro.titulo,fecha_prestamo
from prestamo,libro,usuario
WHERE prestamo.id_libro =libro.id_libro
and prestamo.id_usuario = usuario.id_cliente
go
exec PROCE_DIRECCION '8080','8080','puebla','mexico',8080,'192.168.1.0'
exec PROCE_BIBLIOTECARIO 'admin','admin','admin', 'admin@gmail.com',1,'admin','1234',8080,'8080'
go
--ACTUALIZAR BIBLIOTECARIO
CREATE PROC ACTUALIZAR_BIBLIOTECARIO
@nom nvarchar(30),@ape1 nvarchar(30),
@ape2 int,@email nvarchar(30),
@active int,@us nvarchar(20),
@contr nvarchar(16)
as
declare @id int = (select top(1) id_bibliotecario
					from bibliotecario
					where @nom =nombre
					and @ape1=primer_apellido
					and @ape2=segundo_apellido)
declare @id_di int=(select top(1) id_direccion
		from direccion
		where id_direccion in (select id_direccion
								from bibliotecario
								where nombre=@nom and @ape1=primer_apellido
								and segundo_apellido=@ape2))
update bibliotecario
set id_bibliotecario = @id,
	nombre = @nom,
	primer_apellido = @ape1,
	segundo_apellido = @ape2,
	id_direccion = @id_di,
	email=@email,
	activo=@active,
	usuario=@us,
	contraseña=@contr
	where id_bibliotecario=@id
go

--BUSCAR LIBROS
CREATE PROC BUSCAR_LIBRO
@ISBN NVARCHAR(30)
AS
SELECT titulo AS NOMBRE_LIBRO,isbn,nombre as AUTOR_NOMBRE,primer_apellido AS AUTOR_APELLIDO
FROM libro JOIN libro_autor ON libro.id_libro =libro_autor.id_libro JOIN autor ON autor.id_autor =libro_autor.id_autor
WHERE isbn=@ISBN
GO

--BUSCAR LIBROS
CREATE PROC BUSCAR_LIBRO2
@ISBN NVARCHAR(30)
AS
SELECT titulo AS NOMBRE_LIBRO,isbn,nombre as AUTOR_NOMBRE,primer_apellido AS AUTOR_APELLIDO
FROM libro JOIN libro_autor ON libro.id_libro =libro_autor.id_libro JOIN autor ON autor.id_autor =libro_autor.id_autor
WHERE (@ISBN) like (isbn+'%')
GO