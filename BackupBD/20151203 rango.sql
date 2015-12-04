/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50627
Source Host           : localhost:3306
Source Database       : mdeg_dispensariomedico

Target Server Type    : MYSQL
Target Server Version : 50627
File Encoding         : 65001

Date: 2015-12-03 19:27:01
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `rangos`
-- ----------------------------
DROP TABLE IF EXISTS `rangos`;
CREATE TABLE `rangos` (
  `rango_id` int(11) NOT NULL,
  `rango_descripcion` varchar(50) NOT NULL DEFAULT ' ',
  `orden` int(2) NOT NULL DEFAULT '0',
  `rangoabrev` varchar(12) NOT NULL DEFAULT ' ',
  PRIMARY KEY (`rango_id`),
  KEY `descripcion` (`rango_descripcion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rangos
-- ----------------------------
INSERT INTO `rangos` VALUES ('1', 'ALMIRANTE', '1', 'ALM.');
INSERT INTO `rangos` VALUES ('2', 'VICE ALMIRANTE', '2', 'VALM.');
INSERT INTO `rangos` VALUES ('3', 'CONTRALMIRANTE', '3', 'CALM.');
INSERT INTO `rangos` VALUES ('4', 'CAPITÁN DE NAVÍO', '4', 'CN.');
INSERT INTO `rangos` VALUES ('5', 'CAPITÁN DE FRAGATA', '5', 'CF.');
INSERT INTO `rangos` VALUES ('6', 'CAPITÁN DE CORBETA', '6', 'CC.');
INSERT INTO `rangos` VALUES ('7', 'TENIENTE DE NAVÍO', '7', 'TN.');
INSERT INTO `rangos` VALUES ('10', 'SARGENTO MAYOR', '15', 'SGTMR');
INSERT INTO `rangos` VALUES ('11', 'SARGENTO', '16', 'SGTO');
INSERT INTO `rangos` VALUES ('12', 'CABO', '17', 'CABO');
INSERT INTO `rangos` VALUES ('13', 'MARINERO ESPECIALISTA', '18', 'MRE');
INSERT INTO `rangos` VALUES ('14', 'MARINERO', '19', 'MRO');
INSERT INTO `rangos` VALUES ('15', 'MARINERO AUXILIAR', '20', 'MRO AUX');
INSERT INTO `rangos` VALUES ('16', 'ASIMILADO', '22', 'ASIM');
INSERT INTO `rangos` VALUES ('17', 'GUARDIAMARINA 4TO. AÑO', '10', 'GM.4-A');
INSERT INTO `rangos` VALUES ('18', 'GUARDIAMARINA 3ER. AÑO', '11', 'GM.3-A');
INSERT INTO `rangos` VALUES ('19', 'GUARDIAMARINA 2DO. AÑO', '12', 'GM.2-A');
INSERT INTO `rangos` VALUES ('20', 'GUARDIAMARINA 1ER. AÑO', '13', 'GM.1-A');
INSERT INTO `rangos` VALUES ('21', 'GRUMETE', '21', 'GRUMETE');
INSERT INTO `rangos` VALUES ('28', 'TENIENTE DE FRAGATA', '8', 'TF.');
INSERT INTO `rangos` VALUES ('29', 'TENIENTE DE CORBETA', '9', 'TC.');
INSERT INTO `rangos` VALUES ('100', 'NO ASIGNADO', '100', 'NO ASIG ');
INSERT INTO `rangos` VALUES ('101', 'CIVIL', '101', 'CIVIL');
INSERT INTO `rangos` VALUES ('102', 'EN - Mayor General', '102', 'May. Gral.');
INSERT INTO `rangos` VALUES ('103', 'EN - Gral. de Brigada', '103', 'Gral. Brig.');
INSERT INTO `rangos` VALUES ('104', 'EN - Coronel', '104', 'Cor.');
INSERT INTO `rangos` VALUES ('105', 'EN - Teniente Coronel', '105', 'Tte. Cor.');
INSERT INTO `rangos` VALUES ('106', 'EN - Mayor', '106', 'May.');
INSERT INTO `rangos` VALUES ('107', 'EN - Capitan', '107', 'Cap.');
INSERT INTO `rangos` VALUES ('108', 'EN - 1er. Teniente', '108', '1er. Tte.');
INSERT INTO `rangos` VALUES ('109', 'EN - 2do. Teniente', '109', '2do. Tte.');
INSERT INTO `rangos` VALUES ('110', 'EN - Cadete', '110', 'Cadete');
INSERT INTO `rangos` VALUES ('111', 'EN - Sargento Mayor', '111', 'Sgto. May.');
INSERT INTO `rangos` VALUES ('112', 'EN - Sargento', '112', 'Sgto.');
INSERT INTO `rangos` VALUES ('113', 'EN - Cabo', '113', 'Cabo');
INSERT INTO `rangos` VALUES ('114', 'EN - Raso', '114', 'Raso');
INSERT INTO `rangos` VALUES ('115', 'FARD - Mayor General', '115', 'May. Gral.');
INSERT INTO `rangos` VALUES ('116', 'FARD - Gral. de Brigada', '116', 'Gral. Brig.');
INSERT INTO `rangos` VALUES ('117', 'FARD - Coronel', '117', 'Cor.');
INSERT INTO `rangos` VALUES ('118', 'FARD - Teniente Coronel', '118', 'Tte. Cor.');
INSERT INTO `rangos` VALUES ('119', 'FARD - Mayor', '119', 'May.');
INSERT INTO `rangos` VALUES ('120', 'FARD - Capitan', '120', 'Cap.');
INSERT INTO `rangos` VALUES ('121', 'FARD - 1er. Teniente', '121', '1er. Tte.');
INSERT INTO `rangos` VALUES ('122', 'FARD - 2do. Teniente', '122', '2do. Tte.');
INSERT INTO `rangos` VALUES ('123', 'FARD - Cadete', '123', 'Cadete');
INSERT INTO `rangos` VALUES ('124', 'FARD - Sargento Mayor', '124', 'Sgto. May.');
INSERT INTO `rangos` VALUES ('125', 'FARD - Sargento', '125', 'Sgto.');
INSERT INTO `rangos` VALUES ('126', 'FARD - Cabo', '126', 'Cabo');
INSERT INTO `rangos` VALUES ('127', 'FARD - Raso', '127', 'Raso');
INSERT INTO `rangos` VALUES ('128', 'PN - Mayor General', '128', 'May. Gral.');
INSERT INTO `rangos` VALUES ('129', 'PN - Gral. de Brigada', '129', 'Gral. Brig.');
INSERT INTO `rangos` VALUES ('130', 'PN - Coronel', '130', 'Cor.');
INSERT INTO `rangos` VALUES ('131', 'PN - Teniente Coronel', '131', 'Tte. Cor.');
INSERT INTO `rangos` VALUES ('132', 'PN - Mayor', '132', 'May.');
INSERT INTO `rangos` VALUES ('133', 'PN - Capitan', '133', 'Cap.');
INSERT INTO `rangos` VALUES ('134', 'PN - 1er. Teniente', '134', '1er. Tte.');
INSERT INTO `rangos` VALUES ('135', 'PN - 2do. Teniente', '135', '2do. Tte.');
INSERT INTO `rangos` VALUES ('136', 'PN - Cadete', '136', 'Cadete');
INSERT INTO `rangos` VALUES ('137', 'PN - Sargento Mayor', '137', 'Sgto. May.');
INSERT INTO `rangos` VALUES ('138', 'PN - Sargento', '138', 'Sgto.');
INSERT INTO `rangos` VALUES ('139', 'PN - Cabo', '139', 'Cabo');
INSERT INTO `rangos` VALUES ('140', 'PN - Raso', '140', 'Raso');
