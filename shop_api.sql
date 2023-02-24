-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Feb 24, 2023 alle 02:14
-- Versione del server: 10.4.27-MariaDB
-- Versione PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `shop_api`
--

-- --------------------------------------------------------

--
-- Struttura della tabella `invoices`
--

CREATE TABLE `invoices` (
  `Id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dump dei dati per la tabella `invoices`
--

INSERT INTO `invoices` (`Id`) VALUES
(1),
(2),
(8);

-- --------------------------------------------------------

--
-- Struttura della tabella `invoices_products`
--

CREATE TABLE `invoices_products` (
  `IdInvoice` int(11) NOT NULL,
  `IdProduct` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dump dei dati per la tabella `invoices_products`
--

INSERT INTO `invoices_products` (`IdInvoice`, `IdProduct`, `Quantity`) VALUES
(1, 2, 3),
(2, 1, 2);

-- --------------------------------------------------------

--
-- Struttura della tabella `products`
--

CREATE TABLE `products` (
  `Id` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dump dei dati per la tabella `products`
--

INSERT INTO `products` (`Id`, `Name`, `Price`) VALUES
(1, 'pizza', 5),
(2, 'panino', 4);

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `invoices`
--
ALTER TABLE `invoices`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `invoices_products`
--
ALTER TABLE `invoices_products`
  ADD PRIMARY KEY (`IdInvoice`,`IdProduct`),
  ADD KEY `IdProduct` (`IdProduct`);

--
-- Indici per le tabelle `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Id`);

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `invoices_products`
--
ALTER TABLE `invoices_products`
  ADD CONSTRAINT `invoices_products_ibfk_1` FOREIGN KEY (`IdInvoice`) REFERENCES `invoices` (`id`),
  ADD CONSTRAINT `invoices_products_ibfk_2` FOREIGN KEY (`IdProduct`) REFERENCES `products` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
