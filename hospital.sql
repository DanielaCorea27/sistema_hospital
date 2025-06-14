USE [master]
GO
/****** Object:  Database [HOSPIPLUS]    Script Date: 12/6/2025 17:42:12 ******/
CREATE DATABASE [HOSPIPLUS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HOSPIPLUS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\HOSPIPLUS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HOSPIPLUS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\HOSPIPLUS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HOSPIPLUS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HOSPIPLUS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HOSPIPLUS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET ARITHABORT OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [HOSPIPLUS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HOSPIPLUS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HOSPIPLUS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HOSPIPLUS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HOSPIPLUS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HOSPIPLUS] SET  MULTI_USER 
GO
ALTER DATABASE [HOSPIPLUS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HOSPIPLUS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HOSPIPLUS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HOSPIPLUS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HOSPIPLUS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HOSPIPLUS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HOSPIPLUS] SET QUERY_STORE = ON
GO
ALTER DATABASE [HOSPIPLUS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HOSPIPLUS]
GO
/****** Object:  UserDefinedFunction [dbo].[FNENCONTRARUSUARIO]    Script Date: 12/6/2025 17:42:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREAR FUNCION PARA BUSCAR USUARIOS
CREATE FUNCTION [dbo].[FNENCONTRARUSUARIO](
    @Email VARCHAR(50),
    @Clave VARCHAR(50)
) 
RETURNS INT
AS
BEGIN
    -- DECLARAR VARIABLE LOCAL
    DECLARE @Encontrado INT = 0;
    
    -- CREAR LA CONSULTA PARA ENCONTRAR USUARIO ACTIVO
    SELECT
        @Encontrado = COUNT(*) 
    FROM Usuarios
    WHERE
        Email = @Email
        AND CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('HOSPI', Clave)) = @Clave
        AND EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
    
    -- RETORNAR EL RESULTADO
    RETURN (@Encontrado);
END;
GO
/****** Object:  UserDefinedFunction [dbo].[FNVERIFICARUSUARIO]    Script Date: 12/6/2025 17:42:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--CREAR FUNCION PARA BUSCAR USUARIOS POR CORREO
CREATE FUNCTION [dbo].[FNVERIFICARUSUARIO](
    @Email VARCHAR(50)
) 
RETURNS INT
AS
BEGIN
    -- DECLARAR VARIABLE LOCAL
    DECLARE @Encontrado INT = 0;
    
    -- CREAR LA CONSULTA PARA ENCONTRAR USUARIO ACTIVO
    SELECT
        @Encontrado = COUNT(*) 
    FROM Usuarios
    WHERE
        Email = @Email
        AND EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
    
    -- RETORNAR EL RESULTADO
    RETURN (@Encontrado);
END;
GO
/****** Object:  Table [dbo].[Cita]    Script Date: 12/6/2025 17:42:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cita](
	[CitaID] [int] IDENTITY(1,1) NOT NULL,
	[MedicoID] [int] NOT NULL,
	[PacienteID] [int] NOT NULL,
	[EspecialidadID] [int] NULL,
	[ConsultorioID] [int] NULL,
	[EstadoID] [int] NULL,
	[EstadoCitaID] [int] NULL,
	[MotivoCita] [varchar](255) NULL,
	[Comentarios] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[CitaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consulta]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consulta](
	[ConsultaID] [int] IDENTITY(1,1) NOT NULL,
	[FechaConsulta] [datetime] NOT NULL,
	[MotivoConsulta] [varchar](255) NULL,
	[PacienteID] [int] NULL,
	[MedicoID] [int] NULL,
	[EstadoID] [int] NOT NULL,
	[ConsultorioID] [int] NULL,
	[Examen] [bit] NULL,
	[Bandera] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ConsultaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consultorio]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consultorio](
	[ConsultorioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreConsultorio] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ConsultorioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamentos]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamentos](
	[DepartamentoID] [int] IDENTITY(1,1) NOT NULL,
	[Departamento] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartamentoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Especialidad]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Especialidad](
	[EspecialidadID] [int] IDENTITY(1,1) NOT NULL,
	[NombreEspecialidad] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EspecialidadID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoCita]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoCita](
	[EstadoCitaID] [int] IDENTITY(1,1) NOT NULL,
	[EstadoCita] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoCitaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoExamen]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoExamen](
	[EstadoExamenID] [int] IDENTITY(1,1) NOT NULL,
	[EstadoExamen] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoExamenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoReceta]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoReceta](
	[EstadoRecetaID] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstadoReceta] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoRecetaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[EstadoID] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examen]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examen](
	[ExamenID] [int] IDENTITY(1,1) NOT NULL,
	[TipoExamenID] [int] NULL,
	[EspecialidadID] [int] NULL,
	[FechaExamen] [datetime] NULL,
	[Resultado] [text] NULL,
	[PacienteID] [int] NULL,
	[MedicoID] [int] NULL,
	[ConsultaID] [int] NULL,
	[EstadoExamenID] [int] NOT NULL,
	[EstadoID] [int] NOT NULL,
	[FechaResultado] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExamenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[GeneroID] [int] IDENTITY(1,1) NOT NULL,
	[NombreGenero] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GeneroID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistorialPaciente]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistorialPaciente](
	[HistorialPacienteID] [int] IDENTITY(1,1) NOT NULL,
	[PacienteID] [int] NULL,
	[MotivoConsulta] [varchar](255) NULL,
	[Padecimientos] [varchar](255) NULL,
	[Traumatismos] [varchar](255) NULL,
	[CirugiasPrevias] [varchar](255) NULL,
	[MedicacionActual] [varchar](255) NULL,
	[AntecedentesFamiliares] [varchar](255) NULL,
	[Discapacidad] [varchar](255) NULL,
	[Alergia] [varchar](255) NULL,
	[EnfermedadCronica] [varchar](255) NULL,
	[Observaciones] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[HistorialPacienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoraFin]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoraFin](
	[HoraFinID] [int] IDENTITY(1,1) NOT NULL,
	[NombreHoraFin] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HoraFinID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoraInicio]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoraInicio](
	[HoraInicioID] [int] IDENTITY(1,1) NOT NULL,
	[NombreHoraInicio] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HoraInicioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HorarioCita]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HorarioCita](
	[HorarioCitaID] [int] IDENTITY(1,1) NOT NULL,
	[CitaID] [int] NOT NULL,
	[FechaCita] [date] NOT NULL,
	[Hora] [varchar](10) NOT NULL,
	[Duracion] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[HorarioCitaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicamentoReceta]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicamentoReceta](
	[MedicamentoRecetaID] [int] IDENTITY(1,1) NOT NULL,
	[RecetaID] [int] NULL,
	[MedicamentosID] [int] NULL,
	[Indicaciones] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicamentoRecetaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicamentos]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicamentos](
	[MedicamentosID] [int] IDENTITY(1,1) NOT NULL,
	[NombreMedicamento] [varchar](50) NULL,
	[Cantidad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicamentosID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medico]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medico](
	[MedicoID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[EspecialidadID] [int] NULL,
	[HoraInicioID] [int] NULL,
	[HoraFinID] [int] NULL,
	[ConsultorioID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicoTelefono]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicoTelefono](
	[MedicoID] [int] NOT NULL,
	[TelefonoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MedicoID] ASC,
	[TelefonoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Municipios]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Municipios](
	[MunicipioID] [int] IDENTITY(1,1) NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[Municipio] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MunicipioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pacientes]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pacientes](
	[PacienteID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NOT NULL,
	[GeneroID] [int] NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[MunicipioID] [int] NOT NULL,
	[Residencia] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[PacienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PacienteTelefono]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PacienteTelefono](
	[PacienteID] [int] NOT NULL,
	[TelefonoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PacienteID] ASC,
	[TelefonoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receta]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receta](
	[RecetaID] [int] IDENTITY(1,1) NOT NULL,
	[FechaEmision] [datetime] NULL,
	[PacienteID] [int] NULL,
	[MedicoID] [int] NULL,
	[ConsultaID] [int] NULL,
	[EstadoRecetaID] [int] NULL,
	[EstadoID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RecetaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RolID] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefonos]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefonos](
	[TelefonoID] [int] IDENTITY(1,1) NOT NULL,
	[Telefono] [varchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TelefonoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Telefono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoExamenes]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoExamenes](
	[TipoExamenID] [int] IDENTITY(1,1) NOT NULL,
	[TipoExamen] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoExamenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[RolID] [int] NOT NULL,
	[Clave] [varbinary](1000) NOT NULL,
	[EstadoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Consulta] ADD  DEFAULT ((1)) FOR [EstadoID]
GO
ALTER TABLE [dbo].[EstadoCita] ADD  DEFAULT ('Programada') FOR [EstadoCita]
GO
ALTER TABLE [dbo].[EstadoReceta] ADD  DEFAULT ('Pendiente') FOR [NombreEstadoReceta]
GO
ALTER TABLE [dbo].[Estados] ADD  DEFAULT ('Activo') FOR [Estado]
GO
ALTER TABLE [dbo].[Examen] ADD  DEFAULT ((1)) FOR [EstadoExamenID]
GO
ALTER TABLE [dbo].[Examen] ADD  DEFAULT ((1)) FOR [EstadoID]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Consultorio] FOREIGN KEY([ConsultorioID])
REFERENCES [dbo].[Consultorio] ([ConsultorioID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Consultorio]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Especialidad] FOREIGN KEY([EspecialidadID])
REFERENCES [dbo].[Especialidad] ([EspecialidadID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Especialidad]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Estado] FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Estado]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_EstadoCita] FOREIGN KEY([EstadoCitaID])
REFERENCES [dbo].[EstadoCita] ([EstadoCitaID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_EstadoCita]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Medico] FOREIGN KEY([MedicoID])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Medico]
GO
ALTER TABLE [dbo].[Cita]  WITH CHECK ADD  CONSTRAINT [FK_Cita_Paciente] FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Pacientes] ([PacienteID])
GO
ALTER TABLE [dbo].[Cita] CHECK CONSTRAINT [FK_Cita_Paciente]
GO
ALTER TABLE [dbo].[Consulta]  WITH CHECK ADD FOREIGN KEY([ConsultorioID])
REFERENCES [dbo].[Consultorio] ([ConsultorioID])
GO
ALTER TABLE [dbo].[Consulta]  WITH CHECK ADD FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Consulta]  WITH CHECK ADD FOREIGN KEY([MedicoID])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[Consulta]  WITH CHECK ADD FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Pacientes] ([PacienteID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([ConsultaID])
REFERENCES [dbo].[Consulta] ([ConsultaID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([EspecialidadID])
REFERENCES [dbo].[Especialidad] ([EspecialidadID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([EstadoExamenID])
REFERENCES [dbo].[EstadoExamen] ([EstadoExamenID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([MedicoID])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Pacientes] ([PacienteID])
GO
ALTER TABLE [dbo].[Examen]  WITH CHECK ADD FOREIGN KEY([TipoExamenID])
REFERENCES [dbo].[TipoExamenes] ([TipoExamenID])
GO
ALTER TABLE [dbo].[HistorialPaciente]  WITH CHECK ADD FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Pacientes] ([PacienteID])
GO
ALTER TABLE [dbo].[HorarioCita]  WITH CHECK ADD  CONSTRAINT [FK_HorarioCita_Cita] FOREIGN KEY([CitaID])
REFERENCES [dbo].[Cita] ([CitaID])
GO
ALTER TABLE [dbo].[HorarioCita] CHECK CONSTRAINT [FK_HorarioCita_Cita]
GO
ALTER TABLE [dbo].[MedicamentoReceta]  WITH CHECK ADD FOREIGN KEY([MedicamentosID])
REFERENCES [dbo].[Medicamentos] ([MedicamentosID])
GO
ALTER TABLE [dbo].[MedicamentoReceta]  WITH CHECK ADD FOREIGN KEY([RecetaID])
REFERENCES [dbo].[Receta] ([RecetaID])
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD FOREIGN KEY([ConsultorioID])
REFERENCES [dbo].[Consultorio] ([ConsultorioID])
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD FOREIGN KEY([EspecialidadID])
REFERENCES [dbo].[Especialidad] ([EspecialidadID])
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD FOREIGN KEY([HoraFinID])
REFERENCES [dbo].[HoraFin] ([HoraFinID])
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD FOREIGN KEY([HoraInicioID])
REFERENCES [dbo].[HoraInicio] ([HoraInicioID])
GO
ALTER TABLE [dbo].[Medico]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[MedicoTelefono]  WITH CHECK ADD FOREIGN KEY([MedicoID])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[MedicoTelefono]  WITH CHECK ADD FOREIGN KEY([TelefonoID])
REFERENCES [dbo].[Telefonos] ([TelefonoID])
GO
ALTER TABLE [dbo].[Municipios]  WITH CHECK ADD FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamentos] ([DepartamentoID])
GO
ALTER TABLE [dbo].[Pacientes]  WITH CHECK ADD FOREIGN KEY([GeneroID])
REFERENCES [dbo].[Genero] ([GeneroID])
GO
ALTER TABLE [dbo].[Pacientes]  WITH CHECK ADD FOREIGN KEY([MunicipioID])
REFERENCES [dbo].[Municipios] ([MunicipioID])
GO
ALTER TABLE [dbo].[Pacientes]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[PacienteTelefono]  WITH CHECK ADD FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Pacientes] ([PacienteID])
GO
ALTER TABLE [dbo].[PacienteTelefono]  WITH CHECK ADD FOREIGN KEY([TelefonoID])
REFERENCES [dbo].[Telefonos] ([TelefonoID])
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD FOREIGN KEY([ConsultaID])
REFERENCES [dbo].[Consulta] ([ConsultaID])
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD FOREIGN KEY([EstadoRecetaID])
REFERENCES [dbo].[EstadoReceta] ([EstadoRecetaID])
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD FOREIGN KEY([MedicoID])
REFERENCES [dbo].[Medico] ([MedicoID])
GO
ALTER TABLE [dbo].[Receta]  WITH CHECK ADD FOREIGN KEY([PacienteID])
REFERENCES [dbo].[Pacientes] ([PacienteID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([EstadoID])
REFERENCES [dbo].[Estados] ([EstadoID])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([RolID])
REFERENCES [dbo].[Roles] ([RolID])
GO
ALTER TABLE [dbo].[EstadoCita]  WITH CHECK ADD CHECK  (([EstadoCita]='Finalizada' OR [EstadoCita]='Cancelada' OR [EstadoCita]='Reprogramada' OR [EstadoCita]='Programada'))
GO
ALTER TABLE [dbo].[EstadoReceta]  WITH CHECK ADD CHECK  (([NombreEstadoReceta]='Retirada' OR [NombreEstadoReceta]='Pendiente'))
GO
ALTER TABLE [dbo].[Estados]  WITH CHECK ADD CHECK  (([Estado]='Inactivo' OR [Estado]='Activo'))
GO
/****** Object:  StoredProcedure [dbo].[AgregarTelefono]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA AGREGAR TELEFONOS
CREATE PROCEDURE [dbo].[AgregarTelefono]
    @MedicoID INT,
    @Telefono VARCHAR(15)
AS
BEGIN
    DECLARE @TelefonoID INT;

    BEGIN TRY
        -- Verificar si el teléfono ya existe en la tabla Telefonos
        SELECT @TelefonoID = TelefonoID FROM Telefonos WHERE Telefono = @Telefono;

        -- Si el teléfono ya está asignado a otro médico, devolver un error
        IF @TelefonoID IS NOT NULL AND EXISTS (SELECT 1 FROM MedicoTelefono WHERE TelefonoID = @TelefonoID AND MedicoID != @MedicoID)
        BEGIN
            RAISERROR('Error: El número de teléfono ya está asignado a otro médico.', 16, 1);
            RETURN;
        END

        -- Si el teléfono no existe, insertar en la tabla Telefonos
        IF @TelefonoID IS NULL
        BEGIN
            INSERT INTO Telefonos (Telefono)
            VALUES (@Telefono);

            -- Obtener el TelefonoID recién insertado
            SET @TelefonoID = SCOPE_IDENTITY();
        END

        -- Si no existe la relación entre Medico y Telefono, insertarla
        IF NOT EXISTS (SELECT 1 FROM MedicoTelefono WHERE MedicoID = @MedicoID AND TelefonoID = @TelefonoID)
        BEGIN
            INSERT INTO MedicoTelefono (MedicoID, TelefonoID)
            VALUES (@MedicoID, @TelefonoID);
        END

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarTelefonoPaciente]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarTelefonoPaciente]
    @PacienteID INT,
    @Telefono VARCHAR(15)
AS
BEGIN
    DECLARE @TelefonoID INT;

    BEGIN TRY
		-- Verificar si el teléfono ya existe en la tabla Telefonos
        SELECT @TelefonoID = TelefonoID FROM Telefonos WHERE Telefono = @Telefono;

		-- Si el teléfono ya está asignado a otro paciente, devolver un error
        IF @TelefonoID IS NOT NULL AND EXISTS (SELECT 1 FROM PacienteTelefono WHERE TelefonoID = @TelefonoID AND PacienteID != @PacienteID)
        BEGIN
            RAISERROR('Error: El teléfono ya está registrado.', 16, 1);
            RETURN;
        END

		-- Si el teléfono no existe, insertar en la tabla Telefonos
		IF @TelefonoID IS NULL
		BEGIN
            INSERT INTO Telefonos (Telefono)
            VALUES (@Telefono);

            -- Obtener el TelefonoID recién insertado
            SET @TelefonoID = SCOPE_IDENTITY();
        END

		-- Si no existe la relación entre Paciente y Telefono, insertarla
        IF NOT EXISTS (SELECT 1 FROM PacienteTelefono WHERE PacienteID = @PacienteID AND TelefonoID = @TelefonoID)
        BEGIN
            INSERT INTO PacienteTelefono (PacienteID, TelefonoID)
            VALUES (@PacienteID, @TelefonoID);
        END	

		END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPACTUALIZARCITA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA ACTUALIZAR CITAS
CREATE PROCEDURE [dbo].[SPACTUALIZARCITA]
    @CitaID INT,
    @FechaCita DATE = NULL,
    @Hora VARCHAR(10) = NULL,
    @Duracion INT = NULL,
    @MotivoCita VARCHAR(255) = NULL,
    @PacienteID INT = NULL,
    @MedicoID INT = NULL,
    @EspecialidadID INT = NULL,
    @ConsultorioID INT = NULL,
    @Comentarios TEXT = NULL
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @FechaActual DATE, @HoraActual VARCHAR(10), @EstadoCitaID INT;
        SELECT @FechaActual = FechaCita, @HoraActual = Hora
        FROM HorarioCita WHERE CitaID = @CitaID;

        -- Obtiene el ID del estado "Reprogramada"
        SET @EstadoCitaID = (SELECT EstadoCitaID FROM EstadoCita WHERE EstadoCita = 'Reprogramada');

        UPDATE Cita
        SET 
            MotivoCita = ISNULL(@MotivoCita, MotivoCita),
            PacienteID = ISNULL(@PacienteID, PacienteID),
            MedicoID = ISNULL(@MedicoID, MedicoID),
            EspecialidadID = ISNULL(@EspecialidadID, EspecialidadID),
            ConsultorioID = ISNULL(@ConsultorioID, ConsultorioID),
            Comentarios = ISNULL(@Comentarios, Comentarios),
            EstadoCitaID = CASE
                WHEN ((@FechaCita IS NOT NULL AND @FechaCita <> @FechaActual) OR 
                     (@Hora IS NOT NULL AND @Hora <> @HoraActual))
                THEN @EstadoCitaID  
                ELSE EstadoCitaID
            END
        WHERE CitaID = @CitaID
          AND EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');

        -- Actualizar datos del horario de la cita en la tabla `HorarioCita`
        UPDATE HorarioCita
        SET 
            FechaCita = ISNULL(@FechaCita, FechaCita),
            Hora = ISNULL(@Hora, Hora),
            Duracion = ISNULL(@Duracion, Duracion)
        WHERE CitaID = @CitaID;

        -- Confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores y rollback
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPACTUALIZARHISTORIALPACIENTE]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


----------------------------------------------------------
-- CREAR EL PROCESO ALMACENADO SPACTUALIZARHISTORIALPACIENTE
----------------------------------------------------------
CREATE PROCEDURE [dbo].[SPACTUALIZARHISTORIALPACIENTE]
    @PacienteID INT,
    @MotivoConsulta VARCHAR(255),
    @Padecimientos VARCHAR(255),
    @Traumatismos VARCHAR(255),
    @CirugiasPrevias VARCHAR(255),
    @MedicacionActual VARCHAR(255),
    @AntecedentesFamiliares VARCHAR(255),
    @Discapacidad VARCHAR(255),
    @Alergia VARCHAR(255),
    @EnfermedadCronica VARCHAR(255),
    @Observaciones TEXT
AS
BEGIN
    -- Iniciar una transacción
    BEGIN TRANSACTION;

    -- Verificar que el historial del paciente existe
    IF NOT EXISTS (SELECT 1 FROM HistorialPaciente WHERE PacienteID = @PacienteID)
    BEGIN
        RAISERROR('Error: No existe un historial para este paciente.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Intentar la actualización
    BEGIN TRY
        UPDATE HistorialPaciente
        SET 
            MotivoConsulta = @MotivoConsulta,
            Padecimientos = @Padecimientos,
            Traumatismos = @Traumatismos,
            CirugiasPrevias = @CirugiasPrevias,
            MedicacionActual = @MedicacionActual,
            AntecedentesFamiliares = @AntecedentesFamiliares,
            Discapacidad = @Discapacidad,
            Alergia = @Alergia,
            EnfermedadCronica = @EnfermedadCronica,
            Observaciones = @Observaciones
        WHERE 
            PacienteID = @PacienteID;

        -- Confirmar la transacción si la actualización fue exitosa
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
         -- Si ocurre algún error, revertir todos los cambios
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
        

		 -- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPACTUALIZARMEDICO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA EDITAR MEDICO
CREATE PROCEDURE [dbo].[SPACTUALIZARMEDICO]
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @EmailActual VARCHAR(50),    
    @NuevoEmail VARCHAR(50),    
    @Identificacion VARCHAR(20),
    @EspecialidadID INT,
    @TelefonoActual VARCHAR(15), 
    @NuevoTelefono VARCHAR(15), 
    @HoraInicioID INT,
    @HoraFinID INT,
	@ConsultorioID INT
AS
BEGIN
    DECLARE @UsuarioID INT;
    DECLARE @MedicoID INT;

    BEGIN TRY
        -- Obtener el UsuarioID y MedicoID basado en el EmailActual
        SET @UsuarioID = (SELECT UsuarioID FROM Usuarios WHERE Email = @EmailActual);
        SET @MedicoID = (SELECT MedicoID FROM Medico WHERE UsuarioID = @UsuarioID);

        -- Verificar si el nuevo teléfono ya está asignado a otro médico
        IF EXISTS (SELECT 1 FROM Telefonos T
                   INNER JOIN MedicoTelefono MT ON T.TelefonoID = MT.TelefonoID
                   WHERE T.Telefono = @NuevoTelefono AND MT.MedicoID != @MedicoID)
        BEGIN
            RAISERROR('Error: El número de teléfono ya está asignado a otro médico.', 16, 1);
            RETURN;
        END

        -- Actualizar el teléfono seleccionado en la tabla Telefonos usando TelefonoActual
        UPDATE T
        SET T.Telefono = @NuevoTelefono
        FROM Telefonos T
        INNER JOIN MedicoTelefono MT ON T.TelefonoID = MT.TelefonoID
        WHERE MT.MedicoID = @MedicoID AND T.Telefono = @TelefonoActual;

        -- Actualizar los datos en la tabla Medico
        UPDATE Medico
        SET
            Identificacion = @Identificacion,
            EspecialidadID = @EspecialidadID,
            HoraInicioID = @HoraInicioID,
            HoraFinID = @HoraFinID,
			ConsultorioID = @ConsultorioID
        WHERE
            MedicoID = @MedicoID;

        -- Actualizar los datos del usuario 
        IF @NuevoEmail != @EmailActual
        BEGIN
            -- Verificar si el nuevo email ya existe para otro usuario
            IF EXISTS (SELECT 1 FROM Usuarios WHERE Email = @NuevoEmail AND UsuarioID != @UsuarioID)
            BEGIN
                RAISERROR('Error: El nuevo correo electrónico ya existe en el sistema para otro usuario.', 16, 1);
                RETURN;
            END
            
            -- Si el correo es válido, actualizarlo
            UPDATE Usuarios
            SET
                Nombre = @Nombre,
                Apellido = @Apellido,
                Email = @NuevoEmail
            WHERE
                UsuarioID = @UsuarioID;
        END
        ELSE
        BEGIN
            -- Si el email no cambió, solo actualizar nombre y apellido
            UPDATE Usuarios
            SET
                Nombre = @Nombre,
                Apellido = @Apellido
            WHERE
                UsuarioID = @UsuarioID;
        END

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

        -- Manejo de errores
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPACTUALIZARPACIENTE]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------------

------------------------------------------------------------------------------
			-- PROCESO ALMACENADO PARA ACTUALIZAR PACIENTE
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[SPACTUALIZARPACIENTE]
	@Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @EmailActual VARCHAR(50),
    @NuevoEmail VARCHAR(50),
    @Identificacion VARCHAR(20),
    @GeneroID INT,
    @Residencia VARCHAR(100),
	@FechaNacimiento DATE,
    @TelefonoActual VARCHAR(15),
    @NuevoTelefono VARCHAR(15),
	@MunicipioID INT
AS
BEGIN
    DECLARE @UsuarioID INT;
    DECLARE @PacienteID INT;

    BEGIN TRY
        SET @UsuarioID = (SELECT UsuarioID FROM Usuarios WHERE Email = @EmailActual);
        SET @PacienteID = (SELECT PacienteID FROM Pacientes WHERE UsuarioID = @UsuarioID);

        IF EXISTS (SELECT 1 FROM Telefonos T
		INNER JOIN PacienteTelefono PT ON T.TelefonoID = PT.TelefonoID
            WHERE Telefono = @NuevoTelefono AND PT.PacienteID != @PacienteID
        )
        BEGIN
            RAISERROR('Error: El número de teléfono ya está asignado.', 16, 1);
            RETURN;
        END

        UPDATE Telefonos
        SET Telefono = @NuevoTelefono
        WHERE Telefono = @TelefonoActual;

        UPDATE Pacientes
        SET Identificacion = @Identificacion, 
            GeneroID = @GeneroID, 
            Residencia = @Residencia,
			FechaNacimiento = @FechaNacimiento,
			MunicipioID = @MunicipioID
        WHERE PacienteID = @PacienteID;

		UPDATE Usuarios
            SET 
			Nombre = @Nombre,
			Apellido = @Apellido
            WHERE UsuarioID = @UsuarioID;

        IF @NuevoEmail != @EmailActual
        BEGIN
		 -- Verificar si el nuevo email ya existe para otro usuario
            IF EXISTS (SELECT 1 FROM Usuarios WHERE Email = @NuevoEmail AND UsuarioID != @UsuarioID)
            BEGIN
                RAISERROR('Error: El nuevo correo electrónico ya existe en el sistema para otro usuario.', 16, 1);
                RETURN;
            END

            UPDATE Usuarios
            SET 
			Email = @NuevoEmail
            WHERE UsuarioID = @UsuarioID;
        END

    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPACTUALIZARUSUARIO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA ACTUALIZAR USUARIO
CREATE PROCEDURE [dbo].[SPACTUALIZARUSUARIO]
    @EmailActual VARCHAR(50),
    @NuevoEmail VARCHAR(50),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @RolID INT,
    @Clave VARCHAR(100),
    @EstadoID INT = NULL  
AS
BEGIN
    -- Verificamos si el usuario con el email actual existe
    IF EXISTS (SELECT 1 FROM Usuarios WHERE Email = @EmailActual)
    BEGIN
        -- Actualizamos los datos del usuario
        UPDATE Usuarios
        SET
            Email = @NuevoEmail,
            Nombre = @Nombre,
            Apellido = @Apellido,
            RolID = @RolID,
            Clave = ENCRYPTBYPASSPHRASE('HOSPI', @Clave),
			EstadoID = ISNULL(@EstadoID, EstadoID)  
        WHERE Email = @EmailActual;
    END
    ELSE
    BEGIN
        -- En caso de que no exista el usuario, se envia error
        PRINT 'Usuario no encontrado';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARCITASCONSULTAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA BUSCAR CITAS EN VENTANA CONSULTA
CREATE PROCEDURE [dbo].[SPBUSCARCITASCONSULTAS] 
    @CitaID INT = NULL
AS
BEGIN
    SELECT 
		c.CitaID,
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID,
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico,
        con.ConsultorioID,
        con.NombreConsultorio AS NombreConsultorio
    FROM Cita c
    LEFT JOIN Pacientes p ON c.PacienteID = p.PacienteID  
	LEFT JOIN Usuarios up ON p.UsuarioID = up.UsuarioID  
	LEFT JOIN Medico m ON c.MedicoID = m.MedicoID  
	LEFT JOIN Usuarios um ON m.UsuarioID = um.UsuarioID 
	LEFT JOIN Consultorio con ON c.ConsultorioID = con.ConsultorioID
    WHERE 
        (@CitaID IS NULL OR c.CitaID = @CitaID) AND
        c.EstadoID = 1 
        AND c.EstadoCitaID IN (1, 2); 
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARCONSULTAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR CONSULTA
CREATE PROCEDURE [dbo].[SPBUSCARCONSULTAS] 
    @ConsultaID INT = NULL
AS
BEGIN
    SELECT 
		c.ConsultaID,
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID,
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico,
        con.ConsultorioID,
        con.NombreConsultorio AS NombreConsultorio,
		c.FechaConsulta,
		c.Examen,
		c.MotivoConsulta
    FROM Consulta c
    LEFT JOIN Pacientes p ON c.PacienteID = p.PacienteID  
	LEFT JOIN Usuarios up ON p.UsuarioID = up.UsuarioID  
	LEFT JOIN Medico m ON c.MedicoID = m.MedicoID  
	LEFT JOIN Usuarios um ON m.UsuarioID = um.UsuarioID  
	LEFT JOIN Consultorio con ON c.ConsultorioID = con.ConsultorioID
    WHERE 
        (@ConsultaID IS NULL OR c.ConsultaID = @ConsultaID) AND
        c.EstadoID = 1 
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARCONSULTASEXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMEINTO DE ALMACENAMIENTO PARA BUSCAR CONSULTAS CON EXAMEN
CREATE PROCEDURE [dbo].[SPBUSCARCONSULTASEXAMEN] 
    @ConsultaID INT = NULL
AS
BEGIN
    SELECT 
		c.ConsultaID,
        p.PacienteID, 
        up.Nombre AS NombrePaciente,  
        up.Apellido AS ApellidoPaciente,
		m.MedicoID, 
        um.Nombre AS NombreMedico, 
        um.Apellido AS ApellidoMedico
    FROM Consulta c
    LEFT JOIN Pacientes p ON c.PacienteID = p.PacienteID  
	LEFT JOIN Usuarios up ON p.UsuarioID = up.UsuarioID  
	LEFT JOIN Medico m ON c.MedicoID = m.MedicoID  
	LEFT JOIN Usuarios um ON m.UsuarioID = um.UsuarioID  
    WHERE 
        (@ConsultaID IS NULL OR c.ConsultaID = @ConsultaID) AND
        c.EstadoID = 1 AND
        c.Examen = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARCONSULTASRECETAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR CONSULTAS EN RECETAS
CREATE PROCEDURE [dbo].[SPBUSCARCONSULTASRECETAS]
    @ConsultaID INT = NULL
AS
BEGIN
    SELECT 
        c.ConsultaID,
        p.PacienteID, 
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID, 
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico
    FROM Consulta c
    LEFT JOIN Pacientes p ON c.PacienteID = p.PacienteID
    LEFT JOIN Usuarios up ON p.UsuarioID = up.UsuarioID
    LEFT JOIN Medico m ON c.MedicoID = m.MedicoID
    LEFT JOIN Usuarios um ON m.UsuarioID = um.UsuarioID
    WHERE 
        (@ConsultaID IS NULL OR c.ConsultaID = @ConsultaID) AND
        c.EstadoID = 1 
        AND c.Bandera = 1;
END;
------------------
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCAREXAMENAGENDADO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR EXAMENES AGENDADOS
CREATE PROCEDURE [dbo].[SPBUSCAREXAMENAGENDADO]
    @ExamenID INT = NULL
AS
BEGIN
    SELECT 
        E.ExamenID,
        E.PacienteID,
        E.MedicoID,
        TE.TipoExamen AS TipoExamenNombre,
        ES.NombreEspecialidad AS EspecialidadNombre,
        E.FechaExamen,
        EE.EstadoExamen AS EstadoExamenNombre,
        E.EstadoID
    FROM 
        Examen E
    INNER JOIN 
        TipoExamenes TE ON E.TipoExamenID = TE.TipoExamenID
    INNER JOIN 
        Especialidad ES ON E.EspecialidadID = ES.EspecialidadID
    INNER JOIN 
        EstadoExamen EE ON E.EstadoExamenID = EE.EstadoExamenID
    WHERE 
        E.EstadoID = 1 
        AND EE.EstadoExamen = 'Agendado'
        AND (@ExamenID IS NULL OR E.ExamenID = @ExamenID) 
    ORDER BY 
        E.FechaExamen;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCAREXAMENREGISTRADO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR EXAMENES REGISTRADOS
CREATE PROCEDURE [dbo].[SPBUSCAREXAMENREGISTRADO]
    @ExamenID INT = NULL
AS
BEGIN
    SELECT 
        E.ExamenID,
        E.PacienteID,
        E.MedicoID,
        TE.TipoExamen AS TipoExamenNombre,
        ES.NombreEspecialidad AS EspecialidadNombre,
        E.FechaExamen,
        E.FechaResultado,
        E.Resultado,
        EE.EstadoExamen AS EstadoExamenNombre,
        E.EstadoID
    FROM 
        Examen E
    INNER JOIN 
        TipoExamenes TE ON E.TipoExamenID = TE.TipoExamenID
    INNER JOIN 
        Especialidad ES ON E.EspecialidadID = ES.EspecialidadID
    INNER JOIN 
        EstadoExamen EE ON E.EstadoExamenID = EE.EstadoExamenID
    WHERE 
        E.EstadoID = 1 
        AND (EE.EstadoExamen = 'Pendiente' OR EE.EstadoExamen = 'Entregado') 
        AND (@ExamenID IS NULL OR E.ExamenID = @ExamenID) 
    ORDER BY 
        E.FechaExamen;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARHISTORIALPORPACIENTEID]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPBUSCARHISTORIALPORPACIENTEID]
    @PacienteID INT
AS
BEGIN
    SELECT 
        H.HistorialPacienteID,
        H.PacienteID,
        U.Nombre AS NombrePaciente,
        U.Apellido AS ApellidoPaciente,
        H.MotivoConsulta,
        H.Padecimientos,
        H.Traumatismos,
        H.CirugiasPrevias,
        H.MedicacionActual,
        H.AntecedentesFamiliares,
        H.Discapacidad,
        H.Alergia,
        H.EnfermedadCronica,
        H.Observaciones
    FROM 
        HistorialPaciente H
    INNER JOIN 
        Pacientes P ON H.PacienteID = P.PacienteID
    INNER JOIN 
        Usuarios U ON P.UsuarioID = U.UsuarioID
    WHERE 
		(@PacienteID IS NULL OR P.PacienteID = @PacienteID) AND
        U.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARMEDICAMENTOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MEDICAMENTOS
CREATE PROCEDURE [dbo].[SPBUSCARMEDICAMENTOS]
    @MedicamentosID INT = NULL
AS
BEGIN
    SELECT 
        MedicamentosID,
        NombreMedicamento,
        Cantidad
    FROM 
        Medicamentos
    WHERE 
        (@MedicamentosID IS NULL OR MedicamentosID = @MedicamentosID);
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARMEDICOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR MÉDICOS
CREATE PROCEDURE [dbo].[SPBUSCARMEDICOS] 
    @MedicoID INT = NULL
AS
BEGIN
    SELECT 
        m.MedicoID, 
        u.Nombre AS NombreMedico,  
        u.Apellido AS ApellidoMedico,  
        m.Identificacion,
        e.NombreEspecialidad AS NombreEspecialidad,  
        u.Email AS EmailMedico,  
        t.Telefono AS Telefono,
        i.NombreHoraInicio AS NombreHoraInicio, 
        f.NombreHoraFin AS NombreHoraFin,
		c.NombreConsultorio AS NombreConsultorio
    FROM Medico m
    LEFT JOIN Usuarios u ON m.UsuarioID = u.UsuarioID  -- Unir con Usuarios para obtener Nombre, Apellido y Email
    LEFT JOIN Especialidad e ON m.EspecialidadID = e.EspecialidadID
	LEFT JOIN HoraInicio i ON m.HoraInicioID = i.HoraInicioID
	LEFT JOIN HoraFin f ON m.HoraFinID = f.HoraFinID
	LEFT JOIN Consultorio c ON m.ConsultorioID = c.ConsultorioID
    LEFT JOIN MedicoTelefono mt ON m.MedicoID = mt.MedicoID  -- Relación con MedicoTelefono
    LEFT JOIN Telefonos t ON mt.TelefonoID = t.TelefonoID    -- Relación con Telefonos
    WHERE 
        (@MedicoID IS NULL OR m.MedicoID = @MedicoID) AND
        u.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARPACIENTES]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPBUSCARPACIENTES]
    @PacienteID INT = NULL
AS
BEGIN
    SELECT 
        p.PacienteID, 
        u.Nombre,  
        u.Apellido, 
        p.Identificacion,
		p.FechaNacimiento,
        t.Telefono,
		u.Email,
        p.Residencia,
		m.Municipio,
        g.NombreGenero
    FROM Pacientes p
    INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID
    INNER JOIN Genero g ON p.GeneroID = g.GeneroID
	INNER JOIN Municipios m ON p.MunicipioID = m.MunicipioID
    LEFT JOIN PacienteTelefono pt ON p.PacienteID = pt.PacienteID
    LEFT JOIN Telefonos t ON pt.TelefonoID = t.TelefonoID
    WHERE 
		(@PacienteID IS NULL OR p.PacienteID = @PacienteID) AND
        u.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARPACIENTESEXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR PACIENTES EN EXAMEN
CREATE PROCEDURE [dbo].[SPBUSCARPACIENTESEXAMEN] 
    @PacienteID INT = NULL
AS
BEGIN
    SELECT 
        p.PacienteID, 
        u.Nombre AS NombrePaciente,  -- Obtenido desde la tabla Usuarios
        u.Apellido AS ApellidoPaciente
    FROM Pacientes p
    LEFT JOIN Usuarios u ON p.UsuarioID = u.UsuarioID  -- Unir con Usuarios para obtener Nombre, Apellido
    WHERE 
        (@PacienteID IS NULL OR p.PacienteID = @PacienteID) AND
        u.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARRECETAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR RECETAS
CREATE PROCEDURE [dbo].[SPBUSCARRECETAS]
    @RecetaID INT = NULL
AS
BEGIN
    SELECT 
        r.RecetaID,
        r.FechaEmision,
        r.PacienteID,
        r.MedicoID,
        r.ConsultaID,
        er.NombreEstadoReceta  
    FROM 
        Receta r
    INNER JOIN 
        EstadoReceta er ON r.EstadoRecetaID = er.EstadoRecetaID
    WHERE
        (@RecetaID IS NULL OR RecetaID = @RecetaID) AND
        r.EstadoID = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARUSUARIOROL]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA CONSEGUIR EL ROL DEL USUARIO
CREATE PROCEDURE [dbo].[SPBUSCARUSUARIOROL]
    @Email VARCHAR(50), --Recibe el correo
    @RolID INT OUTPUT --Retorna el rol
AS
BEGIN
    SET NOCOUNT ON; --No envia filas afectadas, mejora rendimiento

    SELECT @RolID = RolID
    FROM Usuarios
    WHERE Email = @Email AND EstadoID = 1; 

    IF @RolID IS NULL
        SET @RolID = 0; 
END;
GO
/****** Object:  StoredProcedure [dbo].[SPBUSCARUSUARIOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--PROCEDIMIENTO DE ALMACENAMIENTO PARA BUSCAR Y MOSTRAR USUARIOS
CREATE PROCEDURE [dbo].[SPBUSCARUSUARIOS] 
    @UsuarioID INT = NULL
AS
BEGIN
    SELECT 
        u.UsuarioID, 
        u.Nombre, 
        u.Apellido, 
        u.Email,
        r.Rol AS Rol, 
        CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('HOSPI', u.Clave)) AS Clave 
    FROM Usuarios u
    INNER JOIN Roles r ON u.RolID = r.RolID  -- Unir con la tabla Roles para obtener el nombre del rol
    WHERE 
        (@UsuarioID IS NULL OR u.UsuarioID = @UsuarioID) AND
        u.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');  -- Solo usuarios activos
END;
GO
/****** Object:  StoredProcedure [dbo].[SPCAMBIARDISPONIBILIDADCONSULTA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA CAMBIAR DISPONIBILIDAD DE CONSULTA
CREATE PROCEDURE [dbo].[SPCAMBIARDISPONIBILIDADCONSULTA]
    @ConsultaID INT
AS
BEGIN
    UPDATE Consulta
    SET Bandera = 0
    WHERE ConsultaID = @ConsultaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPCAMBIARESTADOCITA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ESTADO DE CITA A FINALIZADA
CREATE PROCEDURE [dbo].[SPCAMBIARESTADOCITA]
    @CitaID INT
AS
BEGIN
    UPDATE Cita
    SET EstadoCitaID = (SELECT EstadoCitaID FROM EstadoCita WHERE EstadoCita = 'Finalizada')
    WHERE CitaID = @CitaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPCAMBIARESTADOEXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA ENTREGAR EXAMENES REGISTRADO
CREATE PROCEDURE [dbo].[SPCAMBIARESTADOEXAMEN]
    @ExamenID INT
AS
BEGIN
    UPDATE Examen
    SET 
		EstadoExamenID = 3
    WHERE 
        ExamenID = @ExamenID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPCAMBIARESTADORECETA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA CAMBIAR ESTADO RECETA A ENTREGADO
CREATE PROCEDURE [dbo].[SPCAMBIARESTADORECETA]
    @RecetaID INT
AS
BEGIN
    UPDATE Receta
    SET EstadoRecetaID = (SELECT EstadoRecetaID FROM EstadoReceta WHERE NombreEstadoReceta = 'Retirada')
    WHERE RecetaID = @RecetaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPCargarComboBox]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA CARGAR LOS COMBO BOX
CREATE PROCEDURE [dbo].[SPCargarComboBox]
    @TipoComboBox NVARCHAR(50),
    @MedicoID INT = NULL
AS
BEGIN
    IF @TipoComboBox = 'Medicos'
    BEGIN
        SELECT m.MedicoID, u.Nombre + ' ' + u.Apellido AS Medico
        FROM Medico m
        INNER JOIN Usuarios u ON m.UsuarioID = u.UsuarioID
        WHERE u.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
    END

    ELSE IF @TipoComboBox = 'Pacientes'
    BEGIN
        SELECT p.PacienteID, u.Nombre + ' ' + u.Apellido AS Paciente
        FROM Pacientes p
        INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID
        WHERE u.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
    END

    ELSE IF @TipoComboBox = 'Consultorios'
    BEGIN
        SELECT c.ConsultorioID, c.NombreConsultorio
        FROM Consultorio c
		LEFT JOIN Medico m ON c.ConsultorioID = m.ConsultorioID
		WHERE (@MedicoID IS NULL OR m.MedicoID = @MedicoID);
    END

    ELSE IF @TipoComboBox = 'EstadoCita'
    BEGIN
        SELECT ec.EstadoCitaID, ec.EstadoCita
        FROM EstadoCita ec;
    END

    ELSE IF @TipoComboBox = 'Especialidades'
    BEGIN
        SELECT e.EspecialidadID, e.NombreEspecialidad
        FROM Especialidad e
        LEFT JOIN Medico m ON e.EspecialidadID = m.EspecialidadID
        WHERE (@MedicoID IS NULL OR m.MedicoID = @MedicoID);
    END

END;
GO
/****** Object:  StoredProcedure [dbo].[SPCargarHorasMedico]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA CARGAR LAS HORAS DEL MEDICO
CREATE PROCEDURE [dbo].[SPCargarHorasMedico]
    @MedicoID INT = NULL,
    @FechaCita DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @HoraInicio VARCHAR(5);
    DECLARE @HoraFin VARCHAR(5);
    DECLARE @HoraActual VARCHAR(5);

    DECLARE @HorasDisponibles TABLE (Hora VARCHAR(5) PRIMARY KEY);

    IF @MedicoID IS NOT NULL AND @FechaCita IS NOT NULL
    BEGIN

        SELECT 
            @HoraInicio = LEFT(h1.NombreHoraInicio, 5), 
            @HoraFin = LEFT(h2.NombreHoraFin, 5)
        FROM Medico m
        INNER JOIN HoraInicio h1 ON m.HoraInicioID = h1.HoraInicioID
        INNER JOIN HoraFin h2 ON m.HoraFinID = h2.HoraFinID
        WHERE m.MedicoID = @MedicoID;

        SET @HoraActual = @HoraInicio;

        WHILE @HoraActual < @HoraFin
        BEGIN
            INSERT INTO @HorasDisponibles (Hora) VALUES (@HoraActual);

            SET @HoraActual = CONVERT(VARCHAR(5), DATEADD(MINUTE, 30, CAST(@HoraActual AS TIME)), 108);
        END

        SELECT 
            h.Hora
        FROM @HorasDisponibles h
        WHERE h.Hora NOT IN (
            SELECT LEFT(hc.Hora, 5) 
            FROM HorarioCita hc
            INNER JOIN Cita c ON hc.CitaID = c.CitaID
            WHERE c.MedicoID = @MedicoID
            AND hc.FechaCita = @FechaCita
        )
        ORDER BY h.Hora;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SPELIMINAR]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA ELIMINAR
CREATE PROCEDURE [dbo].[SPELIMINAR]
    @Email VARCHAR(50)
AS
BEGIN
    -- Iniciar una transacción para asegurar que ambas actualizaciones ocurran juntas
    BEGIN TRANSACTION;

    -- Actualizar el estado en la tabla Usuarios a 'inactivo'
    UPDATE Usuarios
    SET EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Inactivo')
    WHERE Email = @Email;

    -- Verificar si ambas actualizaciones se hicieron correctamente
    IF @@ERROR = 0
    BEGIN
        -- Confirmar la transacción
        COMMIT TRANSACTION;
        PRINT 'Registro inactivado correctamente.';
    END
    ELSE
    BEGIN
        -- Revertir la transacción si hubo un error
        ROLLBACK TRANSACTION;
        PRINT 'Error al intentar inactivar el registro';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SPELIMINARCITA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA ELIMINAR
CREATE PROCEDURE [dbo].[SPELIMINARCITA]
    @CitaID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Verifica si la cita existe y está activa
        IF EXISTS (
            SELECT 1 
            FROM Cita 
            WHERE CitaID = @CitaID 
              AND EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo')
        )
        BEGIN
            -- Cambia el estado de la cita a "Inactivo"
            UPDATE Cita
            SET EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Inactivo')
            WHERE CitaID = @CitaID;

            COMMIT TRANSACTION;
        END
        ELSE
        BEGIN
            ROLLBACK TRANSACTION;
        END
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPELIMINARCONSULTA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA ELIMINAR CONSULTA
CREATE PROCEDURE [dbo].[SPELIMINARCONSULTA]
    @ConsultaID INT
AS
BEGIN
    UPDATE Consulta
    SET EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Inactivo')
    WHERE ConsultaID = @ConsultaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPELIMINAREXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA ELIMINAR EXAMENES REGISTRADOS
CREATE PROCEDURE [dbo].[SPELIMINAREXAMEN]
    @ExamenID INT
AS
BEGIN
    UPDATE Examen
    SET 
		EstadoID = 2
    WHERE 
        ExamenID = @ExamenID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPELIMINARPACIENTE]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPELIMINARPACIENTE]
    @Email VARCHAR(50)
AS
BEGIN
	-- Iniciar una transacción para asegurar que ambas actualizaciones ocurran juntas
    BEGIN TRANSACTION;

	-- Actualizar el estado en la tabla Usuarios a 'inactivo' 
    UPDATE Usuarios
    SET EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Inactivo') 
    WHERE Email = @Email;

     -- Verificar si ambas actualizaciones se hicieron correctamente
    IF @@ERROR = 0
    BEGIN

        -- Confirmar la transacción
        COMMIT TRANSACTION;
        PRINT 'Usuario y Paciente inactivados correctamente.';
    END
    ELSE
    BEGIN
        -- Revertir la transacción si hubo un error
        ROLLBACK TRANSACTION;
        PRINT 'Error al intentar inactivar el usuario y Paciente.';
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SPELIMINARRECETA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA ELIMINAR RECETAS
CREATE PROCEDURE [dbo].[SPELIMINARRECETA]
    @RecetaID INT
AS
BEGIN
    UPDATE Receta
    SET EstadoID = 2
    WHERE RecetaID = @RecetaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARCITAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA GUARDAR
CREATE PROCEDURE [dbo].[SPINSERTARCITAS]
    @FechaCita DATE,
    @Hora VARCHAR(10),
    @Duracion INT,
    @MotivoCita VARCHAR(255),
    @PacienteID INT,
    @MedicoID INT,
    @EspecialidadID INT,
    @ConsultorioID INT,
    @Comentarios VARCHAR(MAX)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Insertar en la tabla Cita
        INSERT INTO Cita (
            MedicoID,
            PacienteID,
            EspecialidadID,
            ConsultorioID,
            EstadoID,
            EstadoCitaID,
            MotivoCita,
            Comentarios
        )
        VALUES (
            @MedicoID,
            @PacienteID,
            @EspecialidadID,
            @ConsultorioID,
            (SELECT EstadoID FROM Estados WHERE Estado = 'Activo'),
            (SELECT EstadoCitaID FROM EstadoCita WHERE EstadoCita = 'Programada'),  -- Estado siempre "Programada"
            @MotivoCita,
            @Comentarios
        );

        -- Obtener el ID de la cita recién insertada
        DECLARE @CitaID INT = SCOPE_IDENTITY();

        -- Insertar en la tabla HorarioCita
        INSERT INTO HorarioCita (
            CitaID,
            FechaCita,
            Hora,
            Duracion
        )
        VALUES (
            @CitaID,
            @FechaCita,
            @Hora,
            @Duracion
        );

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Manejo de errores y rollback
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARCONSULTA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA INSERTAR CONSULTA
CREATE PROCEDURE [dbo].[SPINSERTARCONSULTA]
	@PacienteID INT,
	@MedicoID INT,
	@FechaConsulta DATETIME,
    @MotivoConsulta VARCHAR(255),
	@Examen BIT,
    @ConsultorioID INT
AS
BEGIN
    INSERT INTO Consulta (     
        PacienteID,
	    MedicoID,
	    FechaConsulta,
        MotivoConsulta,
	    Examen,
		ConsultorioID,
        EstadoID, 
		Bandera
    )
    VALUES (
		@PacienteID,
		@MedicoID,
		@FechaConsulta,
		@MotivoConsulta,
		@Examen,
		@ConsultorioID,
        DEFAULT, 
		1
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTAREXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA INSERTAR EXAMENES
CREATE PROCEDURE [dbo].[SPINSERTAREXAMEN]
	@PacienteID INT,
    @EspecialidadID INT,
	@TipoExamenID INT,
    @FechaExamen DATETIME
AS
BEGIN
    INSERT INTO Examen (     
        PacienteID,
		EspecialidadID,
		TipoExamenID,
		FechaExamen,
        EstadoExamenID,
        EstadoID       
    )
    VALUES (
		@PacienteID,
		@EspecialidadID,
		@TipoExamenID,
		@FechaExamen,
        DEFAULT, 
        DEFAULT 
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTAREXAMENAGENDADO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA INSERTAR EXAMEN AGENDADO
CREATE PROCEDURE [dbo].[SPINSERTAREXAMENAGENDADO]
	@PacienteID INT,
	@MedicoID INT,
	@ConsultaID INT,
    @EspecialidadID INT,
	@TipoExamenID INT,
    @FechaExamen DATETIME
AS
BEGIN
    INSERT INTO Examen (     
        PacienteID,
        MedicoID,
        ConsultaID,
		EspecialidadID,
		TipoExamenID,
		FechaExamen,
        EstadoExamenID, -- Valor predeterminado 'Agendado'
        EstadoID       -- Valor predeterminado 'Activo'
    )
    VALUES (
		@PacienteID,
		@MedicoID,
		@ConsultaID,
		@EspecialidadID,
		@TipoExamenID,
		@FechaExamen,
        DEFAULT, 
        DEFAULT 
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARHISTORIALPACIENTE]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




----------------------------------------------------------
-- CREAR EL PROCESO ALMACENADO SPINSERTARHISTORIALPACIENTE
----------------------------------------------------------
CREATE PROCEDURE [dbo].[SPINSERTARHISTORIALPACIENTE]
    @PacienteID INT,
    @MotivoConsulta VARCHAR(255),
    @Padecimientos VARCHAR(255),
    @Traumatismos VARCHAR(255),
    @CirugiasPrevias VARCHAR(255),
    @MedicacionActual VARCHAR(255),
    @AntecedentesFamiliares VARCHAR(255),
    @Discapacidad VARCHAR(255),
    @Alergia VARCHAR(255),
    @EnfermedadCronica VARCHAR(255),
    @Observaciones TEXT
AS
BEGIN
    -- Iniciar una transacción
    BEGIN TRANSACTION;

    -- Verificar que el paciente existe
    IF NOT EXISTS (SELECT 1 FROM Pacientes WHERE PacienteID = @PacienteID)
    BEGIN
        RAISERROR('Error: No existe un paciente con ese ID.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Insertar en la tabla
    BEGIN TRY
        INSERT INTO HistorialPaciente (
            PacienteID, MotivoConsulta, Padecimientos, Traumatismos, 
            CirugiasPrevias, MedicacionActual, AntecedentesFamiliares, 
            Discapacidad, Alergia, EnfermedadCronica, Observaciones
        ) VALUES (
            @PacienteID, @MotivoConsulta, @Padecimientos, @Traumatismos, 
            @CirugiasPrevias, @MedicacionActual, @AntecedentesFamiliares, 
            @Discapacidad, @Alergia, @EnfermedadCronica, @Observaciones
        );
        
        -- Si todo fue exitoso, confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error, revertir todos los cambios
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;
        

		 -- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARMEDICAMENTORECETA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA INSERTAR MEDICAMENTOS
CREATE PROCEDURE [dbo].[SPINSERTARMEDICAMENTORECETA]
    @RecetaID INT,
    @MedicamentosID INT,
    @Indicaciones VARCHAR(255)
AS
BEGIN
    INSERT INTO MedicamentoReceta (
        RecetaID,
        MedicamentosID,
        Indicaciones
    )
    VALUES (
        @RecetaID,
        @MedicamentosID,
        @Indicaciones
    );
    
    -- Confirmación del último ID insertado en la tabla MedicamentoReceta
    DECLARE @MedicamentoRecetaID INT = SCOPE_IDENTITY();
    SELECT @MedicamentoRecetaID AS MedicamentoRecetaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARMEDICO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA INSERTAR MEDICO
CREATE PROCEDURE [dbo].[SPINSERTARMEDICO]
    @Email VARCHAR(50),         
    @Identificacion VARCHAR(20), 
    @EspecialidadID INT,        
    @HoraInicioID INT,    
    @HoraFinID INT,       
	@ConsultorioID INT,
    @Telefono VARCHAR(15)    
AS
BEGIN
    DECLARE @UsuarioID INT;
    DECLARE @MedicoID INT;
    DECLARE @TelefonoID INT;

    -- Iniciar una transacción
    BEGIN TRANSACTION;

    BEGIN TRY
        -- Obtener el UsuarioID basado en el Email
        SET @UsuarioID = (SELECT UsuarioID FROM Usuarios WHERE Email = @Email);
        
        -- Verificar que el usuario existe
        IF @UsuarioID IS NULL
        BEGIN
            RAISERROR('Error: No existe un usuario con ese email.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Verificar si el teléfono ya existe en la tabla Telefonos
        SELECT @TelefonoID = TelefonoID FROM Telefonos WHERE Telefono = @Telefono;

        -- Si el teléfono ya existe, lanzar un error y revertir la transacción
        IF @TelefonoID IS NOT NULL
        BEGIN
            RAISERROR('Error: El teléfono ya está registrado en el sistema.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Insertar en la tabla Medico
        INSERT INTO Medico (UsuarioID, Identificacion, EspecialidadID, HoraInicioID, HoraFinID, ConsultorioID)
        VALUES (@UsuarioID, @Identificacion, @EspecialidadID, @HoraInicioID, @HoraFinID, @ConsultorioID);

        -- Obtener el ID del médico recién insertado
        SET @MedicoID = SCOPE_IDENTITY();

        -- Insertar el teléfono ya que no existe
        INSERT INTO Telefonos (Telefono) VALUES (@Telefono);
        SET @TelefonoID = SCOPE_IDENTITY();

        -- Insertar en la tabla MedicoTelefono
        INSERT INTO MedicoTelefono (MedicoID, TelefonoID) VALUES (@MedicoID, @TelefonoID);

        -- Si todo fue exitoso, confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Si ocurre algún error, revertir todos los cambios
        ROLLBACK TRANSACTION;

        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        -- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARPACIENTE]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPINSERTARPACIENTE]
    @Email VARCHAR(50),          
    @Identificacion VARCHAR(20), 
    @GeneroID INT,               
    @FechaNacimiento DATE,       
    @Residencia VARCHAR(100),    
    @Telefono VARCHAR(15),
	@MunicipioID INT
AS
BEGIN
	-- Declarar variables
    DECLARE @UsuarioID INT; 
    DECLARE @PacienteID INT;
    DECLARE @TelefonoID INT;

	-- Comezar transaccion
    BEGIN TRANSACTION;

	-- Ty Catch de progra
    BEGIN TRY
	-- Obtener el UsuarioID basado en el Email
        SET @UsuarioID = (SELECT UsuarioID FROM Usuarios WHERE Email = @Email);
        
		-- Verificar que el usuario existe
        IF @UsuarioID IS NULL
        BEGIN
            RAISERROR('Error: No existe un usuario con ese email.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

		-- Verificar si el teléfono ya existe en la tabla Telefonos
        SELECT @TelefonoID = TelefonoID FROM Telefonos WHERE Telefono = @Telefono;

		-- Si el teléfono ya existe, lanzar un error y revertir la transacción
        IF @TelefonoID IS NOT NULL
        BEGIN
            RAISERROR('Error: El teléfono ya está registrado.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END


		-- Insertar en la tabla Medico
        INSERT INTO Pacientes (UsuarioID, Identificacion, GeneroID, FechaNacimiento, Residencia, MunicipioID)
        VALUES (@UsuarioID, @Identificacion, @GeneroID, @FechaNacimiento, @Residencia,@MunicipioID);

		-- Obtener el ID del paciente recién insertado
        SET @PacienteID = SCOPE_IDENTITY();

		-- Insertar el teléfono ya que no existe
        INSERT INTO Telefonos (Telefono) VALUES (@Telefono);
        SET @TelefonoID = SCOPE_IDENTITY();

		--Insertar en la tabla MedicoTelefono
        INSERT INTO PacienteTelefono (PacienteID, TelefonoID) VALUES (@PacienteID, @TelefonoID);

		-- Si todo fue exitoso, confirmar la transacción
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
		-- Si ocurre algún error, revertir todos los cambios
        ROLLBACK TRANSACTION;

		DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

		-- Capturar los detalles del error
        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);   
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARRECETA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA INSERTAR RECETAS
CREATE PROCEDURE [dbo].[SPINSERTARRECETA]
    @FechaEmision DATETIME,
    @PacienteID INT,
    @MedicoID INT,
    @ConsultaID INT
AS
BEGIN
    INSERT INTO Receta (
        FechaEmision,
        PacienteID,
        MedicoID,
        ConsultaID,
        EstadoRecetaID,
        EstadoID
    )
    VALUES (
        @FechaEmision,
        @PacienteID,
        @MedicoID,
        @ConsultaID,
        1,  -- EstadoRecetaID se establece como 1 (Pendiente)
        1   -- EstadoID se establece como 1 (Activo)
    );
    
    -- Confirmación del último ID insertado en la tabla Receta
    DECLARE @RecetaID INT = SCOPE_IDENTITY();
    SELECT @RecetaID AS RecetaID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPINSERTARUSUARIO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO PARA ALMACENAR USUARIO 
CREATE PROCEDURE [dbo].[SPINSERTARUSUARIO]
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Email VARCHAR(50),
    @RolID INT,
    @Clave VARCHAR(100),
    @EstadoID INT = NULL 
AS
BEGIN
    DECLARE @ERROR INT = 0;
    DECLARE @ID INT = 0;

    BEGIN TRY
        -- Asignar por defecto Activo
        IF @EstadoID IS NULL
        BEGIN
            SET @EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
        END

        --Validacion de datos completos
        IF ((LEN(@Nombre) = 0) OR (LEN(@Apellido) = 0) OR (LEN(@Email) = 0) OR (LEN(@RolID) = 0) OR (LEN(@Clave) = 0))
        BEGIN 
            SET @ERROR = 1;
        END

        IF @ERROR = 0
        BEGIN
            -- Proceso para insertar
            INSERT INTO Usuarios(Nombre, Apellido, Email, RolID, Clave, EstadoID)
            VALUES(
                @Nombre,
                @Apellido,
                @Email,
                @RolID,
                ENCRYPTBYPASSPHRASE('HOSPI', @Clave),
                @EstadoID);

            -- Obtener el ID del recien registro
            SET @ID = SCOPE_IDENTITY();
        END

        -- Retornar el ID
        SELECT @ID;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        DECLARE @ErrorSeverity INT;
        DECLARE @ErrorState INT;

        SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();

        -- Si se da error por los parametros unicos devolver error
        IF ERROR_NUMBER() = 2627 OR ERROR_NUMBER() = 2601
        BEGIN
            RAISERROR('Error: El correo electrónico ya existe en el sistema', @ErrorSeverity, @ErrorState);
        END
        ELSE
        BEGIN
            RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
        END
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMEDICAMENTOSRECETA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR MEDICAMENTOS
CREATE PROCEDURE [dbo].[SPMEDICAMENTOSRECETA]
    @RecetaID INT
AS
BEGIN
    SELECT 
        MR.RecetaID,
        MED.NombreMedicamento,
        MR.Indicaciones
    FROM 
        MedicamentoReceta MR
    INNER JOIN Medicamentos MED ON MR.MedicamentosID = MED.MedicamentosID
    WHERE 
        MR.RecetaID = @RecetaID
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARCITAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR CITAS
CREATE PROCEDURE [dbo].[SPMOSTRARCITAS]
    @MedicoID INT = NULL,
    @FechaCita DATE = NULL,
    @PacienteID INT = NULL
AS
BEGIN
    SELECT 
        c.CitaID,
        e.EspecialidadID, 
        e.NombreEspecialidad AS Especialidad,
        m.MedicoID, 
        CONCAT(mu.Nombre, ' ', mu.Apellido) AS Medico,
        p.PacienteID, 
        CONCAT(pu.Nombre, ' ', pu.Apellido) AS Paciente,
        hc.FechaCita,
        hc.Hora,
        hc.Duracion,
        con.ConsultorioID, 
        con.NombreConsultorio AS Consultorio,
        c.MotivoCita,
        ec.EstadoCitaID, 
        ec.EstadoCita AS Estado,
        c.Comentarios
    FROM Cita c
    INNER JOIN HorarioCita hc ON c.CitaID = hc.CitaID
    INNER JOIN Especialidad e ON c.EspecialidadID = e.EspecialidadID
    INNER JOIN Medico m ON c.MedicoID = m.MedicoID
    INNER JOIN Usuarios mu ON m.UsuarioID = mu.UsuarioID
    INNER JOIN Pacientes p ON c.PacienteID = p.PacienteID
    INNER JOIN Usuarios pu ON p.UsuarioID = pu.UsuarioID
    INNER JOIN Consultorio con ON c.ConsultorioID = con.ConsultorioID
    INNER JOIN EstadoCita ec ON c.EstadoCitaID = ec.EstadoCitaID
    WHERE (@MedicoID IS NULL OR c.MedicoID = @MedicoID)
      AND (@FechaCita IS NULL OR hc.FechaCita = @FechaCita)
      AND (@PacienteID IS NULL OR c.PacienteID = @PacienteID)
      AND c.EstadoID = (SELECT EstadoID FROM Estados WHERE Estado = 'Activo');
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARCITASCONSULTA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO PARA MOSTRAR CITAS EN VENTANA CONSULTA
CREATE PROCEDURE [dbo].[SPMOSTRARCITASCONSULTA]
AS
BEGIN
    SELECT 
        c.CitaID,
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID,
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico,
        con.ConsultorioID,
        con.NombreConsultorio AS NombreConsultorio
    FROM 
        Cita c
    INNER JOIN 
        Pacientes p ON c.PacienteID = p.PacienteID
    INNER JOIN 
        Usuarios up ON p.UsuarioID = up.UsuarioID
    INNER JOIN 
        Medico m ON c.MedicoID = m.MedicoID
    INNER JOIN 
        Usuarios um ON m.UsuarioID = um.UsuarioID
    INNER JOIN 
        Consultorio con ON c.ConsultorioID = con.ConsultorioID
    WHERE 
        c.EstadoID = 1 
        AND c.EstadoCitaID IN (1, 2); 
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARCONSULTAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR CONSULTAS
CREATE PROCEDURE [dbo].[SPMOSTRARCONSULTAS]
AS
BEGIN
    SELECT 
        c.ConsultaID,
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID,
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico,
        con.ConsultorioID,
        con.NombreConsultorio AS NombreConsultorio,
		c.FechaConsulta,
		c.Examen,
		c.MotivoConsulta
    FROM 
        Consulta c
    INNER JOIN 
        Pacientes p ON c.PacienteID = p.PacienteID
    INNER JOIN 
        Usuarios up ON p.UsuarioID = up.UsuarioID
    INNER JOIN 
        Medico m ON c.MedicoID = m.MedicoID
    INNER JOIN 
        Usuarios um ON m.UsuarioID = um.UsuarioID
    INNER JOIN 
        Consultorio con ON c.ConsultorioID = con.ConsultorioID
    WHERE 
        c.EstadoID = 1 
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARCONSULTASEXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR CONSULTAS CON EXAMEN
CREATE PROCEDURE [dbo].[SPMOSTRARCONSULTASEXAMEN]
AS
BEGIN
    SELECT 
        c.ConsultaID,
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID,
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico
    FROM 
        Consulta c
    INNER JOIN 
        Pacientes p ON c.PacienteID = p.PacienteID
    INNER JOIN 
        Usuarios up ON p.UsuarioID = up.UsuarioID
    INNER JOIN 
        Medico m ON c.MedicoID = m.MedicoID
    INNER JOIN 
        Usuarios um ON m.UsuarioID = um.UsuarioID
    WHERE 
        c.EstadoID = 1 
        AND c.Examen = 1 -- Solo consultas donde Examen es true
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARCONSULTASRECETAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENMAIENTO PARA MOSTRAR CONSULTAS
CREATE PROCEDURE [dbo].[SPMOSTRARCONSULTASRECETAS]
AS
BEGIN
    SELECT 
        c.ConsultaID,
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente,
        m.MedicoID,
        um.Nombre AS NombreMedico,
        um.Apellido AS ApellidoMedico
    FROM 
        Consulta c
    INNER JOIN 
        Pacientes p ON c.PacienteID = p.PacienteID
    INNER JOIN 
        Usuarios up ON p.UsuarioID = up.UsuarioID
    INNER JOIN 
        Medico m ON c.MedicoID = m.MedicoID
    INNER JOIN 
        Usuarios um ON m.UsuarioID = um.UsuarioID
    WHERE 
        c.EstadoID = 1 
        AND c.Bandera = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRAREXAMENESAGENDADOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR EXAMENES AGENDADOS
CREATE PROCEDURE [dbo].[SPMOSTRAREXAMENESAGENDADOS]
AS
BEGIN
    SELECT 
        E.ExamenID,
		E.PacienteID,
		E.MedicoID,
        TE.TipoExamen AS TipoExamenNombre,
        ES.NombreEspecialidad AS EspecialidadNombre,
        E.FechaExamen,
        EE.EstadoExamen AS EstadoExamenNombre,
        E.EstadoID
    FROM 
        Examen E
    INNER JOIN 
        TipoExamenes TE ON E.TipoExamenID = TE.TipoExamenID
    INNER JOIN 
        Especialidad ES ON E.EspecialidadID = ES.EspecialidadID
    INNER JOIN 
        EstadoExamen EE ON E.EstadoExamenID = EE.EstadoExamenID
    WHERE 
        E.EstadoID = 1 
        AND EE.EstadoExamen = 'Agendado' 
    ORDER BY 
        E.FechaExamen;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRAREXAMENESDETALLADOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR EXAMENES DETALLADOS
CREATE PROCEDURE [dbo].[SPMOSTRAREXAMENESDETALLADOS]
    @ExamenID INT
AS
BEGIN
    SELECT 
        E.ExamenID,
        P.PacienteID,
        UP.Nombre AS NombrePaciente,
        UP.Apellido AS ApellidoPaciente,
        M.MedicoID,
        UM.Nombre AS NombreMedico,
        UM.Apellido AS ApellidoMedico,
        TE.TipoExamen AS TipoExamenNombre,
        ES.NombreEspecialidad AS EspecialidadNombre,
        E.FechaExamen,
        E.Resultado,
        EE.EstadoExamen AS EstadoExamenNombre,
        E.EstadoID,
        E.FechaResultado
    FROM 
        Examen E
    INNER JOIN 
        Pacientes P ON E.PacienteID = P.PacienteID
    INNER JOIN 
        Usuarios UP ON P.UsuarioID = UP.UsuarioID
    LEFT JOIN 
        Medico M ON E.MedicoID = M.MedicoID
    LEFT JOIN 
        Usuarios UM ON M.UsuarioID = UM.UsuarioID
    INNER JOIN 
        TipoExamenes TE ON E.TipoExamenID = TE.TipoExamenID
    INNER JOIN 
        Especialidad ES ON E.EspecialidadID = ES.EspecialidadID
    INNER JOIN 
        EstadoExamen EE ON E.EstadoExamenID = EE.EstadoExamenID
    WHERE 
        E.EstadoID = 1 
        AND (EE.EstadoExamen = 'Pendiente' OR EE.EstadoExamen = 'Entregado')
        AND E.ExamenID = @ExamenID -- Filtro por ExamenID
    ORDER BY 
        E.FechaExamen;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRAREXAMENESREGISTRADOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR EXAMENES REGISTRADOS
CREATE PROCEDURE [dbo].[SPMOSTRAREXAMENESREGISTRADOS]
AS
BEGIN
    SELECT 
        E.ExamenID,
        E.PacienteID,
        E.MedicoID,
        TE.TipoExamen AS TipoExamenNombre,
        ES.NombreEspecialidad AS EspecialidadNombre,
        E.FechaExamen,
        E.Resultado,
        EE.EstadoExamen AS EstadoExamenNombre,
        E.EstadoID,
        E.FechaResultado
    FROM 
        Examen E
    INNER JOIN 
        TipoExamenes TE ON E.TipoExamenID = TE.TipoExamenID
    INNER JOIN 
        Especialidad ES ON E.EspecialidadID = ES.EspecialidadID
    INNER JOIN 
        EstadoExamen EE ON E.EstadoExamenID = EE.EstadoExamenID
    WHERE 
        E.EstadoID = 1 
        AND (EE.EstadoExamen = 'Pendiente' OR EE.EstadoExamen = 'Entregado')
    ORDER BY 
        E.FechaExamen;
END;
---------------
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARHISTORIALPACIENTES]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SPMOSTRARHISTORIALPACIENTES]
AS
BEGIN
    -- Seleccionar todos los datos del historial junto con el nombre y apellido del paciente, filtrando solo usuarios activos
    SELECT 
		H.HistorialPacienteID,
        H.PacienteID,
        U.Nombre AS NombrePaciente,
        U.Apellido AS ApellidoPaciente,
        H.MotivoConsulta,
        H.Padecimientos,
        H.Traumatismos,
        H.CirugiasPrevias,
        H.MedicacionActual,
        H.AntecedentesFamiliares,
        H.Discapacidad,
        H.Alergia,
        H.EnfermedadCronica,
        H.Observaciones
    FROM 
        HistorialPaciente H
    INNER JOIN 
        Pacientes P ON H.PacienteID = P.PacienteID
    INNER JOIN 
        Usuarios U ON P.UsuarioID = U.UsuarioID
    WHERE 
        U.EstadoID = 1 -- Filtrar solo usuarios activos
    ORDER BY 
        H.PacienteID; 
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARMEDICAMENTOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR MEDICAMENTOS
CREATE PROCEDURE [dbo].[SPMOSTRARMEDICAMENTOS]
AS
BEGIN
    SELECT 
        MedicamentosID,
        NombreMedicamento,
        Cantidad
    FROM 
        Medicamentos;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARMEDICO]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR MEDICOS
CREATE PROCEDURE [dbo].[SPMOSTRARMEDICO]
AS
BEGIN
    WITH MedicosConTelefono AS (
        SELECT 
            M.MedicoID, 
            U.Nombre AS NombreMedico,  
            U.Apellido AS ApellidoMedico,  
            U.Email AS EmailMedico,  
            M.Identificacion,  
            E.EspecialidadID,  
            E.NombreEspecialidad AS NombreEspecialidad,
            T.TelefonoID,  
            T.Telefono AS Telefono,  
            I.HoraInicioID,  
			I.NombreHoraInicio AS NombreHoraInicio,
            F.HoraFinID,  
			F.NombreHoraFin AS NombreHoraFin,
			C.ConsultorioID,  
			C.NombreConsultorio AS NombreConsultorio,
            U.EstadoID AS Estado,  
            ROW_NUMBER() OVER (PARTITION BY M.MedicoID ORDER BY T.TelefonoID) AS RN  
        FROM Medico M
        LEFT JOIN Usuarios U ON M.UsuarioID = U.UsuarioID  
        LEFT JOIN MedicoTelefono MT ON M.MedicoID = MT.MedicoID
        LEFT JOIN Telefonos T ON MT.TelefonoID = T.TelefonoID
        LEFT JOIN Especialidad E ON M.EspecialidadID = E.EspecialidadID
		LEFT JOIN HoraInicio I ON M.HoraInicioID = I.HoraInicioID
		LEFT JOIN HoraFin F ON M.HoraFinID = F.HoraFinID
		LEFT JOIN Consultorio C ON M.ConsultorioID = C.ConsultorioID
        WHERE U.EstadoID = 1  
    )
    SELECT 
        MedicoID, 
        NombreMedico,  
        ApellidoMedico,  
        EmailMedico,  
        Identificacion,  
        EspecialidadID,  
        NombreEspecialidad,
        TelefonoID,  
        Telefono,  
        HoraInicioID, 
		NombreHoraInicio,
        HoraFinID,
		NombreHoraFin,
		ConsultorioID,
		NombreConsultorio
    FROM MedicosConTelefono
    WHERE RN = 1;  -- Filtrar para mostrar solo el primer teléfono
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARPACIENTES]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------------------------------------------------

------------------------------------------------------------------------------
			-- CREAR PROCESO ALMACENADO PARA MOSTRAR PACIENTE
------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[SPMOSTRARPACIENTES]
AS
BEGIN
	WITH PacientesConTelefono AS (
		SELECT 
			p.PacienteID AS ID, 
			u.Nombre, 
			u.Apellido, 
			u.Email,
			p.Identificacion, 
			g.NombreGenero AS Genero, 
			p.FechaNacimiento, 
			t.Telefono,
			p.Residencia,
			m.Municipio,
			d.Departamento,
			ROW_NUMBER() OVER (PARTITION BY p.PacienteID ORDER BY T.TelefonoID) AS RN  -- Asigna un número a cada teléfono por médico
		FROM Pacientes p
		INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID
		INNER JOIN Genero g ON p.GeneroID = g.GeneroID
		LEFT JOIN PacienteTelefono pt ON p.PacienteID = pt.PacienteID
		LEFT JOIN Telefonos t ON pt.TelefonoID = t.TelefonoID
		LEFT JOIN Municipios m ON p.MunicipioID = m.MunicipioID
		LEFT JOIN Departamentos d ON m.DepartamentoID = d.DepartamentoID
		WHERE u.EstadoID = 1  -- Solo mostrar médicos con estado activo
	)
	SELECT
		ID,
		Nombre,
		Apellido,
		Email,
		Identificacion,
		Genero,
		FechaNacimiento,
		Telefono,
		ReSidencia,
		Municipio,
		Departamento
	FROM PacientesConTelefono
	WHERE RN = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARPACIENTESAEXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR PACIENTES EN EXAMEN
CREATE PROCEDURE [dbo].[SPMOSTRARPACIENTESAEXAMEN]
AS
BEGIN
    SELECT 
        p.PacienteID,
        up.Nombre AS NombrePaciente,
        up.Apellido AS ApellidoPaciente
    FROM 
        Pacientes p
    INNER JOIN 
        Usuarios up ON p.UsuarioID = up.UsuarioID
    WHERE 
        up.EstadoID = 1; -- Estado activo para pacientes
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARRECETADETALLADA]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR RECETAS DETALLADAS
CREATE PROCEDURE [dbo].[SPMOSTRARRECETADETALLADA]
    @RecetaID INT
AS
BEGIN
    SELECT 
        R.RecetaID,
        R.FechaEmision,
        P.PacienteID,
        UP.Nombre AS NombrePaciente,
        UP.Apellido AS ApellidoPaciente,
        M.MedicoID,
        UM.Nombre AS NombreMedico,
        UM.Apellido AS ApellidoMedico,
        R.ConsultaID,
        ER.NombreEstadoReceta AS EstadoRecetaNombre,
        R.EstadoID
    FROM 
        Receta R
    INNER JOIN 
        Pacientes P ON R.PacienteID = P.PacienteID
    INNER JOIN 
        Usuarios UP ON P.UsuarioID = UP.UsuarioID
    LEFT JOIN 
        Medico M ON R.MedicoID = M.MedicoID
    LEFT JOIN 
        Usuarios UM ON M.UsuarioID = UM.UsuarioID
    INNER JOIN 
        EstadoReceta ER ON R.EstadoRecetaID = ER.EstadoRecetaID
    WHERE 
        R.RecetaID = @RecetaID -- Filtro por RecetaID específico
    ORDER BY 
        R.FechaEmision;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARRECETAS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR RECETAS
CREATE PROCEDURE [dbo].[SPMOSTRARRECETAS]
AS
BEGIN
    SELECT 
        r.RecetaID,
        r.FechaEmision,
        r.PacienteID,
        r.MedicoID,
        r.ConsultaID,
        er.NombreEstadoReceta 
    FROM 
        Receta r
    INNER JOIN 
        EstadoReceta er ON r.EstadoRecetaID = er.EstadoRecetaID
    WHERE 
        r.EstadoID = 1;
END;
GO
/****** Object:  StoredProcedure [dbo].[SPMOSTRARUSUARIOS]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA MOSTRAR USUARIOS
CREATE PROCEDURE [dbo].[SPMOSTRARUSUARIOS]
AS
BEGIN
    SELECT 
        u.UsuarioID, 
        u.Nombre, 
        u.Apellido,
        u.Email,
        r.RolID,
        r.Rol AS Rol,  
        CONVERT(VARCHAR, DECRYPTBYPASSPHRASE('HOSPI', u.Clave)) AS Clave,
        e.Estado AS Estado 
    FROM Usuarios u
    INNER JOIN Roles r ON u.RolID = r.RolID  
    INNER JOIN Estados e ON u.EstadoID = e.EstadoID 
    WHERE e.Estado = 'Activo'  -- Solo mostrar usuarios con estado activo
END;
GO
/****** Object:  StoredProcedure [dbo].[SPREGISTRARRESULTADOSEXAMEN]    Script Date: 12/6/2025 17:42:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--PROCEDIMIENTO DE ALMACENAMIENTO PARA REGISTRAR RESULTADOS DE EXAMENES
CREATE PROCEDURE [dbo].[SPREGISTRARRESULTADOSEXAMEN]
    @ExamenID INT,
    @Resultado TEXT,
    @FechaResultado DATETIME
AS
BEGIN
    UPDATE Examen
    SET 
        Resultado = @Resultado,
        FechaResultado = @FechaResultado,
		EstadoExamenID = 2
    WHERE 
        ExamenID = @ExamenID;
END;
GO
USE [master]
GO
ALTER DATABASE [HOSPIPLUS] SET  READ_WRITE 
GO
