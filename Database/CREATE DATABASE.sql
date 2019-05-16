use OCEANIC


create table GOODSTYPE (
  ID int ,
  CODE varchar(50),
  NAME varchar(100),
  PRIMARY KEY( ID)
)


create table SIZE(
  ID int,
  TYPE varchar(2),
  MAXHEIGHT int,
  MAXDEPTH int,
  MAXBREADTH int,
  PRIMARY KEY( ID)
)



CREATE TABLE PRICE (
  ID int,
  SIZEID int,
  MAXWEIGHT int,
  PRICE varchar(50),
  PRIMARY KEY(ID),
  FOREIGN KEY(SIZEID) REFERENCES Size(ID)
)


CREATE TABLE  EXTRAFEE (
  ID int,
  GOODSTYPEID int,
  EXTRAFEE int,
  PRIMARY KEY(ID),
  FOREIGN KEY(GOODSTYPEID) REFERENCES GoodsType(ID)

)



CREATE TABLE TRANSPORTTYPE(
  ID int,
  CODE  varchar(25),
  NAME varchar(50),
  PRIMARY KEY(ID)
)


CREATE TABLE CITY (
  ID int PRIMARY KEY,
  CODE varchar(25),
  NAME varchar(100),
  ISACTIVE BIT
)

CREATE TABLE ROUTE (
  ID int PRIMARY key,
  FROMCITYID int,
  TOCITYID int,
  TRANSPORTTYPEID int,
  HOURS int,
  ISACTIVE TINYINT,
  FOREIGN KEY(TOCITYID) REFERENCES CITY(ID),
  FOREIGN KEY(FROMCITYID) REFERENCES CITY(ID),
  FOREIGN KEY(TRANSPORTTYPEID) REFERENCES TRANSPORTTYPE(ID)
)



CREATE TABLE ORDERDELIVERY (
    ID int PRIMARY key,
    FROMCITYID int,
    TOCITYID int,
    GOODSTYPEID int,
    STARTDATE datetime,
    ARIVALDATE datetime,
    TOTALFEE int,
    ROUTEIDS varchar(500),
  FOREIGN KEY(TOCITYID) REFERENCES CITY(ID),
   FOREIGN KEY(FROMCITYID) REFERENCES CITY(ID),
      FOREIGN KEY(GOODSTYPEID) REFERENCES GoodsType(ID),
)
----------------------------------------------------
DELETE FROM ROUTE
DELETE FROM CITY
DELETE FROM PRICE
DELETE FROM EXTRAFEE
DELETE FROM GOODSTYPE 
DELETE FROM SIZE
DELETE FROM TRANSPORTTYPE





-----------------------------------------------

INSERT INTO TRANSPORTTYPE VALUES (1,'Car','Car')
INSERT INTO TRANSPORTTYPE VALUES (2,'Sea','Sea')
INSERT INTO TRANSPORTTYPE VALUES (3,'Airplane','Airplane')


INSERT INTO SIZE VALUES (1, 'A', 25, 25, 25)
INSERT INTO SIZE VALUES (2, 'B', 40, 40, 40)
INSERT INTO SIZE VALUES (3, 'C', 200, 200, 200)


INSERT INTO GOODSTYPE VALUES (1, 'RED', 'Recorded Delivery')
INSERT INTO GOODSTYPE VALUES (2, 'WEA', 'Weapons')
INSERT INTO GOODSTYPE VALUES (3, 'LIA', 'Live Animals')
INSERT INTO GOODSTYPE VALUES (4, 'CAP', 'Cautious Parcels')
INSERT INTO GOODSTYPE VALUES (5, 'REF', 'Refrigerated Goods')


INSERT INTO EXTRAFEE VALUES (1,1,0)
INSERT INTO EXTRAFEE VALUES (2,2,100)
INSERT INTO EXTRAFEE VALUES (3,3,0)
INSERT INTO EXTRAFEE VALUES (4,4,75)
INSERT INTO EXTRAFEE VALUES (5,5,10)

INSERT INTO PRICE VALUES (1, 1, 1, 40)
INSERT INTO PRICE VALUES (2, 1, 6, 60)
INSERT INTO PRICE VALUES (3, 1, 20, 80)
INSERT INTO PRICE VALUES (4, 2, 1, 48)
INSERT INTO PRICE VALUES (5, 2, 6, 68)
INSERT INTO PRICE VALUES (6, 2, 20, 88)
INSERT INTO PRICE VALUES (7, 3, 1, 80)
INSERT INTO PRICE VALUES (8, 3, 6, 100)
INSERT INTO PRICE VALUES (9, 3, 20, 120)


