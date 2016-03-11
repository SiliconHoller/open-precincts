-- ----------------------------------------------------------------------------
-- MySQL Workbench Migration
-- Migrated Schemata: voterwatch
-- Source Schemata: voterwatch
-- Created: Sat Mar 08 21:12:05 2014
-- ----------------------------------------------------------------------------

SET FOREIGN_KEY_CHECKS = 0;;

-- ----------------------------------------------------------------------------
-- Schema voterwatch
-- ----------------------------------------------------------------------------
DROP SCHEMA IF EXISTS `voterwatch` ;
CREATE SCHEMA IF NOT EXISTS `voterwatch` ;

-- ----------------------------------------------------------------------------
-- Table voterwatch.addresses
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`addresses` (
  `address_id` INT(11) NOT NULL AUTO_INCREMENT,
  `address1` VARCHAR(100) NOT NULL,
  `address2` VARCHAR(100) NULL DEFAULT NULL,
  `city` VARCHAR(50) NOT NULL,
  `state` VARCHAR(2) NOT NULL,
  `zip` VARCHAR(5) NOT NULL,
  `zip_plusfour` VARCHAR(4) NULL DEFAULT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`address_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'voter addresses';

-- ----------------------------------------------------------------------------
-- Table voterwatch.affiliations
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`affiliations` (
  `affiliationid` INT(11) NOT NULL AUTO_INCREMENT,
  `partycode` VARCHAR(10) NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`affiliationid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'party affiliations';

-- ----------------------------------------------------------------------------
-- Table voterwatch.district_relationships
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`district_relationships` (
  `districtid` INT(11) NOT NULL,
  `parentdistrict` INT(11) NOT NULL,
  `descr` VARCHAR(128) NULL DEFAULT NULL,
  PRIMARY KEY (`districtid`, `parentdistrict`),
  INDEX `fk_district_idx` (`districtid` ASC),
  INDEX `fk_parent_idx` (`parentdistrict` ASC),
  CONSTRAINT `fk_district`
    FOREIGN KEY (`districtid`)
    REFERENCES `voterwatch`.`districts` (`districtid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_parent`
    FOREIGN KEY (`parentdistrict`)
    REFERENCES `voterwatch`.`districts` (`districtid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'graph-like structure defining containment relationships between districts.  The basic relationship is that the child district is wholly contained in the parent district';

-- ----------------------------------------------------------------------------
-- Table voterwatch.districts
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`districts` (
  `districtid` INT(11) NOT NULL AUTO_INCREMENT,
  `districttypeid` INT(11) NOT NULL,
  `identifier` VARCHAR(80) NOT NULL,
  `name` VARCHAR(80) NULL DEFAULT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`districtid`),
  INDEX `fk_district_type_idx` (`districttypeid` ASC),
  CONSTRAINT `fk_district_type`
    FOREIGN KEY (`districttypeid`)
    REFERENCES `voterwatch`.`district_types` (`districttypeid`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
AUTO_INCREMENT = 21476
DEFAULT CHARACTER SET = utf8
COMMENT = 'Individual geopolitical districts defining voter populations';

-- ----------------------------------------------------------------------------
-- Table voterwatch.district_types
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`district_types` (
  `districttypeid` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`districttypeid`))
ENGINE = InnoDB
AUTO_INCREMENT = 21
DEFAULT CHARACTER SET = utf8
COMMENT = 'geopolitical definitions of voter groups';

-- ----------------------------------------------------------------------------
-- Table voterwatch.ohioraw
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`ohioraw` (
  `recordid` INT(11) NOT NULL AUTO_INCREMENT,
  `importdate` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `importidentifier` VARCHAR(45) NOT NULL,
  `sosvoterid` VARCHAR(13) NULL DEFAULT NULL,
  `countynumber` VARCHAR(2) NULL DEFAULT NULL,
  `countyid` VARCHAR(50) NULL DEFAULT NULL,
  `lastname` VARCHAR(50) NULL DEFAULT NULL,
  `firstname` VARCHAR(50) NULL DEFAULT NULL,
  `middlename` VARCHAR(50) NULL DEFAULT NULL,
  `suffix` VARCHAR(10) NULL DEFAULT NULL,
  `yearofbirth` VARCHAR(10) NULL DEFAULT NULL,
  `registrationdate` VARCHAR(10) NULL DEFAULT NULL,
  `partyaffiliation` VARCHAR(1) NULL DEFAULT NULL,
  `resaddress1` VARCHAR(100) NULL DEFAULT NULL,
  `resaddress2` VARCHAR(100) NULL DEFAULT NULL,
  `rescity` VARCHAR(50) NULL DEFAULT NULL,
  `resstate` VARCHAR(20) NULL DEFAULT NULL,
  `reszip` VARCHAR(5) NULL DEFAULT NULL,
  `reszipplusfour` VARCHAR(4) NULL DEFAULT NULL,
  `rescountry` VARCHAR(50) NULL DEFAULT NULL,
  `respostalcode` VARCHAR(10) NULL DEFAULT NULL,
  `mailaddress1` VARCHAR(100) NULL DEFAULT NULL,
  `mailaddress2` VARCHAR(100) NULL DEFAULT NULL,
  `mailcity` VARCHAR(50) NULL DEFAULT NULL,
  `mailstate` VARCHAR(20) NULL DEFAULT NULL,
  `mailzip` VARCHAR(5) NULL DEFAULT NULL,
  `mailziplusfour` VARCHAR(4) NULL DEFAULT NULL,
  `mailcountry` VARCHAR(50) NULL DEFAULT NULL,
  `mailpostalcode` VARCHAR(10) NULL DEFAULT NULL,
  `careercenter` VARCHAR(80) NULL DEFAULT NULL,
  `city` VARCHAR(80) NULL DEFAULT NULL,
  `cityschooldistrict` VARCHAR(80) NULL DEFAULT NULL,
  `countycourtdistrict` VARCHAR(80) NULL DEFAULT NULL,
  `congressionaldiscrict` VARCHAR(2) NULL DEFAULT NULL,
  `courtofappeals` VARCHAR(2) NULL DEFAULT NULL,
  `educationservicecenter` VARCHAR(80) NULL DEFAULT NULL,
  `exemptedvillageschooldistrict` VARCHAR(80) NULL DEFAULT NULL,
  `librarydistrict` VARCHAR(80) NULL DEFAULT NULL,
  `localschooldistrict` VARCHAR(80) NULL DEFAULT NULL,
  `municipalcourtdiscrict` VARCHAR(80) NULL DEFAULT NULL,
  `precinct` VARCHAR(80) NULL DEFAULT NULL,
  `precinctcode` VARCHAR(40) NULL DEFAULT NULL,
  `stateboardofeducation` VARCHAR(2) NULL DEFAULT NULL,
  `staterepdistrict` VARCHAR(2) NULL DEFAULT NULL,
  `statesenatedistrict` VARCHAR(2) NULL DEFAULT NULL,
  `township` VARCHAR(40) NULL DEFAULT NULL,
  `village` VARCHAR(40) NULL DEFAULT NULL,
  `ward` VARCHAR(40) NULL DEFAULT NULL,
  `rawelectionlist` LONGTEXT NULL DEFAULT NULL,
  `rawvotingdata` LONGTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`recordid`))
ENGINE = InnoDB
AUTO_INCREMENT = 46889
DEFAULT CHARACTER SET = utf8
COMMENT = 'Raw data import from SOS voter information';

-- ----------------------------------------------------------------------------
-- Table voterwatch.roles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`roles` (
  `roleid` INT(11) NOT NULL AUTO_INCREMENT,
  `rolename` VARCHAR(45) NOT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`roleid`))
ENGINE = InnoDB
AUTO_INCREMENT = 3
DEFAULT CHARACTER SET = utf8
COMMENT = 'List of possible user roles';

-- ----------------------------------------------------------------------------
-- Table voterwatch.tally
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`tally` (
  `tally_id` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(128) NOT NULL,
  `ttype_id` INT(11) NOT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  `event_start` DATE NULL DEFAULT NULL,
  `event_end` DATE NULL DEFAULT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`tally_id`),
  INDEX `FK_tally_type_idx` (`ttype_id` ASC),
  CONSTRAINT `FK_tally_type`
    FOREIGN KEY (`ttype_id`)
    REFERENCES `voterwatch`.`tally_type` (`ttype_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'A counting event--election, survey, GOTV campaign, etc';

-- ----------------------------------------------------------------------------
-- Table voterwatch.tally_type
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`tally_type` (
  `ttype_id` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(64) NOT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`ttype_id`),
  UNIQUE INDEX `name_UNIQUE` (`name` ASC))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'Counting event (election, survey, etc) to be collected--the principal purpose of this system';

-- ----------------------------------------------------------------------------
-- Table voterwatch.user_districts
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`user_districts` (
  `userid` INT(11) NOT NULL,
  `districtid` INT(11) NOT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`userid`, `districtid`),
  INDEX `fk_district_user_idx` (`userid` ASC),
  INDEX `fk_user_district_idx` (`districtid` ASC),
  CONSTRAINT `fk_district_user`
    FOREIGN KEY (`userid`)
    REFERENCES `voterwatch`.`users` (`userid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_district`
    FOREIGN KEY (`districtid`)
    REFERENCES `voterwatch`.`districts` (`districtid`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'mapping between users and their districts of responsibility.  Along with specific permissions, this results in the available actions that a user may take for a district and sub district';

-- ----------------------------------------------------------------------------
-- Table voterwatch.users
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`users` (
  `userid` INT(11) NOT NULL AUTO_INCREMENT,
  `firstname` VARCHAR(45) NOT NULL,
  `lastname` VARCHAR(45) NOT NULL,
  `emailaddress` VARCHAR(45) NOT NULL,
  `userpass` MEDIUMBLOB NULL DEFAULT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  PRIMARY KEY (`userid`))
ENGINE = InnoDB
AUTO_INCREMENT = 2
DEFAULT CHARACTER SET = utf8
COMMENT = 'voter watch system users';

-- ----------------------------------------------------------------------------
-- Table voterwatch.user_roles
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`user_roles` (
  `userid` INT(11) NOT NULL,
  `roleid` INT(11) NOT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`userid`, `roleid`),
  INDEX `fk_role_user_idx` (`userid` ASC),
  INDEX `fk_user_role_idx` (`roleid` ASC),
  CONSTRAINT `fk_role_user`
    FOREIGN KEY (`userid`)
    REFERENCES `voterwatch`.`users` (`userid`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_user_role`
    FOREIGN KEY (`roleid`)
    REFERENCES `voterwatch`.`roles` (`roleid`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

-- ----------------------------------------------------------------------------
-- Table voterwatch.voter_addresses
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`voter_addresses` (
  `voterid` INT(11) NOT NULL,
  `addressid` INT(11) NOT NULL,
  `address_flags` INT(11) NOT NULL DEFAULT '0',
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`voterid`, `addressid`),
  INDEX `fk_voter_idx` (`voterid` ASC),
  INDEX `fk_address_address_idx` (`addressid` ASC),
  CONSTRAINT `fk_address_address`
    FOREIGN KEY (`addressid`)
    REFERENCES `voterwatch`.`addresses` (`address_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_address_voter`
    FOREIGN KEY (`voterid`)
    REFERENCES `voterwatch`.`voters` (`voterid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'matches between voters and addresses';

-- ----------------------------------------------------------------------------
-- Table voterwatch.voters
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`voters` (
  `voterid` INT(11) NOT NULL AUTO_INCREMENT,
  `statevoterid` VARCHAR(45) NULL DEFAULT NULL,
  `countyvoterid` VARCHAR(45) NULL DEFAULT NULL,
  `lastname` VARCHAR(50) NOT NULL,
  `firstname` VARCHAR(45) NOT NULL,
  `middlename` VARCHAR(45) NULL DEFAULT NULL,
  `suffix` VARCHAR(20) NULL DEFAULT NULL,
  `yearofbirth` INT(11) NOT NULL,
  `registrationdate` DATETIME NOT NULL,
  `partyaffiliation` INT(11) NULL DEFAULT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`voterid`),
  INDEX `fk_affiliation_idx` (`partyaffiliation` ASC),
  CONSTRAINT `fk_affiliation`
    FOREIGN KEY (`partyaffiliation`)
    REFERENCES `voterwatch`.`affiliations` (`affiliationid`)
    ON DELETE SET NULL
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'basic voter data';

-- ----------------------------------------------------------------------------
-- Table voterwatch.voter_count
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`voter_count` (
  `count_id` INT(11) NOT NULL AUTO_INCREMENT,
  `tally_id` INT(11) NOT NULL,
  `voterid` INT(11) NOT NULL,
  `methodid` INT(11) NOT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`count_id`),
  INDEX `FK_voter_tally_idx` (`tally_id` ASC),
  INDEX `FK_voter_voter_idx` (`voterid` ASC),
  INDEX `FK_voter_method_idx` (`methodid` ASC),
  CONSTRAINT `FK_voter_tally`
    FOREIGN KEY (`tally_id`)
    REFERENCES `voterwatch`.`tally` (`tally_id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_voter_voter`
    FOREIGN KEY (`voterid`)
    REFERENCES `voterwatch`.`voters` (`voterid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `FK_voter_method`
    FOREIGN KEY (`methodid`)
    REFERENCES `voterwatch`.`voter_method` (`vmid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'Count of a voter--election, survey, GOTV, etc';

-- ----------------------------------------------------------------------------
-- Table voterwatch.voter_method
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`voter_method` (
  `vmid` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  `descr` MEDIUMTEXT NULL DEFAULT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`vmid`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'Means by which the voter interacted with the particular tally (phone calls for surveys are one example, absentee vs early voting vs. polling station for elections are another)';

-- ----------------------------------------------------------------------------
-- Table voterwatch.voter_districts
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS `voterwatch`.`voter_districts` (
  `voterid` INT(11) NOT NULL,
  `districtid` INT(11) NOT NULL,
  `created_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_dt` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `modified_by` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`voterid`, `districtid`),
  INDEX `fk_vd_voter_idx` (`voterid` ASC),
  INDEX `fk_vd_district_idx` (`districtid` ASC),
  CONSTRAINT `fk_vd_district`
    FOREIGN KEY (`districtid`)
    REFERENCES `voterwatch`.`districts` (`districtid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_vd_voter`
    FOREIGN KEY (`voterid`)
    REFERENCES `voterwatch`.`voters` (`voterid`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8
COMMENT = 'voter membership within districts';
SET FOREIGN_KEY_CHECKS = 1;;
