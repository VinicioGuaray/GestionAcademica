-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 01-08-2022 a las 18:03:15
-- Versión del servidor: 10.4.21-MariaDB
-- Versión de PHP: 7.4.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `uenp_gestionacademico`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `asistencias`
--

CREATE TABLE `asistencias` (
  `Asistencia_Id` int(11) NOT NULL,
  `asistencia` varchar(2) NOT NULL,
  `observacion` varchar(200) NOT NULL,
  `fecha` date NOT NULL,
  `hora` time NOT NULL,
  `puntuacion` int(11) NOT NULL,
  `Usuario_Id` int(11) NOT NULL,
  `Curso_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `asistencias`
--

INSERT INTO `asistencias` (`Asistencia_Id`, `asistencia`, `observacion`, `fecha`, `hora`, `puntuacion`, `Usuario_Id`, `Curso_Id`) VALUES
(1, 'si', 's/n', '2022-07-27', '19:30:17', 0, 3, 3),
(2, 'si', 's/n', '2022-07-28', '19:30:17', 0, 8, 1),
(3, 'si', '', '2022-07-04', '19:30:17', 0, 3, 3),
(4, 'si', '', '2022-07-27', '19:30:17', 0, 8, 1),
(5, 'si', '', '2022-07-28', '19:30:17', 0, 17, 5),
(6, 'No', 's/n', '2022-07-30', '838:59:59', 0, 17, 5),
(7, 'Si', 's/n', '2022-07-30', '838:59:59', 0, 18, 5),
(8, 'No', 's/n', '2022-07-30', '05:13:21', 0, 17, 5),
(9, 'Si', 's/n', '2022-07-30', '05:13:21', 0, 18, 5),
(10, 'No', 's/n', '2022-08-01', '12:35:41', 0, 13, 4),
(11, 'Si', 's/n', '2022-08-01', '12:48:38', 10, 13, 4),
(12, 'No', 's/n', '2022-08-01', '12:48:50', 0, 13, 4),
(13, 'No', 's/n', '2022-08-01', '11:38:43', 0, 19, 2),
(14, 'Si', 's/n', '2022-08-01', '11:38:43', 10, 20, 2),
(15, 'No', 's/n', '2022-08-01', '11:43:55', 0, 21, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `cursos`
--

CREATE TABLE `cursos` (
  `Curso_Id` int(11) NOT NULL,
  `curso` varchar(20) NOT NULL,
  `paralelo` char(2) NOT NULL,
  `aula` varchar(10) NOT NULL,
  `Ciclo` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `cursos`
--

INSERT INTO `cursos` (`Curso_Id`, `curso`, `paralelo`, `aula`, `Ciclo`) VALUES
(1, '8vo', 'A', 'A-202', '2021-2022'),
(2, '9no ', 'A', 'A-202', '2021-2022'),
(3, '10mo', 'A', 'B-20', '2021-2022'),
(4, '1ro', 'A', 'A -2022', '2021-2022'),
(5, '2do', 'A', 'A-2023', '2021-2022'),
(6, '3ro', 'A', 'A-2024', '2021-2022');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `gamificaciones`
--

CREATE TABLE `gamificaciones` (
  `Gamificacion_Id` int(11) NOT NULL,
  `Curso_Id` int(11) NOT NULL,
  `Asistencia_Id` int(11) NOT NULL,
  `Usuario_Id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `Rol_Id` int(11) NOT NULL,
  `Tipo` varchar(20) NOT NULL,
  `Nivel` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`Rol_Id`, `Tipo`, `Nivel`) VALUES
