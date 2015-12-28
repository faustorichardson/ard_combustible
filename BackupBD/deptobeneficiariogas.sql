/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50627
Source Host           : localhost:3306
Source Database       : mdeg_combustible

Target Server Type    : MYSQL
Target Server Version : 50627
File Encoding         : 65001

Date: 2015-12-28 11:53:28
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `deptobeneficiariogas`
-- ----------------------------
DROP TABLE IF EXISTS `deptobeneficiariogas`;
CREATE TABLE `deptobeneficiariogas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `departamento` varchar(150) NOT NULL DEFAULT '',
  `tipo` int(11) NOT NULL,
  `tarjeta` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of deptobeneficiariogas
-- ----------------------------
