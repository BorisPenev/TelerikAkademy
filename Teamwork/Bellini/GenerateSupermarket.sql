USE db9ac536f7668248bf9f78a20300872169;

CREATE TABLE IF NOT EXISTS `Measures` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Measure Name` varchar(30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `Vendors` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Vendor Name` varchar(80) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

CREATE TABLE IF NOT EXISTS `Products` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `VendorID` int,
  `Product Name` varchar(80) NOT NULL,
  `MeasureID` int,
  `Base Price` float NOT NULL,
  PRIMARY KEY (`ID`),
FOREIGN KEY (VendorID) REFERENCES Vendors(ID),
FOREIGN KEY (MeasureID) REFERENCES Measures(ID)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1 ;

INSERT INTO `Measures` (`ID`, `Measure Name`) VALUES
(100, 'Liters'),
(200, 'Pieces'),
(300, 'Galons'),
(400, 'Kilograms'),
(500, 'Hektoliters');

INSERT INTO `Vendors` (`ID`, `Vendor Name`) VALUES
(10, 'Zagorka Corp.'),
(20, 'Targovishte Bottling Ltd.'),
(30, 'Vinprom Peshtera'),
(40, 'Targovishte Bottling Ltd0.'),
(50, 'Vinprom Peshtera0'),
(60, 'Milka Ltd.'),
(70, 'Svoge Ltd.'),
(80, 'Zagorka Corp.2'),
(90, 'Targovishte Bottling Ltd.2'),
(100, 'Vinprom Peshtera2'),
(110, 'Milka Ltd.2'),
(120, 'Svoge Ltd.2'),
(130, 'Zagorka Corp.3'),
(140, 'Targovishte Bottling Ltd.3'),
(150, 'Vinprom Peshtera3'),
(160, 'Milka Ltd.3'),
(170, 'Svoge Ltd.4'),
(180, 'Vinprom Peshtera4'),
(190, 'Milka Ltd.4'),
(200, 'Svoge Ltd.4');

INSERT INTO `Products` (`ID`, `VendorID`, `Product Name`, `MeasureID`, `Base Price`) VALUES 
(1, 10, 'Chocolate "Svoge"0', 200, 1.45),
(2, 20, 'Chocolate0', 100, 1.85),
(3, 30, 'Chocolate "Svoge"10', 200, 1.45),
(4, 40, 'Chocolate10', 100, 1.85),
(5, 50, 'Chocolate "Svoge"', 200, 1.45),
(6, 60, '"Zagorka"', 100, 0.76),
(7, 70, '"Targovishte"', 300, 3.9),
(8, 80, 'Grozdova', 300, 5.9),
(9, 90, '"Milka"', 500, 8.85),
(10, 100, 'Chocolate "Svoge1"', 200, 1.45),
(11, 110, 'Beer "Zagorka1"', 100, 0.86),
(12, 120, 'Vodka "Targovishte1"', 300, 2.9),
(13, 130, 'Peshterska Grozdova1', 400, 4.9),
(14, 140, 'Chocolate "Milka"1', 500, 100.85),
(15, 150, 'Chocolate "Svoge"1', 200, 1.45),
(16, 160, 'Beer "Zagorka"2', 300, 0.86),
(17, 170, 'Vodka "Targovishte"2', 100, 2.9),
(18, 180, 'Peshterska Grozdova2', 100, 4.9),
(19, 190, 'Chocolate "Milka"2', 300, 1.85),
(20, 200, 'Chocolate "Svoge"2', 400, 10.45),
(21, 110, 'Beer "Zagorka"3', 500, 90.86),
(22, 120, 'Vodka "Targovishte"3', 100, 2.9),
(23, 130, 'Peshterska Grozdova3', 100, 4.9),
(24, 140, 'Chocolate "Milka"3', 200, 1.85),
(25, 150, 'Chocolate "Svoge"3', 200, 1.45),
(26, 160, 'Beer "Zagorka"4', 100, 0.86),
(27, 170, 'Vodka "Targovishte"4', 100, 2.9),
(28, 180, 'Peshterska Grozdova4', 100, 4.9),
(29, 190, 'Chocolate "Milka"4', 200, 3.85),
(30, 200, 'Chocolate "Svoge"4', 200, 13.45),
(31, 110, 'Beer "Zagorka"5', 100, 0.86),
(32, 120, 'Vodka "Targovishte"5', 200, 2.9),
(33, 130, 'Peshterska Grozdova5', 300, 4.9),
(34, 140, 'Chocolate "Milka"5', 200, 1.85),
(35, 150, 'Chocolate "Svoge"5', 400, 1.45),
(36, 160, 'Beer "Zagorka"6', 100, 0.86),
(37, 170, 'Vodka "Targovishte"6', 500, 20.9),
(38, 180, 'Peshterska Grozdova6', 100, 4.9),
(39, 190, 'Chocolate "Milka"6', 200, 1.85),
(40, 200, 'Chocolate "Svoge"6', 200, 1.45),
(41, 110, 'Beer "Zagorka"7', 100, 0.86),
(42, 120, 'Vodka "Targovishte"7', 100, 2.9),
(43, 130, 'Peshterska Grozdova7', 100, 4.9),
(44, 140, 'Chocolate "Milka"7', 500, 1.85),
(45, 150, 'Chocolate "Svoge"7', 200, 1.45),
(46, 160, 'Beer "Zagorka"8', 100, 0.86),
(47, 170, 'Vodka "Targovishte"8', 100, 2.9),
(48, 180, 'Peshterska Grozdova8', 500, 40.9),
(49, 190, 'Chocolate "Milka"8', 200, 1.85),
(50, 200, 'Chocolate "Svoge"8', 300, 10.45);