INSERT INTO CITY (ID, CODE, NAME, ISACTIVE) VALUES
(4, 'DKO', 'DE KANARISKE OER', 1),
(5, 'TUN', 'TUNIS', 1),
(6, 'SUA', 'SUAKIN', 1),
(7, 'TAN', 'TANGER', 1),
(8, 'CAI', 'CAIRO', 1),
(9, 'KAG', 'KAP GUARDAFUI', 1),
(10, 'AMA', 'AMATAVE', 1),
(11, 'MOC', 'MOCAMBIQUE', 1),
(12, 'KAM', 'KAP ST.MARIE', 1),
(13, 'KAP', 'KAPSTADEN', 1),
(14, 'HVA', 'HVALBUGTEN', 1),
(15, 'HEL', 'ST. HELENA', 1),
(16, 'SLA', 'SLAVEKYSTEN', 1),
(17, 'GUL', 'GULDKYSTEN', 1),
(18, 'SIL', 'SIERRA LEONE', 1),
(19, 'DAK', 'DAKAR', 1),
(20, 'MAR', 'MARRAKESH', 1),
(21, 'SAH', 'SAHARA', 1),
(22, 'TIM', 'TIMBUKTU', 1),
(23, 'WAD', 'WADAI', 1),
(24, 'DAR', 'DARFUR', 1),
(25, 'CON', 'CONGO', 1),
(26, 'OMD', 'OMDURMAN', 1),
(27, 'TRI', 'TRIPOLI', 1),
(28, 'LUA', 'LUANDA', 1),
(29, 'KAB', 'KABALO', 1),
(30, 'VIF', 'VICTORIAFALDENE', 1),
(31, 'BAG', 'BAHREL GHAZAL', 1),
(32, 'ADA', 'ADDIS ABEBA', 1),
(33, 'VIS', 'VICTORIASOEN', 1),
(34, 'ZAN', 'ZANZIBAR', 1),
(35, 'DRA', 'DRAGEBJERGET', 1);


INSERT INTO ROUTE VALUES(1,7,27,3,8,1)
INSERT INTO ROUTE VALUES(2,27,7,3,8,1)
INSERT INTO ROUTE VALUES(3,7,20,3,8,1)
INSERT INTO ROUTE VALUES(4,20,7,3,8,1)
INSERT INTO ROUTE VALUES(5,20,18,3,8,1)
INSERT INTO ROUTE VALUES(6,28,20,3,8,1)
INSERT INTO ROUTE VALUES(7,20,17,3,8,1)
INSERT INTO ROUTE VALUES(8,17,20,3,8,1)
INSERT INTO ROUTE VALUES(9,18,15,3,8,1)
INSERT INTO ROUTE VALUES(10,15,18,3,8,1)
INSERT INTO ROUTE VALUES(11,15,13,3,8,1)
INSERT INTO ROUTE VALUES(12,13,15,3,8,1)
INSERT INTO ROUTE VALUES(13,13,14,3,8,1)
INSERT INTO ROUTE VALUES(14,14,13,3,8,1)
INSERT INTO ROUTE VALUES(15,13,29,3,8,1)
INSERT INTO ROUTE VALUES(16,29,13,3,8,1)
INSERT INTO ROUTE VALUES(17,13,35,3,8,1)
INSERT INTO ROUTE VALUES(18,35,13,3,8,1)
INSERT INTO ROUTE VALUES(19,13,10,3,8,1)
INSERT INTO ROUTE VALUES(20,10,13,3,8,1)
INSERT INTO ROUTE VALUES(21,13,12,3,8,1)
INSERT INTO ROUTE VALUES(22,12,13,3,8,1)
INSERT INTO ROUTE VALUES(23,14,17,3,8,1)
INSERT INTO ROUTE VALUES(24,17,14,3,8,1)
INSERT INTO ROUTE VALUES(25,14,28,3,8,1)
INSERT INTO ROUTE VALUES(26,28,14,3,8,1)
INSERT INTO ROUTE VALUES(27,17,20,3,8,1)
INSERT INTO ROUTE VALUES(28,20,17,3,8,1)
INSERT INTO ROUTE VALUES(29,17,27,3,8,1)
INSERT INTO ROUTE VALUES(30,27,17,3,8,1)
INSERT INTO ROUTE VALUES(31,17,28,3,8,1)
INSERT INTO ROUTE VALUES(32,28,17,3,8,1)
INSERT INTO ROUTE VALUES(33,27,24,3,8,1)
INSERT INTO ROUTE VALUES(34,24,27,3,8,1)
INSERT INTO ROUTE VALUES(35,24,6,3,8,1)
INSERT INTO ROUTE VALUES(36,6,24,3,8,1)
INSERT INTO ROUTE VALUES(37,24,29,3,8,1)
INSERT INTO ROUTE VALUES(38,29,24,3,8,1)
INSERT INTO ROUTE VALUES(39,6,8,3,8,1)
INSERT INTO ROUTE VALUES(40,8,6,3,8,1)
INSERT INTO ROUTE VALUES(41,6,33,3,8,1)
INSERT INTO ROUTE VALUES(42,33,6,3,8,1)
INSERT INTO ROUTE VALUES(43,33,9,3,8,1)
INSERT INTO ROUTE VALUES(44,9,33,3,8,1)
INSERT INTO ROUTE VALUES(45,33,35,3,8,1)
INSERT INTO ROUTE VALUES(46,35,33,3,8,1)
INSERT INTO ROUTE VALUES(47,9,10,3,8,1)
INSERT INTO ROUTE VALUES(48,10,9,3,8,1)


/*-------
DROP TABLE ROUTE

DROP TABLE EXTRAFEE
DROP TABLE ORDERDELIVERY

DROP TABLE TRANSPORTTYPE
DROP TABLE PRICE
DROP TABLE GOODSTYPE 
DROP TABLE CITY
DROP TABLE SIZE
*/