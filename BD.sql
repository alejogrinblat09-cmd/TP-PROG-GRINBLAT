CREATE DATABASE ApoyoEscolar;
GO

USE ApoyoEscolar;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(50) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL
);

CREATE TABLE Materias (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Tutores (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500) NOT NULL,
    Imagen VARCHAR(200) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1
);

CREATE TABLE TutoresMaterias (
    IdTutor INT NOT NULL FOREIGN KEY REFERENCES Tutores(Id),
    IdMateria INT NOT NULL FOREIGN KEY REFERENCES Materias(Id),
    PRIMARY KEY (IdTutor, IdMateria)
);

CREATE TABLE ClasesApoyo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdTutor INT NOT NULL FOREIGN KEY REFERENCES Tutores(Id),
    IdMateria INT NOT NULL FOREIGN KEY REFERENCES Materias(Id),
    FechaHora DATETIME NOT NULL,
    Cupo INT NOT NULL
);

CREATE TABLE Inscripciones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdClaseApoyo INT NOT NULL FOREIGN KEY REFERENCES ClasesApoyo(Id),
    IdUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuarios(Id),
    FechaInscripcion DATETIME NOT NULL,
    Cancelado BIT NOT NULL DEFAULT 0,
    FechaCancelacion DATETIME NULL
);
GO

INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido) VALUES
('alejo', '1234', 'Alejo', 'Grinblat'),
('sofi', '1234', 'Sofía', 'Martínez'),
('tomi', '1234', 'Tomás', 'Pérez');

INSERT INTO Materias (Nombre) VALUES
('Matemática'),
('Física'),
('Química'),
('Historia'),
('Lengua'),
('Inglés'),
('Biología'),
('Programación');

INSERT INTO Tutores (Nombre, Descripcion, Imagen, Activo) VALUES
('Lucía Fernández', 'Estudiante de 6to año. Explica paso a paso y con muchos ejercicios.', 'tutor1.jpg', 1),
('Martín Gómez', 'Le encantan los números y los problemas difíciles.', 'tutor2.jpg', 1),
('Valentina López', 'Ayuda a preparar pruebas y trabajos prácticos.', 'tutor3.jpg', 1),
('Joaquín Díaz', 'Ganó la olimpíada de física del año pasado.', 'tutor4.jpg', 1),
('Camila Romero', 'Explica química con ejemplos de la vida diaria.', 'tutor5.jpg', 1),
('Mateo Sosa', 'Fanático de la historia argentina.', 'tutor6.jpg', 1),
('Martina Álvarez', 'Ayuda con comprensión de textos y redacción.', 'tutor7.jpg', 1),
('Benjamín Torres', 'Vivió un año en Estados Unidos, habla inglés fluido.', 'tutor8.jpg', 1),
('Julieta Ruiz', 'Quiere estudiar medicina y ama la biología.', 'tutor9.jpg', 1),
('Santiago Herrera', 'Programa en C# y JavaScript desde los 12 años.', 'tutor10.jpg', 1),
('Catalina Medina', 'Paciente y clara, ideal para empezar de cero.', 'tutor11.jpg', 1),
('Felipe Castro', 'Prepara para exámenes integradores.', 'tutor12.jpg', 1),
('Emilia Vargas', 'Resume los temas en esquemas fáciles de estudiar.', 'tutor13.jpg', 1),
('Lautaro Molina', 'Explica física con experimentos caseros.', 'tutor14.jpg', 1),
('Agustina Rojas', 'Ayuda con orales y exposiciones.', 'tutor15.jpg', 1),
('Thiago Ortiz', 'Le gusta enseñar con juegos y desafíos.', 'tutor16.jpg', 1),
('Mía Silva', 'Corrige escritos y ayuda con ortografía.', 'tutor17.jpg', 1),
('Bautista Núñez', 'Explica álgebra y funciones de forma simple.', 'tutor18.jpg', 1),
('Renata Acosta', 'Prepara para exámenes internacionales de inglés.', 'tutor19.jpg', 1),
('Nicolás Benítez', 'Ayuda con bases de datos y SQL.', 'tutor20.jpg', 1),
('Paula Giménez', 'Por ahora no está dando clases.', 'tutor21.jpg', 0);

INSERT INTO TutoresMaterias (IdTutor, IdMateria) VALUES
(1, 1), (1, 2),
(2, 1),
(3, 4), (3, 5),
(4, 2), (4, 1),
(5, 3), (5, 7),
(6, 4),
(7, 5),
(8, 6),
(9, 7), (9, 3),
(10, 8), (10, 1),
(11, 1), (11, 5),
(12, 2), (12, 3),
(13, 4), (13, 7),
(14, 2),
(15, 5), (15, 4),
(16, 1), (16, 8),
(17, 5),
(18, 1),
(19, 6),
(20, 8),
(21, 1);
GO

