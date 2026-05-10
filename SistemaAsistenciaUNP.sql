IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Carreras] (
    [CarreraId] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Descripcion] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Carreras] PRIMARY KEY ([CarreraId])
);
GO

CREATE TABLE [Docentes] (
    [DocenteId] int NOT NULL IDENTITY,
    [Nombres] nvarchar(max) NOT NULL,
    [Apellidos] nvarchar(max) NOT NULL,
    [Correo] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NULL,
    [Especialidad] nvarchar(max) NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Docentes] PRIMARY KEY ([DocenteId])
);
GO

CREATE TABLE [Asignaturas] (
    [AsignaturaId] int NOT NULL IDENTITY,
    [Codigo] nvarchar(max) NOT NULL,
    [Nombre] nvarchar(max) NOT NULL,
    [Creditos] int NOT NULL,
    [CarreraId] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Asignaturas] PRIMARY KEY ([AsignaturaId]),
    CONSTRAINT [FK_Asignaturas_Carreras_CarreraId] FOREIGN KEY ([CarreraId]) REFERENCES [Carreras] ([CarreraId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Estudiantes] (
    [EstudianteId] int NOT NULL IDENTITY,
    [Carnet] nvarchar(max) NOT NULL,
    [Nombres] nvarchar(max) NOT NULL,
    [Apellidos] nvarchar(max) NOT NULL,
    [Correo] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NULL,
    [CarreraId] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Estudiantes] PRIMARY KEY ([EstudianteId]),
    CONSTRAINT [FK_Estudiantes_Carreras_CarreraId] FOREIGN KEY ([CarreraId]) REFERENCES [Carreras] ([CarreraId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Grupos] (
    [GrupoId] int NOT NULL IDENTITY,
    [NombreGrupo] nvarchar(max) NOT NULL,
    [Turno] nvarchar(max) NULL,
    [Aula] nvarchar(max) NULL,
    [PeriodoAcademico] nvarchar(max) NULL,
    [AsignaturaId] int NOT NULL,
    [DocenteId] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Grupos] PRIMARY KEY ([GrupoId]),
    CONSTRAINT [FK_Grupos_Asignaturas_AsignaturaId] FOREIGN KEY ([AsignaturaId]) REFERENCES [Asignaturas] ([AsignaturaId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Grupos_Docentes_DocenteId] FOREIGN KEY ([DocenteId]) REFERENCES [Docentes] ([DocenteId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Matriculas] (
    [MatriculaId] int NOT NULL IDENTITY,
    [EstudianteId] int NOT NULL,
    [GrupoId] int NOT NULL,
    [FechaMatricula] datetime2 NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_Matriculas] PRIMARY KEY ([MatriculaId]),
    CONSTRAINT [FK_Matriculas_Estudiantes_EstudianteId] FOREIGN KEY ([EstudianteId]) REFERENCES [Estudiantes] ([EstudianteId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Matriculas_Grupos_GrupoId] FOREIGN KEY ([GrupoId]) REFERENCES [Grupos] ([GrupoId]) ON DELETE NO ACTION
);
GO

CREATE TABLE [SesionesClase] (
    [SesionClaseId] int NOT NULL IDENTITY,
    [Fecha] datetime2 NOT NULL,
    [Tema] nvarchar(max) NULL,
    [GrupoId] int NOT NULL,
    [Estado] bit NOT NULL,
    CONSTRAINT [PK_SesionesClase] PRIMARY KEY ([SesionClaseId]),
    CONSTRAINT [FK_SesionesClase_Grupos_GrupoId] FOREIGN KEY ([GrupoId]) REFERENCES [Grupos] ([GrupoId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Asistencias] (
    [AsistenciaId] int NOT NULL IDENTITY,
    [SesionClaseId] int NOT NULL,
    [EstudianteId] int NOT NULL,
    [EstadoAsistencia] nvarchar(max) NOT NULL,
    [Observaciones] nvarchar(max) NULL,
    CONSTRAINT [PK_Asistencias] PRIMARY KEY ([AsistenciaId]),
    CONSTRAINT [FK_Asistencias_Estudiantes_EstudianteId] FOREIGN KEY ([EstudianteId]) REFERENCES [Estudiantes] ([EstudianteId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Asistencias_SesionesClase_SesionClaseId] FOREIGN KEY ([SesionClaseId]) REFERENCES [SesionesClase] ([SesionClaseId]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Asignaturas_CarreraId] ON [Asignaturas] ([CarreraId]);
GO

CREATE INDEX [IX_Asistencias_EstudianteId] ON [Asistencias] ([EstudianteId]);
GO

CREATE INDEX [IX_Asistencias_SesionClaseId] ON [Asistencias] ([SesionClaseId]);
GO

CREATE INDEX [IX_Estudiantes_CarreraId] ON [Estudiantes] ([CarreraId]);
GO

CREATE INDEX [IX_Grupos_AsignaturaId] ON [Grupos] ([AsignaturaId]);
GO

CREATE INDEX [IX_Grupos_DocenteId] ON [Grupos] ([DocenteId]);
GO

CREATE INDEX [IX_Matriculas_EstudianteId] ON [Matriculas] ([EstudianteId]);
GO

CREATE INDEX [IX_Matriculas_GrupoId] ON [Matriculas] ([GrupoId]);
GO

CREATE INDEX [IX_SesionesClase_GrupoId] ON [SesionesClase] ([GrupoId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260508005525_CreateDatabaseTables', N'8.0.26');
GO

COMMIT;
GO

