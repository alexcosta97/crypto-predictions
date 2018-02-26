-- phpMyAdmin SQL Dump
-- version 4.4.15.10
-- https://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Feb 26, 2018 at 08:58 PM
-- Server version: 5.5.52-MariaDB
-- PHP Version: 5.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db5_21611431`
--

-- --------------------------------------------------------

--
-- Table structure for table `Exchanges`
--

CREATE TABLE IF NOT EXISTS `Exchanges` (
  `ExchangeID` int(11) NOT NULL,
  `ExchangeName` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `Stats`
--

CREATE TABLE IF NOT EXISTS `Stats` (
  `StatID` int(11) NOT NULL,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `ExchangeID` int(11) NOT NULL,
  `HighestLatestDiff` double NOT NULL,
  `AvgDiff` double NOT NULL,
  `AvgGrowthTime` bigint(20) NOT NULL,
  `AvgDeclineTime` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
