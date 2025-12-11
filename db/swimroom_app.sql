-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 11-12-2025 a las 01:21:59
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `swimroom_app`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL,
  `Apellido` varchar(100) NOT NULL,
  `Email` varchar(200) NOT NULL,
  `PasswordHash` varchar(256) NOT NULL,
  `FechaRegistro` datetime(6) NOT NULL,
  `Activo` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Nombre`, `Apellido`, `Email`, `PasswordHash`, `FechaRegistro`, `Activo`) VALUES
(1, 'Wilthon', 'Baque', 'baquewilthon@gmail.com', '1165EDCC7C0420E90C6FD66EBFFB31BC75FD7AB1BCEAA6E6284869472D133A73', '2025-12-10 18:16:34.137341', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ventasdiferidas`
--

CREATE TABLE `ventasdiferidas` (
  `Id` int(11) NOT NULL,
  `ClienteNombre` longtext NOT NULL,
  `MontoCompra` decimal(65,30) NOT NULL,
  `NumCuotas` int(11) NOT NULL,
  `IngresoMensual` decimal(65,30) NOT NULL,
  `DiasAtrasoMax` int(11) NOT NULL,
  `PorcentajeCuotasPagadas` double NOT NULL,
  `ProbabilidadMora` double NOT NULL,
  `NivelRiesgo` longtext NOT NULL,
  `FechaRegistro` datetime(6) NOT NULL,
  `Cedula` longtext DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `ventasdiferidas`
--

INSERT INTO `ventasdiferidas` (`Id`, `ClienteNombre`, `MontoCompra`, `NumCuotas`, `IngresoMensual`, `DiasAtrasoMax`, `PorcentajeCuotasPagadas`, `ProbabilidadMora`, `NivelRiesgo`, `FechaRegistro`, `Cedula`) VALUES
(1, 'Baque Wilthon', 200.000000000000000000000000000000, 3, 600.000000000000000000000000000000, 10, 80, 0.15900053918657428, 'bajo', '2025-12-07 22:26:57.340622', NULL),
(2, 'Ana Torres', 120.000000000000000000000000000000, 3, 800.000000000000000000000000000000, 5, 100, 0.08, 'bajo', '2025-12-07 22:35:06.000000', NULL),
(3, 'Carlos Pérez', 300.000000000000000000000000000000, 6, 650.000000000000000000000000000000, 45, 60, 0.82, 'alto', '2025-12-07 22:35:06.000000', NULL),
(4, 'María López', 80.000000000000000000000000000000, 2, 500.000000000000000000000000000000, 0, 100, 0.05, 'bajo', '2025-12-07 22:35:06.000000', NULL),
(5, 'Jorge Silva', 250.000000000000000000000000000000, 5, 550.000000000000000000000000000000, 20, 75, 0.55, 'medio', '2025-12-07 22:35:06.000000', NULL),
(6, 'Lucía Herrera', 400.000000000000000000000000000000, 8, 700.000000000000000000000000000000, 60, 50, 0.9, 'alto', '2025-12-07 22:35:06.000000', NULL),
(7, 'Pedro Gómez', 180.000000000000000000000000000000, 4, 620.000000000000000000000000000000, 10, 90, 0.2, 'bajo', '2025-12-07 22:35:06.000000', NULL),
(8, 'Diana Ruiz', 220.000000000000000000000000000000, 3, 480.000000000000000000000000000000, 35, 65, 0.7, 'medio', '2025-12-07 22:35:06.000000', NULL),
(9, 'Gema Solange Vega Pereira', 1000.000000000000000000000000000000, 6, 1000.000000000000000000000000000000, 0, 10, 1, 'alto', '2025-12-07 22:50:37.818219', '2300899255');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20251208031340_InitialCreate', '9.0.0'),
('20251208034808_AddCedulaToVentas', '9.0.0'),
('20251210231141_AddUsuarios', '9.0.0');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `ventasdiferidas`
--
ALTER TABLE `ventasdiferidas`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `ventasdiferidas`
--
ALTER TABLE `ventasdiferidas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
