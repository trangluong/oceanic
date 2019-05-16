
USE OCEANIC;


create table GOODSTYPE (
  ID int ,
  CODE varchar(50),
  NAME varchar(100),
  CREATEDATE Datetime,
  CREATEBY varchar(100)
  PRIMARY KEY( ID)
)

create table SIZE(
  ID int,
  TYPE varchar(2),
  MAXHEIGHT int,
  MAXDEPTH int,
  MAXBREADTH int,
  CREATEDATE Datetime,
  CREATEBY varchar(100),
  PRIMARY KEY( ID)
)

CREATE TABLE PRICE (
  ID int,
  SIZEID int,
  MAXWEIGHT int,
  PRICE varchar(50),
  CREATEDATE Datetime,
  CREATEBY varchar(100),
  PRIMARY KEY(ID),
  FOREIGN KEY(SIZEID) REFERENCES Size(ID)
)


CREATE TABLE  EXTRAFEE (
  ID int,
  GOODSTYPEID int,
  EXTRAFEE int,
  CREATEDATE Datetime,
  CREATEBY varchar(100),
  PRIMARY KEY(ID),
  FOREIGN KEY(GOODSTYPEID) REFERENCES GoodsType(ID)

)


CREATE TABLE TRANSPORTTYPE(
  ID int,
  CODE  varchar(25),
  NAME varchar(50),
  CREATEDATE Datetime,
  CREATEBY varchar(100),
  PRIMARY KEY(ID)
)




CREATE TABLE CITY (
  ID int PRIMARY KEY,
  CODE varchar(25),
  NAME varchar(100),
  ISACTIVE BIT,
  CREATEDATE Datetime,
  CREATEBY varchar(100)
)

CREATE TABLE ROUTE (
  ID int PRIMARY key,
  FROMCITYID int,
  TOCITYID int,
  TRANSPORTTYPEID int,
  LONGHOURS int,
  SEGMENTS int,
  ISACTIVE BIT ,
  CREATEDATE Datetime,
  CREATEBY varchar(100)
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



INSERT INTO CITY (ID, CODE, NAME, ISACTIVE, CREATEBY, CREATEDATE) VALUES
(4, 'DKO', 'DE KANARISKE OER', 1, 'trangln', '04/05/2019'),
(5, 'TUN', 'TUNIS', 1, 'trangln', '04/05/2019'),
(6, 'SUA', 'SUAKIN', 1, 'trangln', '04/05/2019'),
(7, 'TAN', 'TANGER', 1, 'trangln', '04/05/2019'),
(8, 'CAI', 'CAIRO', 1, 'trangln', '04/05/2019'),
(9, 'KAG', 'KAP GUARDAFUI', 1, 'trangln', '04/05/2019'),
(10, 'AMA', 'AMATAVE', 1, 'trangln', '04/05/2019'),
(11, 'MOC', 'MOCAMBIQUE', 1,'trangln', '04/05/2019'),
(12, 'KAM', 'KAP ST.MARIE', 1, 'trangln', '04/05/2019'),
(13, 'KAP', 'KAPSTADEN', 1, 'trangln', '04/05/2019'),
(14, 'HVA', 'HVALBUGTEN', 1, 'trangln', '04/05/2019'),
(15, 'HEL', 'ST. HELENA', 1, 'trangln', '04/05/2019'),
(16, 'SLA', 'SLAVEKYSTEN', 1,'trangln', '04/05/2019'),
(17, 'GUL', 'GULDKYSTEN', 1,'trangln', '04/05/2019'),
(18, 'SIL', 'SIERRA LEONE', 1,'trangln', '04/05/2019'),
(19, 'DAK', 'DAKAR', 1, 'trangln', '04/05/2019'),
(20, 'MAR', 'MARRAKESH', 1, 'trangln', '04/05/2019'),
(21, 'SAH', 'SAHARA', 1, 'trangln', '04/05/2019'),
(22, 'TIM', 'TIMBUKTU', 1, 'trangln', '04/05/2019'),
(23, 'WAD', 'WADAI', 1, 'trangln', '04/05/2019'),
(24, 'DAR', 'DARFUR', 1, 'trangln', '04/05/2019'),
(25, 'CON', 'CONGO', 1, 'trangln', '04/05/2019'),
(26, 'OMD', 'OMDURMAN', 1, 'trangln', '04/05/2019'),
(27, 'TRI', 'TRIPOLI', 1, 'trangln', '04/05/2019'),
(28, 'LUA', 'LUANDA', 1, 'trangln', '04/05/2019'),
(29, 'KAB', 'KABALO', 1, 'trangln', '04/05/2019'),
(30, 'VIF', 'VICTORIAFALDENE', 1, 'trangln', '04/05/2019'),
(31, 'BAG', 'BAHREL GHAZAL', 1, 'trangln', '04/05/2019'),
(32, 'ADA', 'ADDIS ABEBA', 1, 'trangln', '04/05/2019'),
(33, 'VIS', 'VICTORIASOEN', 1, 'trangln', '04/05/2019'),
(34, 'ZAN', 'ZANZIBAR', 1, 'trangln', '04/05/2019'),
(35, 'DRA', 'DRAGEBJERGET', 1, 'trangln', '04/05/2019');

INSERT INTO GOODSGOODSTYPE(ID, CODE, NAME, `add_percent`, `admin_id`) VALUES
(1, 'RED', 'Recorded Delivery', 0, 1),
(2, 'WEA', 'Weapons', 20, 1),
(3, 'LIA', 'Live Animals', 50, 1),
(4, 'CAP', 'Cautious Parcels', 20, 1),
(5, 'REF', 'Refrigerated Goods', 40, 1);