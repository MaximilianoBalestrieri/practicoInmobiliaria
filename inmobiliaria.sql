-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 22-04-2025 a las 22:41:31
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
-- Base de datos: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `dniPropietario` varchar(11) NOT NULL,
  `nombrePropietario` varchar(255) NOT NULL,
  `dniInquilino` varchar(11) NOT NULL,
  `nombreInquilino` varchar(255) NOT NULL,
  `fechaInicio` date NOT NULL,
  `fechaFinal` date NOT NULL,
  `monto` decimal(15,0) NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  `vigente` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`idContrato`, `dniPropietario`, `nombrePropietario`, `dniInquilino`, `nombreInquilino`, `fechaInicio`, `fechaFinal`, `monto`, `idInmueble`, `direccion`, `vigente`) VALUES
(1, '33456788', 'Selene Nicole Farias', '22321456', 'Jorge Suarez', '2025-04-18', '2025-05-01', 118000, 4, 'Pte. Perón 4552 Dto. 0 , Capital Federal', 1),
(4, '20922882', 'Analia Farias', '22321456', 'Jorge Suarez', '2025-04-25', '2025-04-26', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 1),
(5, '22666454', 'Cristina Diaz', '27444236', 'Paula Fernanda Gonzalez', '2025-05-01', '2025-05-03', 250000, 24, 'Av. España 1250 Piso 1 Dto. a , Villa Dolores', 0),
(6, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-04-27', '2025-05-01', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 0),
(7, '20922882', 'Analia Farias', '27444236', 'Paula Fernanda Gonzalez', '2025-04-25', '2025-04-26', 245000, 9, 'Thorne 1439 Piso 10 Dto. 0 , Ituzaingo', 0),
(8, '20922882', 'Analia Farias', '33488655', 'Mariano Ríos', '2025-04-09', '2025-04-15', 100000, 23, 'Pública 0 Dto. 0 , La Paz', 0),
(9, '27344555', 'Maria Celeste  Balestrieri', '33488655', 'Mariano Ríos', '2025-04-21', '2025-06-21', 244000, 25, 'Pje Bianchi 45 Dto. 0 , Villa Dolores', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `idInmueble` int(11) NOT NULL,
  `dniPropietario` varchar(11) NOT NULL,
  `calle` varchar(50) NOT NULL,
  `nro` int(10) NOT NULL,
  `piso` int(2) DEFAULT NULL,
  `dpto` varchar(11) DEFAULT NULL,
  `localidad` varchar(50) NOT NULL,
  `provincia` varchar(50) NOT NULL,
  `uso` varchar(20) NOT NULL,
  `tipo` varchar(20) NOT NULL,
  `ambientes` int(2) NOT NULL,
  `pileta` varchar(2) NOT NULL,
  `parrilla` varchar(2) NOT NULL,
  `garage` varchar(2) NOT NULL,
  `latitud` decimal(11,0) NOT NULL,
  `longitud` decimal(11,0) NOT NULL,
  `precio` int(11) NOT NULL,
  `ImagenPortada` varchar(255) DEFAULT NULL,
  `vigente` tinyint(2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`idInmueble`, `dniPropietario`, `calle`, `nro`, `piso`, `dpto`, `localidad`, `provincia`, `uso`, `tipo`, `ambientes`, `pileta`, `parrilla`, `garage`, `latitud`, `longitud`, `precio`, `ImagenPortada`, `vigente`) VALUES
(4, '33456788', 'Pte. Perón', 4552, 0, '0', 'Capital Federal', 'Buenos Aires', 'Residencial', 'Casa', 2, '1', '1', '1', 15, 13, 38000, '~/Imagenes/Inmuebles/casa1_2abc6b05-e9f4-44db-8e17-0a6932b172d5.jpg', 0),
(6, '10884239', 'Rivadavia', 10200, 10, 'A', 'Merlo', 'San Luis', 'Residencial', 'Casa', 2, '0', '1', '1', 25, 44, 98000, '~/Imagenes/Inmuebles/casa4_3c79f870-dfcf-46b0-83e5-8d2e22763af8.jpg', 0),
(8, '25748654', 'Republica del Líbano', 450, 1, 'A', 'San Martin', 'Buenos Aires', 'Residencial', 'Casa', 4, '0', '0', '1', 35, 44, 105000, '~/Imagenes/Inmuebles/casa1_bd22f4a2-8cd4-428d-ae51-2622952f56f3.jpg', 0),
(9, '20922882', 'Thorne', 1439, 10, '0', 'Ituzaingo', 'Buenos Aires', 'Residencial', 'Casa', 3, '1', '1', '1', 33, 13, 85000, '~/Imagenes/Inmuebles/casa1.jpg', 1),
(16, '20922882', 'Europa', 321, 2, 'A', 'Ituzaingó', 'Buenos Aires', 'Comercial', 'Casa', 3, '1', '0', '1', 35, 32, 89000, '~/Imagenes/Inmuebles/casa1_76ce8c3b-d017-403c-a35f-1ab531d86ade.jpg', 0),
(18, '20922882', 'Chacabuco', 648, 0, NULL, 'Buenos Aires', 'Buenos Aires', 'Residencial', 'Casa', 2, '0', '0', '0', 12, 321, 33000, '~/Imagenes/Inmuebles/casa3.jpg', 0),
(21, '25748654', 'Perú', 456, 6, 'A', 'Merlo', 'San Luis', '5', '5', 2, '0', '0', '0', 5, 511, 33000, '~/Imagenes/Inmuebles/casa7_0b15543b-01ac-49c2-b81c-28c8d168efa0.jpg', 0),
(22, '25748654', 'Belgrano', 12, 0, NULL, 'Villa Dolores', 'Córdoba', 'Comercial', 'Casa', 6, '1', '1', '0', 25, 125, 74000, '/Uploads/Portadas/casa4.jpg', 1),
(23, '20922882', 'Pública', 0, 0, '0', 'La Paz', 'Córdoba', 'Residencial', 'Casa', 4, '1', '1', '1', 123, 25, 100000, '/Uploads/Portadas/casa7.jpg', 1),
(24, '22666454', 'Av. España', 1250, 1, 'a', 'Villa Dolores', 'Córdoba', 'Residencial', 'Casa', 3, '1', '1', '1', 25, 125, 88000, '~/Imagenes/Inmuebles/casa4_b8b5af27-c6a0-4432-8596-e7923fb26bb4.jpg', 1),
(25, '27344555', 'Pje Bianchi', 45, 0, '0', 'Villa Dolores', 'Córdoba', 'Residencial', 'Casa', 4, '0', '1', '1', 12, 44, 450000, '/Uploads/Portadas/casa1.jpg', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueblefotocarrusel`
--

CREATE TABLE `inmueblefotocarrusel` (
  `Id` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL,
  `RutaFoto` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueblefotocarrusel`
--

INSERT INTO `inmueblefotocarrusel` (`Id`, `IdInmueble`, `RutaFoto`) VALUES
(15, 22, '~/Imagenes/Carrusel/casa1Interior2.jpg'),
(17, 6, '~/Imagenes/Carrusel/casa1_64b17522-eba5-4a4b-91ed-8e3d83fdf5c6.jpg'),
(20, 21, '~/Imagenes/Carrusel/casa2Interior1_80e021ba-0b4c-405b-83ef-1b4fd2cbea07.jpg'),
(21, 21, '~/Imagenes/Carrusel/casa2Interior2_1abf5804-2a4e-44eb-b68a-6f8b5491e712.jpg'),
(23, 6, '~/Imagenes/Carrusel/casa3Interior1_46e5cd17-7d50-4ae8-a1b3-d593a03f53f5.jpg'),
(25, 23, '~/Imagenes/Carrusel/casa1Interior1_b0334e7c-47fa-4083-9632-4d65f3a82e5a.jpg'),
(26, 8, '~/Imagenes/Carrusel/casa1Interior1_5a383221-4a3d-4385-abf7-0f77696cf7f0.jpg'),
(28, 8, '~/Imagenes/Carrusel/casa2Interior1_dd83cf22-2fbf-4eb4-a4d1-99d923eeaef3.jpg'),
(29, 8, '~/Imagenes/Carrusel/casa2Interior2_a1d75a5f-235d-44a1-8bd9-fcd6093fe735.jpg'),
(31, 24, '~/Imagenes/Carrusel/casa3Interior2_50ae7d87-9454-4b59-922d-10420105f49f.jpg'),
(33, 25, '~/Imagenes/Carrusel/casa2Interior2_d95b5de4-0a9f-4614-80e4-38c9ab0ecafb.jpg'),
(34, 25, '~/Imagenes/Carrusel/casa3Interior1_b5fd0c1f-61b0-4edc-9733-fea547f20443.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `dniInquilino` varchar(11) NOT NULL,
  `apellidoInquilino` varchar(50) NOT NULL,
  `nombreInquilino` varchar(50) NOT NULL,
  `contactoInquilino` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `dniInquilino`, `apellidoInquilino`, `nombreInquilino`, `contactoInquilino`) VALUES
(1, '22321456', 'Suarez', 'Jorge', '1162105442'),
(2, '33488655', 'Ríos', 'Mariano', '3544226644'),
(3, '27444236', 'Gonzalez', 'Paula Fernanda', '2664235465');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pagos`
--

CREATE TABLE `pagos` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `nroPago` int(11) NOT NULL,
  `fechaPago` date NOT NULL,
  `importe` decimal(15,0) NOT NULL,
  `detalle` varchar(255) NOT NULL,
  `estado` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pagos`
--

INSERT INTO `pagos` (`idPago`, `idContrato`, `nroPago`, `fechaPago`, `importe`, `detalle`, `estado`) VALUES
(2, 7, 1, '2025-05-06', 20000, 'mes de mayo', 'Pagado'),
(3, 1, 2, '2025-04-15', 120000, 'mes de abril 2025.', 'Pagado'),
(7, 1, 3, '2025-04-29', 11000, 'mes de mayo 2025', 'Anulado'),
(8, 5, 1, '2025-04-19', 150000, 'Abril de 2025', 'Pagado'),
(9, 5, 2, '2025-04-25', 150500, 'mes de mayo 2025', 'Anulado'),
(10, 7, 2, '2025-04-22', 125000, 'Abono por un mes.', 'Pagado'),
(11, 1, 1, '2025-03-01', 120000, 'Mes de marzo 2025..', 'Pagado'),
(12, 1, 4, '2025-05-20', 120000, 'mes de junio 2025.', 'Anulado'),
(13, 7, 3, '2025-06-01', 125000, 'mes de junio de 2025', 'Pagado'),
(14, 7, 3, '2025-06-01', 125000, 'mes de junio de 2025', 'Anulado'),
(15, 1, 5, '2025-04-21', 1200000, 'mes de abril', 'Pagado'),
(16, 1, 6, '2025-04-28', 12444, 'Abono por un mes', 'Pagado'),
(17, 5, 2, '2025-05-01', 150000, 'Mayo 2025', 'Pagado'),
(18, 4, 0, '0001-01-01', 0, '', 'Anulado'),
(19, 4, 1, '2025-04-01', 90000, 'mes de abril', 'Pagado');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `dniPropietario` varchar(11) NOT NULL,
  `apellidoPropietario` varchar(50) NOT NULL,
  `nombrePropietario` varchar(50) NOT NULL,
  `contactoPropietario` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `dniPropietario`, `apellidoPropietario`, `nombrePropietario`, `contactoPropietario`) VALUES
(1, '10884239', 'Lisbon', 'Teresa', '1146276586'),
(2, '25748654', 'Jane', 'Patrick', '1144891234'),
(3, '22666454', 'Diaz', 'Cristina', '2661222363'),
(4, '33456788', 'Farias', 'Selene Nicole', '3544616228'),
(8, '27344555', 'Balestrieri', 'Maria Celeste ', '354438887'),
(9, '20922882', 'Farias', 'Analia', '1169761345');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`);

--
-- Indices de la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdInmueble` (`IdInmueble`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`),
  ADD UNIQUE KEY `dniInquilino` (`dniInquilino`);

--
-- Indices de la tabla `pagos`
--
ALTER TABLE `pagos`
  ADD PRIMARY KEY (`idPago`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT de la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pagos`
--
ALTER TABLE `pagos`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `inmueblefotocarrusel`
--
ALTER TABLE `inmueblefotocarrusel`
  ADD CONSTRAINT `inmueblefotocarrusel_ibfk_1` FOREIGN KEY (`IdInmueble`) REFERENCES `inmueble` (`idInmueble`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
