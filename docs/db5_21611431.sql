CREATE TABLE Exchanges (
  `ExchangeID` int(11) NOT NULL AUTO_INCREMENT,
  `ExchangeName` varchar(50) NOT NULL,
  Primary Key (`ExchangeID`)
)

-- --------------------------------------------------------

--
-- Table structure for table `Stats`
--

CREATE TABLE IF NOT EXISTS `Stats` (
  `StatID` int(11) NOT NULL AUTO_INCREMENT,
  `StartDate` datetime DEFAULT NULL,
  `EndDate` datetime DEFAULT NULL,
  `ExchangeID` int(11) NOT NULL,
  `HighestLatestDiff` double NOT NULL,
  `AvgDiff` double NOT NULL,
  `AvgGrowthTime` bigint(20) NOT NULL,
  `AvgDeclineTime` bigint(20) NOT NULL,
  PRIMARY KEY (`StatID`),
  Key 'ExchangeID' (`ExchangeID`),
  CONSTRAINT `FKExchange` FOREIGN KEY (`ExchangeID`) REFERENCES `Exchanges` (`ExchangeID`) ON DELETE CASCADE ON UPDATE CASCADE
)
