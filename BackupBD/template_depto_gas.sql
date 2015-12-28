/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50627
Source Host           : localhost:3306
Source Database       : mdeg_combustible

Target Server Type    : MYSQL
Target Server Version : 50627
File Encoding         : 65001

Date: 2015-12-28 11:51:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `template_depto_gas`
-- ----------------------------
DROP TABLE IF EXISTS `template_depto_gas`;
CREATE TABLE `template_depto_gas` (
  `id` double DEFAULT NULL,
  `departamento` varchar(255) DEFAULT NULL,
  `tipo` double DEFAULT NULL,
  `tarjeta` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of template_depto_gas
-- ----------------------------
INSERT INTO `template_depto_gas` VALUES ('1', 'BASE NAVAL “27 DE FEBRERO”', '1', null);
INSERT INTO `template_depto_gas` VALUES ('2', 'BASE NAVAL “LAS CALDERAS”', '1', null);
INSERT INTO `template_depto_gas` VALUES ('3', 'COMANDANCIA DE PUERTO DE BOCA CHICA', '2', null);
INSERT INTO `template_depto_gas` VALUES ('4', 'COMANDANCIA DE PUERTO DE HAINA', '2', null);
INSERT INTO `template_depto_gas` VALUES ('5', 'COMANDANCIA DE PUERTO DE SANTO DOMINGO', '2', null);
INSERT INTO `template_depto_gas` VALUES ('6', 'COMANDANCIA DE PUERTO DE LA  ROMANA', '2', null);
INSERT INTO `template_depto_gas` VALUES ('7', 'COMANDANCIA DE PUERTO DE SAN PEDRO DE MACORIS', '2', null);
INSERT INTO `template_depto_gas` VALUES ('8', 'COMANDANCIA DE PUERTO MULTIMODAL CAUCEDO', '2', null);
INSERT INTO `template_depto_gas` VALUES ('9', 'COMANDANCIA DE PUERTO DE MANZANILLO', '2', null);
INSERT INTO `template_depto_gas` VALUES ('10', 'COMANDANCIA DE PUERTO DE SAMANA', '2', null);
INSERT INTO `template_depto_gas` VALUES ('11', 'COMANDANCIA DE PUERTO PLATA, (ARD)', '2', null);
INSERT INTO `template_depto_gas` VALUES ('12', 'COMANDANCIA DE PUERTO DE AZUA', '2', null);
INSERT INTO `template_depto_gas` VALUES ('13', 'COMANDANCIA DE PUERTO DE BARAHONA', '2', null);
INSERT INTO `template_depto_gas` VALUES ('14', 'COMANDANCIA DE PUERTO DE CABO ROJO', '2', null);
INSERT INTO `template_depto_gas` VALUES ('15', 'DESTACAMENTO DE PALENQUE, SAN CRISTÓBAL.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('16', 'DESTACAMENTO MIXTO DE SIERRA PRIETA', '3', null);
INSERT INTO `template_depto_gas` VALUES ('17', 'DESTACAMENTO MIXTO DEL ACUARIO NACIONAL', '3', null);
INSERT INTO `template_depto_gas` VALUES ('18', 'DESTACAMENTO OPERATIVO CIENAGA, (ARD)', '3', null);
INSERT INTO `template_depto_gas` VALUES ('19', 'SUBDIRECCION DEL DESTACAMENTO FARO A COLON, (ARD)', '3', null);
INSERT INTO `template_depto_gas` VALUES ('20', 'DESTACAMENTO CABEZA DE TORO, HIGÜEY', '3', null);
INSERT INTO `template_depto_gas` VALUES ('21', 'DESTACAMENTO DE LA  ISLA SAONA.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('22', 'DESTACAMENTO DE MICHES,  SEYBO.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('23', 'DESTACAMENTO DE SABANA DE LA MAR,  HATO MAYOR', '3', null);
INSERT INTO `template_depto_gas` VALUES ('24', 'DESTACAMENTO DOTACION BOCA DE CHAVON CASA DE CAMPO', '3', null);
INSERT INTO `template_depto_gas` VALUES ('25', 'DESTACAMENTO DE CABRERA, NAGUA.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('26', 'DESTACAMENTO DE LUPERÓN, PUERTO PLATA.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('27', 'DESTACAMENTO DE SANCHEZ,  SAMANA.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('28', 'DESTACAMENTO MIXTO ALTO BANDERA,  CONSTANZA.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('29', 'DESTACAMENTO DE JUANCHO, PEDERNALES', '3', null);
INSERT INTO `template_depto_gas` VALUES ('30', 'DESTACAMENTO DE LA  ISLA BEATA.', '3', null);
INSERT INTO `template_depto_gas` VALUES ('31', 'DESTACAMENTO DE PALMAR DE OCOA, BANÍ', '3', null);
INSERT INTO `template_depto_gas` VALUES ('32', 'DESTACAMENTO TORTUGUERO DE AZUA, (ARD)', '3', null);
INSERT INTO `template_depto_gas` VALUES ('33', 'PUESTO BOSQUE DEL ISABELA, (ARD)', '4', null);
INSERT INTO `template_depto_gas` VALUES ('34', 'PUESTO  AVANZADO  RIO SOCO, SAN PEDRO DE MACORIS', '4', null);
INSERT INTO `template_depto_gas` VALUES ('35', 'PUESTO  DE SABANA DE NISIBON, HIGÜEY', '4', null);
INSERT INTO `template_depto_gas` VALUES ('36', 'PUESTO AVANZADO BOCA CHICA.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('37', 'PUESTO AVANZADO DE  CATUANO,  ISLA SAONA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('38', 'PUESTO AVANZADO DE CELEDONIO, MICHES', '4', null);
INSERT INTO `template_depto_gas` VALUES ('39', 'PUESTO AVANZADO DE COSTA ESMERALDA, MICHES', '4', null);
INSERT INTO `template_depto_gas` VALUES ('40', 'PUESTO AVANZADO DE JUANILLO,  HIGÜEY', '4', null);
INSERT INTO `template_depto_gas` VALUES ('41', 'PUESTO AVANZADO UVERO ALTO, HIGÜEY', '4', null);
INSERT INTO `template_depto_gas` VALUES ('42', 'PUESTO BOCA DE CHAVON, LA ROMANA.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('43', 'PUESTO BOCA DE MAIMON (LA VACAMA)  HIGÜEY.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('44', 'PUESTO DE BAYAHÍBE, LA ROMANA.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('45', 'PUESTO DE BOCA DE YUMA,  HIGÜEY.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('46', 'PUESTO DE CELEDONIO, MICHES', '4', null);
INSERT INTO `template_depto_gas` VALUES ('47', 'PUESTO DE CUMAYASA, LA ROMANÁ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('48', 'PUESTO DE LA ISLA CATALINA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('49', 'PUESTO DE LAS CAÑITAS, SABANA DE LA MAR.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('50', 'PUESTO DE LAS SARDINAS, (ARD)', '4', null);
INSERT INTO `template_depto_gas` VALUES ('51', 'PUESTO DE VISTA CATALINA, LA ROMANA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('52', 'PUESTO DEL CORTESITO,  HIGÜEY.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('53', 'PUESTO DEL MACAO, HIGUEY', '4', null);
INSERT INTO `template_depto_gas` VALUES ('54', 'PUESTO EL LIMON,  MICHES', '4', null);
INSERT INTO `template_depto_gas` VALUES ('55', 'PUESTO AVANZADO DE RIO BOBA, NAGUA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('56', 'PUESTO BAOBA DEL PIÑAL,  NAGUA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('57', 'PUESTO CAYO LEVANTADO,  SAMANÁ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('58', 'PUESTO DE ARROYO BARRIL, SAMANÁ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('59', 'PUESTO DE BUEN HOMBRE, MONTE CRISTI', '4', null);
INSERT INTO `template_depto_gas` VALUES ('60', 'PUESTO DE CAMBIASO, PUERTO PLATA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('61', 'PUESTO DE LA  ERMITA, GASPAR HERNANDEZ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('62', 'PUESTO DE LA JAGUA, NAGUA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('63', 'PUESTO DE LA LAGUNA DEL CRISTAL, LOS HAITISES', '4', null);
INSERT INTO `template_depto_gas` VALUES ('64', 'PUESTO DE LA MAJAGUA DE SANCHEZ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('65', 'PUESTO DE LAS GALERAS, SAMANÁ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('66', 'PUESTO DE LAS TERRENAS,  SAMANÁ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('67', 'PUESTO DE LOS CACAOS, SAMANÁ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('68', 'PUESTO DE MAIMON DE PUERTO PLATA.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('69', 'PUESTO DE MATANCITA,  NAGUA.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('70', 'PUESTO DE OCEAN WORLD, PUERTO PLATA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('71', 'PUESTO DE PAROLI,  MONTE CRISTI', '4', null);
INSERT INTO `template_depto_gas` VALUES ('72', 'PUESTO DE PUNTA RUSIA, MONTE CRISTI', '4', null);
INSERT INTO `template_depto_gas` VALUES ('73', 'PUESTO DE RIO SAN JUAN, NAGUA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('74', 'PUESTO DEL CASTILLO, ARD.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('75', 'PUESTO AVANZADO \"LAS CALDERAS\", BANÍ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('76', 'PUESTO DE  MIRAMAR,  PEDERNALES.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('77', 'PUESTO DE CABO PEQUEÑO, ARD', '4', null);
INSERT INTO `template_depto_gas` VALUES ('78', 'PUESTO DE LA PRESA DE VALDESIA', '4', null);
INSERT INTO `template_depto_gas` VALUES ('79', 'PUESTO DE LOS ALMENDROS,  BANÍ', '4', null);
INSERT INTO `template_depto_gas` VALUES ('80', 'PUESTO DE PUERTO EN MEDIO, ARD.', '4', null);
INSERT INTO `template_depto_gas` VALUES ('81', 'PUESTO DE SALINAS, ARD, BANI', '4', null);
INSERT INTO `template_depto_gas` VALUES ('82', 'BOTE ZODIAC \"TINGLAR\"  BI-4, (ARD)', '6', null);
INSERT INTO `template_depto_gas` VALUES ('83', 'BUQUE INSIGNIA PATRULLERO DE ALTURA \"ALMIRANTE DIDIEZ BURGOS\" PA-301', '6', null);
INSERT INTO `template_depto_gas` VALUES ('84', 'DRAGA HISPANIOLA', '6', null);
INSERT INTO `template_depto_gas` VALUES ('85', 'GC-106, GUARDACOSTAS \"BELLATRIX\"', '6', null);
INSERT INTO `template_depto_gas` VALUES ('86', 'GC-108 , GUARDACOSTAS \"CAPELLA\"', '6', null);
INSERT INTO `template_depto_gas` VALUES ('87', 'GUARDACOSTAS  \"SIRIUS\" GC-110', '6', null);
INSERT INTO `template_depto_gas` VALUES ('88', 'GUARDACOSTAS \" PROCION \" GC-103', '6', null);
INSERT INTO `template_depto_gas` VALUES ('89', 'GUARDACOSTAS \"ALDEBARAN\" GC-104', '6', null);
INSERT INTO `template_depto_gas` VALUES ('90', 'GUARDACOSTAS \"ALTAIR\", GC-112', '6', null);
INSERT INTO `template_depto_gas` VALUES ('91', 'GUARDACOSTAS \"ANTARES\" GC-105', '6', null);
INSERT INTO `template_depto_gas` VALUES ('92', 'GUARDACOSTAS \"ARIES\" GC-101', '6', null);
INSERT INTO `template_depto_gas` VALUES ('93', 'GUARDACOSTAS \"CANOPUS\" GC-107', '6', null);
INSERT INTO `template_depto_gas` VALUES ('94', 'GUARDACOSTAS \"ORIÓN, GC-109', '6', null);
INSERT INTO `template_depto_gas` VALUES ('95', 'LA-8, LANCHA AUX. \"BEATA\"', '6', null);
INSERT INTO `template_depto_gas` VALUES ('96', 'LANCHA \"SEA GIPSY\"', '6', null);
INSERT INTO `template_depto_gas` VALUES ('97', 'LANCHA AUXILIAR LA-2 \"CAYO VIGIA, (ARD)', '6', null);
INSERT INTO `template_depto_gas` VALUES ('98', 'LANCHA DE DESEMBARCO \" NEYBA\" LD-31', '6', null);
INSERT INTO `template_depto_gas` VALUES ('99', 'LANCHA DE RAPIDA \"POLLUX\", LR-156', '6', null);
INSERT INTO `template_depto_gas` VALUES ('100', 'LANCHA DE RESCATE \"ACAMAR\" LR-154', '6', null);
INSERT INTO `template_depto_gas` VALUES ('101', 'LANCHA DE RESCATE \"DENEB\" LR-153', '6', null);
INSERT INTO `template_depto_gas` VALUES ('102', 'LANCHA DE RESCATE \"DUBHE\"LR-164, (ARD)', '6', null);
INSERT INTO `template_depto_gas` VALUES ('103', 'LANCHA DE RESCATE \"HAMAL\" LR-151, (ARD)', '6', null);
INSERT INTO `template_depto_gas` VALUES ('104', 'LANCHA INTERCEPTORA \"CASTOR\" LI-155, ARD.', '6', null);
INSERT INTO `template_depto_gas` VALUES ('105', 'LANCHA INTERCEPTORA LI- 165 \'\'REGULUS\'\'', '6', null);
INSERT INTO `template_depto_gas` VALUES ('106', 'LANCHA INTERCEPTORA LI- 169 \'\'ALGENIB\'\'', '6', null);
INSERT INTO `template_depto_gas` VALUES ('107', 'LANCHA INTERCEPTORA LI-166 \'\'DENEBOLA\'\'', '6', null);
INSERT INTO `template_depto_gas` VALUES ('108', 'LANCHA INTERCEPTORA LI-167 \'\'ACRUX\'\'', '6', null);
INSERT INTO `template_depto_gas` VALUES ('109', 'LANCHA RAPIDA \"ATRIA\", LR-157', '6', null);
INSERT INTO `template_depto_gas` VALUES ('110', 'LANCHA RAPIDA \"ELNATH\" LR-161', '6', null);
INSERT INTO `template_depto_gas` VALUES ('111', 'LANCHA RAPIDA \"ENIF\" LR-159, (ARD)', '6', null);
INSERT INTO `template_depto_gas` VALUES ('112', 'LANCHA RAPIDA \"NUNKI\" LR-163', '6', null);
INSERT INTO `template_depto_gas` VALUES ('113', 'LANCHA RAPIDA \"POLARIS\" LR-162', '6', null);
INSERT INTO `template_depto_gas` VALUES ('114', 'LANCHA RAPIDA \"RIGEL\" LR-160', '6', null);
INSERT INTO `template_depto_gas` VALUES ('115', 'LANCHA RÁPIDA INTERCEPTORÄ \"SHAULA\", LR-158', '6', null);
INSERT INTO `template_depto_gas` VALUES ('116', 'PATRULLERO MEDIANO  \"CAPOTILLO\" PM-204', '6', null);
INSERT INTO `template_depto_gas` VALUES ('117', 'PATRULLERO MEDIANO \"TORTUGUERO\" PM-203.', '6', null);
INSERT INTO `template_depto_gas` VALUES ('118', 'REMOLCADOR \"GUAROA\" RM-3. (ARD)', '6', null);
INSERT INTO `template_depto_gas` VALUES ('119', 'RM-2, REMOLCADOR \"GUARIONEX\" , (ARD)', '6', null);