(1, 'administrador', 1),
(2, 'profesor', 2),
(3, 'alumno', 3),
(4, 'representante', 4);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Usuario_Id` int(11) NOT NULL,
  `Nombre` varchar(60) NOT NULL,
  `Apellido` varchar(60) NOT NULL,
  `correo` varchar(200) NOT NULL,
  `fechaNaci` date DEFAULT NULL,
  `cedula` varchar(10) NOT NULL,
  `telefono` varchar(10) NOT NULL,
  `direccion` varchar(50) NOT NULL,
  `activo` bit(1) NOT NULL,
  `password` varchar(200) NOT NULL,
  `Rol_Id` int(11) NOT NULL,
  `Curso_Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Usuario_Id`, `Nombre`, `Apellido`, `correo`, `fechaNaci`, `cedula`, `telefono`, `direccion`, `activo`, `password`, `Rol_Id`, `Curso_Id`) VALUES
(1, 'Jose', 'Garcia', 'jose@gmail.com', '2019-06-20', '0202124632', '0967463020', 'Guamani', b'1', '$2y$10$1eHsh44qw2YPfUIwdvxVXehrj23q4I6x0uhNC.f8rkCdE43XZIWEq', 1, 1),
(3, 'vinicio', 'Guaray', 'vinicioguaray@gmail.com', '2012-06-12', '0202124632', '0967463020', 'Guamani', b'1', '$2y$10$1eHsh44qw2YPfUIwdvxVXehrj23q4I6x0uhNC.f8rkCdE43XZIWEq', 1, 3),
(7, 'jose', 'lema', 'vinicioguaray@hotmail.com', NULL, '0202124632', '0987654321', 'san jose', b'1', '$2y$10$SJx.IVJ9SAWTuzxezpKnm.GYXH8mc/y6rc6vhwT9OxoN3Zpau4.wC', 2, 1),
(8, 'jose', 'lema', 'vinicioguaray@hotmail.com', NULL, '0202124632', '0987654321', 'san jose', b'1', '$2y$10$JbORGKvThDnnnCVIjvCOHe.c5ciPDdrV8e4hO0vwPJN.5h5K9C64y', 2, 1),
(12, 'vini', 'vini', 'jhj', NULL, '0987654321', '0987654324', 'jh', b'1', '$2y$10$2LDCYldBN1m6ck2TFYCgIOS02H5oCVO7TrB3ami9CCC85I1RmhdlO', 3, 6),
(13, 'iu', 'ui', 'iu', NULL, '8787878787', '8877877787', 'yuy', b'1', '$2y$10$rur2GdPUUZ/xOjsg8rHl4.Zsflz/raergWfqE7qNfS1j2XJ/gHPea', 3, 4),
(15, 'prueba', 'prueba', 'prueba', '2022-07-11', '0987652343', '0987652343', 'prueba', b'1', '$2y$10$L3aOALgV3pmZnJYPNe8J6O2OHh8a8aXaQR0EntTc6DAfozdmjounS', 4, 4),
(16, 'prueba', 'prueba', 'prueba', '2022-07-07', '0987652343', '0987652343', 'prueba', b'1', '$2y$10$m5RHquNawdWqBdmZzxs0aOopuLbeNjoIB9dMkR67gPALqT9IORSEW', 4, 4),
(17, 'Mario', 'Guaray', 'marioguaray@gmail.com', '2017-01-03', '0202124361', '0939443952', 'Guamani', b'1', '$2y$10$mbPy7oY/3reEtHkbA2/i5OO/q5c7B3X502fTY.9h0zSDtDGh9foOu', 3, 5),
(18, 'prueba', 'prueba', 'prueba', '2022-07-06', '0987652343', '0987652343', 'prueba', b'1', '$2y$10$onX4gLol1FV.phtQGcoIt.Nh/olgFC7AGb6DXZwb73Hx2ovsrPcxu', 3, 5),
(19, 'prueba 3', 'prueba 3', 'prueba', '2021-05-03', '0987652343', '0987652343', 'prueba', b'1', '$2y$10$kdqz1HsienhJQuuQH0aZJuy6X3Zu87j4HoD7Z.gJe/55WUKocL90a', 3, 2),
(20, 'prueba 4', 'prueba 4', 'prueba', '2022-08-10', '0987652343', '0987652343', 'prueba', b'1', '$2y$10$om.R2..ySbGYUsb0.w3K6uTV6wzBFnzDZAcGgFLL3oh78isUPJqVO', 3, 2),
(21, 'Renato', 'Toasa', 'renato@uisrael.com', '2022-08-02', '0987654321', '0987652343', 'prueba', b'1', '$2y$10$h5.vt.zwsn.b8tioPIG/CeoCnIGBKm.8BWHKUEFTE4QFRPdj2GLWa', 3, 3);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `asistencias`
--
ALTER TABLE `asistencias`
  ADD PRIMARY KEY (`Asistencia_Id`),
  ADD KEY `Usuario_Id` (`Usuario_Id`),
  ADD KEY `Curso_Id` (`Curso_Id`);

--
-- Indices de la tabla `cursos`
--
ALTER TABLE `cursos`
  ADD PRIMARY KEY (`Curso_Id`);

--
-- Indices de la tabla `gamificaciones`
--
ALTER TABLE `gamificaciones`
  ADD PRIMARY KEY (`Gamificacion_Id`),
  ADD KEY `FK_Gamificacion_asistencia` (`Asistencia_Id`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Rol_Id`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Usuario_Id`),
  ADD KEY `fk_TablaRol_usuario` (`Rol_Id`),
  ADD KEY `fk_Usuario_Curso` (`Curso_Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `asistencias`
--
ALTER TABLE `asistencias`
  MODIFY `Asistencia_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `cursos`
--
ALTER TABLE `cursos`
  MODIFY `Curso_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `gamificaciones`
--
ALTER TABLE `gamificaciones`
  MODIFY `Gamificacion_Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `Rol_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Usuario_Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `asistencias`
--
ALTER TABLE `asistencias`
  ADD CONSTRAINT `asistencias_ibfk_1` FOREIGN KEY (`Usuario_Id`) REFERENCES `usuarios` (`Usuario_Id`),
  ADD CONSTRAINT `asistencias_ibfk_2` FOREIGN KEY (`Curso_Id`) REFERENCES `cursos` (`Curso_Id`);

--
-- Filtros para la tabla `gamificaciones`
--
ALTER TABLE `gamificaciones`
  ADD CONSTRAINT `FK_Gamificacion_asistencia` FOREIGN KEY (`Asistencia_Id`) REFERENCES `asistencias` (`Asistencia_Id`);

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `fk_TablaRol_usuario` FOREIGN KEY (`Rol_Id`) REFERENCES `roles` (`Rol_Id`),
  ADD CONSTRAINT `fk_Usuario_Curso` FOREIGN KEY (`Curso_Id`) REFERENCES `cursos` (`Curso_Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
