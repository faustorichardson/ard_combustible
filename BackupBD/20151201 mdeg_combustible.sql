/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50627
Source Host           : localhost:3306
Source Database       : mdeg_combustible

Target Server Type    : MYSQL
Target Server Version : 50627
File Encoding         : 65001

Date: 2015-12-01 16:46:11
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `departamento_autoriza`
-- ----------------------------
DROP TABLE IF EXISTS `departamento_autoriza`;
CREATE TABLE `departamento_autoriza` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `departamento` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of departamento_autoriza
-- ----------------------------
INSERT INTO `departamento_autoriza` VALUES ('1', 'COMANDANCIA GENERAL');
INSERT INTO `departamento_autoriza` VALUES ('2', 'SUB COMANDANCIA GENERAL');

-- ----------------------------
-- Table structure for `errors`
-- ----------------------------
DROP TABLE IF EXISTS `errors`;
CREATE TABLE `errors` (
  `secuencia` int(4) NOT NULL AUTO_INCREMENT,
  `cia` decimal(2,0) NOT NULL DEFAULT '0',
  `numero` decimal(8,0) NOT NULL DEFAULT '0',
  `fecha` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `message` text NOT NULL,
  `msgoleodbc` varchar(1000) NOT NULL DEFAULT '',
  `source` text NOT NULL,
  `programa` text NOT NULL,
  `app` varchar(100) NOT NULL DEFAULT ' ',
  `apphelp` varchar(100) NOT NULL DEFAULT ' ',
  `hora` varchar(20) NOT NULL DEFAULT 'curtime()',
  `linea` decimal(6,0) NOT NULL DEFAULT '0',
  `area` char(2) NOT NULL DEFAULT '0',
  `conexion` decimal(3,0) NOT NULL DEFAULT '0',
  `parametro` varchar(100) NOT NULL DEFAULT ' ',
  `usuario` varchar(20) NOT NULL DEFAULT '',
  `userwin` varchar(100) NOT NULL DEFAULT '',
  `pc` varchar(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`secuencia`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of errors
-- ----------------------------

-- ----------------------------
-- Table structure for `existencia`
-- ----------------------------
DROP TABLE IF EXISTS `existencia`;
CREATE TABLE `existencia` (
  `tipocombustible` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  PRIMARY KEY (`tipocombustible`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of existencia
-- ----------------------------

-- ----------------------------
-- Table structure for `info`
-- ----------------------------
DROP TABLE IF EXISTS `info`;
CREATE TABLE `info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `institucion` varchar(100) NOT NULL,
  `direccion` varchar(150) NOT NULL,
  `provincia` int(11) NOT NULL,
  `telefono` varchar(12) DEFAULT NULL,
  `extension` varchar(15) DEFAULT NULL,
  `fax` varchar(12) DEFAULT NULL,
  `mail` varchar(100) DEFAULT NULL,
  `web` varchar(100) DEFAULT NULL,
  `cdtegral` int(11) NOT NULL,
  `enccomb` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of info
-- ----------------------------

-- ----------------------------
-- Table structure for `movimientocombustible`
-- ----------------------------
DROP TABLE IF EXISTS `movimientocombustible`;
CREATE TABLE `movimientocombustible` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo_movimiento` varchar(1) NOT NULL,
  `tipo_combustible` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `nota` text,
  `beneficiario` int(11) NOT NULL,
  `tipobeneficiario` int(11) NOT NULL,
  `autorizadopor` int(11) NOT NULL,
  `fecha` datetime NOT NULL,
  `id_pedido` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of movimientocombustible
-- ----------------------------

-- ----------------------------
-- Table structure for `pedidos`
-- ----------------------------
DROP TABLE IF EXISTS `pedidos`;
CREATE TABLE `pedidos` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `fecha` datetime NOT NULL,
  `enccomb` int(11) NOT NULL,
  `cdtegral` int(11) NOT NULL,
  `tipo_combustible` int(11) NOT NULL,
  `cantidad` int(11) NOT NULL,
  `nota` text NOT NULL,
  `status` bit(1) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of pedidos
-- ----------------------------

-- ----------------------------
-- Table structure for `tipobeneficiario`
-- ----------------------------
DROP TABLE IF EXISTS `tipobeneficiario`;
CREATE TABLE `tipobeneficiario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipobeneficiario` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tipobeneficiario
-- ----------------------------

-- ----------------------------
-- Table structure for `tipo_combustible`
-- ----------------------------
DROP TABLE IF EXISTS `tipo_combustible`;
CREATE TABLE `tipo_combustible` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `combustible` varchar(50) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tipo_combustible
-- ----------------------------
INSERT INTO `tipo_combustible` VALUES ('1', 'GASOLINA');
INSERT INTO `tipo_combustible` VALUES ('2', 'GASOIL');
INSERT INTO `tipo_combustible` VALUES ('3', 'GAS');
INSERT INTO `tipo_combustible` VALUES ('4', 'GAS KEROSENE');

-- ----------------------------
-- Table structure for `usuarios`
-- ----------------------------
DROP TABLE IF EXISTS `usuarios`;
CREATE TABLE `usuarios` (
  `idusuarios` int(11) NOT NULL AUTO_INCREMENT,
  `usuario` varchar(25) NOT NULL,
  `clave` varchar(25) NOT NULL,
  `status` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`idusuarios`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of usuarios
-- ----------------------------
INSERT INTO `usuarios` VALUES ('1', 'frichardson', 'frrh0414', '');

-- ----------------------------
-- Procedure structure for `AgregarError`
-- ----------------------------
DROP PROCEDURE IF EXISTS `AgregarError`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AgregarError`(IN nNumero int
      ,IN dFecha datetime
      ,IN cMessage text 
      ,IN cSource text
      ,IN cPrograma text
      ,IN cApp text
      ,IN cApphelp Char(100) 
      ,IN cHora Char(15)  
      ,IN nLinea int  
      ,IN cUsuario Char(15) 
      ,IN cUserwin Char(20) 
      ,IN cPC Char(20)	)
BEGIN
set cApphelp = ifnull(cApphelp,'');

 -- Insert statements for procedure here
	INSERT INTO errors
       (numero
      ,fecha
      ,message
      ,source
      ,programa
      ,app
      ,apphelp
      ,hora
      ,linea
      ,usuario
      ,userwin
      ,pc)
     VALUES
       (nNumero 
      ,dFecha 
      ,cMessage 
      ,cSource 
      ,cPrograma 
      ,cApp 
      ,cApphelp 
      ,cHora 
      ,nLinea 
      ,cUsuario 
      ,cUserwin 
      ,cPC);
END
;;
DELIMITER ;
