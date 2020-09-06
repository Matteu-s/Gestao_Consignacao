
-- -----------------------------------------------------
-- Schema retaguarda
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `retaguarda` DEFAULT CHARACTER SET utf8 ;
USE `retaguarda` ;
-- -----------------------------------------------------
-- Table `retaguarda`.`Vendas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `retaguarda`.`Vendas` (
  `CodLote` INT NOT NULL AUTO_INCREMENT,
  `NomeProduto` VARCHAR(40) NULL,
  `PrecoProduto` DOUBLE NULL,
  `SaidaDiaria` INT NULL,
  `TotalSaidaDiariaFin` DOUBLE NULL,
  `Devolucoes` INT NULL,
  `SaidaMenosDevolucoes` INT NULL, 
  `ValorTotalAtual` DOUBLE NULL,
  `Cobrador` VARCHAR(15) NULL,
  `Rota` VARCHAR(40) NULL,
  `DataSaida`timestamp null default current_timestamp,
  `SubtracaoConsignados` int null,
  PRIMARY KEY (`CodLote`))
ENGINE = InnoDB;
-------------------------------------------------------
-- Table `retaguarda`.`Registros`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `retaguarda`.`Registros` (
  `CodRegistro` INT NOT NULL AUTO_INCREMENT,
  `ValorRecebido` DOUBLE NULL,
  `ProdutosDevolvidos` INT NULL,
  `Promissorias` INT NOT NULL,
  `Consignados` INT NULL,
  `ProdutoTotalDevolvido` INT NULL,
  `ValorTotalRecebido` DOUBLE NULL,
  `ValorTotalConsignado` INT NULL,
  `ProdutosPendente` INT NULL,
  `DataRegistro` timestamp null default current_timestamp,
  `Vendas_CodLote` INT NOT NULL,
  `NomeProduto` VARCHAR(40) NULL,
  `Cobrador` VARCHAR(15) NULL,
  `Rota` VARCHAR(40) NULL,
  `PrecoProduto` DOUBLE NULL, 
  `SubtracaoConsignados` INT NULL,
  PRIMARY KEY (`CodRegistro`, `Vendas_CodLote`),
  INDEX `fk_Registros_Vendas_idx` (`Vendas_CodLote` ASC) VISIBLE,
  CONSTRAINT `fk_Registros_Vendas`
    FOREIGN KEY (`Vendas_CodLote`)
    REFERENCES `retaguarda`.`Vendas` (`CodLote`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `Retaguarda`.`Apuracao` (
  `CodApuracao` INT NOT NULL AUTO_INCREMENT,
  `Receitas` DOUBLE NULL,
  `Despesas` DOUBLE NULL,
  `Faturamento` DOUBLE NULL,
  `DataDe` DATETIME NULL,
  `DataAte` DATETIME NULL,
  `DataApuracao` timestamp null default current_timestamp,
  PRIMARY KEY (`CodApuracao`))
ENGINE = InnoDB;
SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

Select * from Registros;

select * from registros where promissorias = 1232;

select sum(ValorRecebido) from registros where DataRegistro between '2019-10-10 00:00:00' and '2020-01-03 00:00:00';
select sum(ValorRecebido), sum(ProdutosDevolvidos), sum(Consignados), PrecoProduto from Registros where Promissorias = 1 and NomeProduto = 'BERMUDA - B34';

SELECT * FROM VENDAS;

Select distinct Promissorias, NomeProduto, ValorTotalConsignado, ValorTotalRecebido, ProdutoTotalDevolvido, ProdutosPendente, Cobrador, Rota from registros where Promissorias = 14 order by Promissorias;

SELECT v.NomeProduto, v.DataSaida, v.SubtracaoConsignados from vendas as v where CodLote = 5;