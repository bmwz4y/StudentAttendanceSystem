CREATE DATABASE  IF NOT EXISTS `studentattendancesystemdb` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `studentattendancesystemdb`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win32 (AMD64)
--
-- Host: localhost    Database: studentattendancesystemdb
-- ------------------------------------------------------
-- Server version	5.6.33-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `lecture_attendance`
--

DROP TABLE IF EXISTS `lecture_attendance`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lecture_attendance` (
  `University_ID` varchar(50) NOT NULL,
  `6/12/2016` varchar(8) DEFAULT NULL,
  `7/12/2016` varchar(8) DEFAULT NULL,
  `8/12/2016` varchar(8) DEFAULT NULL,
  `9/12/2016` varchar(8) DEFAULT NULL,
  `10/12/2016` varchar(8) DEFAULT NULL,
  PRIMARY KEY (`University_ID`),
  UNIQUE KEY `University_ID_UNIQUE` (`University_ID`),
  CONSTRAINT `lecture_attendance_ibfk_1` FOREIGN KEY (`University_ID`) REFERENCES `students` (`University_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lecture_attendance`
--

LOCK TABLES `lecture_attendance` WRITE;
/*!40000 ALTER TABLE `lecture_attendance` DISABLE KEYS */;
INSERT INTO `lecture_attendance` VALUES ('20100171062','9:12 PM','7:23 AM','3:36 AM','2:50 AM','2:50 AM'),('20102171058','9:20 AM','2:48 AM','1:08 AM','',''),('20112171028','9:12 PM',NULL,'3:36 AM','2:50 AM','2:50 AM'),('20120171006','9:12 PM',NULL,'3:36 AM','2:50 AM','2:50 AM'),('20120171010','9:12 PM',NULL,'3:36 AM','2:50 AM','2:50 AM'),('20120171079','9:12 PM',NULL,'3:36 AM','2:50 AM','2:50 AM'),('20120171088','9:12 PM',NULL,'3:36 AM','2:50 AM','2:50 AM'),('20120171091','9:12 PM','7:09 AM','3:36 AM','2:50 AM','2:50 AM'),('20122171018','9:12 PM','3:36 AM','3:36 AM','2:50 AM','2:50 AM'),('20122171033','9:12 PM','9:12 PM','3:36 AM','2:50 AM','2:50 AM');
/*!40000 ALTER TABLE `lecture_attendance` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `students` (
  `University_ID` varchar(50) NOT NULL,
  `Student_Name` varchar(50) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Serial_Number` int(11) NOT NULL,
  `Phone_Number` varchar(50) DEFAULT NULL,
  `Pass_Word` varchar(50) NOT NULL,
  PRIMARY KEY (`University_ID`),
  UNIQUE KEY `University_ID_UNIQUE` (`University_ID`),
  UNIQUE KEY `Student_Name_UNIQUE` (`Student_Name`),
  UNIQUE KEY `Email_UNIQUE` (`Email`),
  UNIQUE KEY `Serial_Number_UNIQUE` (`Serial_Number`),
  UNIQUE KEY `Phone_Number_UNIQUE` (`Phone_Number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students`
--

LOCK TABLES `students` WRITE;
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
INSERT INTO `students` VALUES ('20100171062','Mohammad Eqbal Nayef Zaareer','justcpe10@gmail.com',1,'0798031142','062'),('20102171058','Yahya (mohd khier) Khaleel al-mubaideen','ymmubaieen10@cit.just.edu.jo',3,'0785323313','058'),('20112171028','Yousef Abdulmenem Al Swidan','yaswadan11@cit.just.edu.jo',10,'0788999965','028'),('20120171006','Mumen Ziad Naser Jarrah','mzaljarah12@cit.just.edu.jo',4,'0785323322','006'),('20120171010','Omar abed Allah Mohammad Hadi Shboul','oahadialshbool12@cit.just.edu.jo',8,'0781597046','010'),('20120171079','Khaled Ahmad Ghazi Badarneh','kabadarneh122@cit.just.edu.jo',2,'0798031143','079'),('20120171088','Laith Mohammad Nour Abat','lmabat12@cit.just.edu.jo',6,'0788456982','088'),('20120171091','Jamal Abed AL Nasser Ahmad Younis','jayounis126@cit.just.edu.jo',7,'0774598630','091'),('20122171018','Rami Tawfiq Mohammad Mayyas','rtmayyas12@cit.just.edu.jo',5,'0798031149','018'),('20122171033','Yazan Zakariya Mahmoud Omary','yzalomary12@cit.just.edu.jo',9,'','033');
/*!40000 ALTER TABLE `students` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-12-11  2:03:50