DECLARE @hoy DATETIME = CAST(CAST(GETDATE() AS DATE) AS DATETIME);

INSERT INTO ClasesApoyo (IdTutor, IdMateria, FechaHora, Cupo) VALUES
(1, 1, DATEADD(HOUR, 16, DATEADD(DAY, -7, @hoy)), 5),
(1, 2, DATEADD(HOUR, 17, DATEADD(DAY, -3, @hoy)), 5),
(4, 2, DATEADD(HOUR, 15, DATEADD(DAY, -5, @hoy)), 4),
(8, 6, DATEADD(HOUR, 14, DATEADD(DAY, -2, @hoy)), 6),
(1, 1, DATEADD(HOUR, 3, GETDATE()), 5),
(1, 2, DATEADD(HOUR, 16, DATEADD(DAY, 2, @hoy)), 1),
(1, 1, DATEADD(HOUR, 17, DATEADD(DAY, 4, @hoy)), 5),
(2, 1, DATEADD(HOUR, 15, DATEADD(DAY, 3, @hoy)), 4),
(2, 1, DATEADD(HOUR, 16, DATEADD(DAY, 8, @hoy)), 4),
(3, 4, DATEADD(HOUR, 14, DATEADD(DAY, 2, @hoy)), 6),
(3, 5, DATEADD(HOUR, 15, DATEADD(DAY, 9, @hoy)), 6),
(4, 2, DATEADD(HOUR, 16, DATEADD(DAY, 1, @hoy)), 3),
(4, 1, DATEADD(HOUR, 17, DATEADD(DAY, 6, @hoy)), 3),
(5, 3, DATEADD(HOUR, 15, DATEADD(DAY, 5, @hoy)), 5),
(5, 7, DATEADD(HOUR, 16, DATEADD(DAY, 10, @hoy)), 5),
(6, 4, DATEADD(HOUR, 14, DATEADD(DAY, 3, @hoy)), 8),
(7, 5, DATEADD(HOUR, 15, DATEADD(DAY, 7, @hoy)), 4),
(8, 6, DATEADD(HOUR, 16, DATEADD(DAY, 2, @hoy)), 6),
(8, 6, DATEADD(HOUR, 17, DATEADD(DAY, 9, @hoy)), 6),
(9, 7, DATEADD(HOUR, 14, DATEADD(DAY, 4, @hoy)), 5),
(10, 8, DATEADD(HOUR, 18, DATEADD(DAY, 3, @hoy)), 10),
(10, 1, DATEADD(HOUR, 18, DATEADD(DAY, 11, @hoy)), 10),
(11, 1, DATEADD(HOUR, 15, DATEADD(DAY, 6, @hoy)), 4),
(12, 3, DATEADD(HOUR, 16, DATEADD(DAY, 8, @hoy)), 5),
(13, 7, DATEADD(HOUR, 14, DATEADD(DAY, 5, @hoy)), 5),
(14, 2, DATEADD(HOUR, 17, DATEADD(DAY, 12, @hoy)), 4),
(15, 5, DATEADD(HOUR, 15, DATEADD(DAY, 4, @hoy)), 6),
(16, 8, DATEADD(HOUR, 16, DATEADD(DAY, 7, @hoy)), 8),
(17, 5, DATEADD(HOUR, 14, DATEADD(DAY, 10, @hoy)), 5),
(18, 1, DATEADD(HOUR, 17, DATEADD(DAY, 2, @hoy)), 3),
(19, 6, DATEADD(HOUR, 15, DATEADD(DAY, 13, @hoy)), 6),
(20, 8, DATEADD(HOUR, 18, DATEADD(DAY, 6, @hoy)), 10);

INSERT INTO Inscripciones (IdClaseApoyo, IdUsuario, FechaInscripcion, Cancelado, FechaCancelacion) VALUES
(1, 1, DATEADD(DAY, -10, @hoy), 0, NULL),
(2, 1, DATEADD(DAY, -6, @hoy), 0, NULL),
(3, 2, DATEADD(DAY, -8, @hoy), 0, NULL),
(4, 3, DATEADD(DAY, -4, @hoy), 0, NULL),
(5, 1, DATEADD(DAY, -1, @hoy), 0, NULL),
(6, 2, DATEADD(DAY, -1, @hoy), 0, NULL),
(7, 1, DATEADD(DAY, -2, @hoy), 0, NULL),
(10, 1, DATEADD(DAY, -1, @hoy), 0, NULL),
(12, 3, DATEADD(DAY, -2, @hoy), 0, NULL),
(8, 2, DATEADD(DAY, -3, @hoy), 1, DATEADD(DAY, -1, @hoy));
GO
