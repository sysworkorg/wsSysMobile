-- =============================================
-- Author:		Diego Martinez
-- Create date: 20/02/2012
-- Description:	Obtiene Registros Paginados de Articulos
-- =============================================
CREATE PROCEDURE [dbo].[wsSysMobileSPPaginacionArticulos] 
	@PageNumber int,
	@PageSize int
AS

	DECLARE @PageN int
	DECLARE @tampag int
	
	IF @pageNumber <= 1 SET @PageN = 1
	IF @pageNumber > 1 SET @PageN = @pageNumber-1

	SET @tampag=@pageSize*@PageN

	DECLARE @ultimo nvarchar(25)
	SET ROWCOUNT @tampag
	SELECT @ultimo=idArticulo FROM wsSysMobileArticulos ORDER BY 1

	IF  @pageNumber=1
		BEGIN 
			SET ROWCOUNT @pageSize
			SELECT idArticulo,descripcion,idRubro,impuestos,tasaIva,exento,precio1,precio2,precio3,precio4,precio5,precio6,precio7,precio8,precio9,precio10
				FROM wsSysMobileArticulos
					ORDER BY 1
		END
	ELSE
		BEGIN
			SET ROWCOUNT @pageSize
			SELECT idArticulo,descripcion,idRubro,impuestos,tasaIva,exento,precio1,precio2,precio3,precio4,precio5,precio6,precio7,precio8,precio9,precio10
				FROM wsSysMobileArticulos
					WHERE idArticulo > @ultimo
						ORDER BY 1
		END

		SET ROWCOUNT 0
GO
-- =============================================
-- Author:		Diego Martinez
-- Create date: 20/02/2012
-- Description:	Obtiene Registros Paginados de Clientes
-- =============================================
CREATE PROCEDURE [dbo].[wsSysMobileSPPaginacionClientes] 
	@PageNumber int,
	@PageSize int
AS

	DECLARE @PageN int
	DECLARE @tampag int
	
	IF @pageNumber <= 1 SET @PageN = 1
	IF @pageNumber > 1 SET @PageN = @pageNumber-1

	SET @tampag=@pageSize*@PageN

	DECLARE @ultimo nvarchar(15)
	SET ROWCOUNT @tampag
	SELECT @ultimo=codigo FROM wsSysMobileClientes ORDER BY 1

	IF  @pageNumber=1
		BEGIN 
			SET ROWCOUNT @pageSize
			SELECT codigo,codigoOpcional,Razon_Social,calle,numero,piso,departamento,localidad,numero_documento,iva,clase,descuento,cpteDefault,idVendedor,telefono,mail
				FROM wsSysMobileClientes
					ORDER BY 1
		END
	ELSE
		BEGIN
			SET ROWCOUNT @pageSize
			SELECT codigo,codigoOpcional,Razon_Social,calle,numero,piso,departamento,localidad,numero_documento,iva,clase,descuento,cpteDefault,idVendedor,telefono,mail
				FROM wsSysMobileClientes
					WHERE codigo > @ultimo
						ORDER BY 1
		END

		SET ROWCOUNT 0
GO
