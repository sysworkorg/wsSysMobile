CREATE VIEW [dbo].[wsSysMobileArticulos]
AS
SELECT     LTRIM(IDARTICULO) AS idArticulo, DESCRIPCION, LTRIM(ISNULL(IDRUBRO, '')) AS IdRubro, ISNULL(IMPUESTOS, 0) AS Impuestos, TasaIVA, EXENTO, PRECIO1, 
                      PRECIO2, PRECIO3, PRECIO4, PRECIO5, PRECIO6, PRECIO7, PRECIO8, PRECIO9, PRECIO10
FROM         dbo.V_MA_ARTICULOS
WHERE     (SUSPENDIDO = 0) AND (SUSPENDIDOV = 0) AND (SUSPENDIDOGM = 0)
GO




CREATE VIEW [dbo].[wsSysMobileClientes]
AS
SELECT     LTRIM(CODIGO) AS CODIGO, ISNULL(CODIGOOPCIONAL, CODIGO) AS codigoOpcional, RAZON_SOCIAL, CALLE, NUMERO, PISO, DEPARTAMENTO, 
                      ISNULL(LOCALIDAD, '') AS localidad, ISNULL(NUMERO_DOCUMENTO, '') AS numero_documento, ISNULL(IVA, '1') AS iva, ISNULL(Clase, 1) AS Clase, 
                      ISNULL(Descuento, 0) AS descuento, ISNULL(COMPROBANTEPREDETERMINADO_VENTAS, 'FP') AS cpteDefault, LTRIM(ISNULL(IdVendedor, '')) AS idVendedor, 
                      TELEFONO, MAIL
FROM         dbo.Vt_Clientes
GO




CREATE VIEW [dbo].[wsSysMobileDepositos]
AS
SELECT     LTRIM(IdDeposito) AS idDeposito, Descripcion
FROM         dbo.V_TA_DEPOSITO
GO





CREATE VIEW [dbo].[wsSysMobileRubros]
AS
SELECT     LTRIM(IdRubro) AS idRubro, Descripcion
FROM         dbo.V_TA_Rubros
GO




CREATE VIEW [dbo].[wsSysMobileStockArticulos]
AS
SELECT     LTRIM(dbo.STK_SALDOS_Unidades.IDArticulo) AS idArticulo, SUM(dbo.STK_SALDOS_Unidades.Stock) AS Stock
FROM         dbo.STK_SALDOS_Unidades WITH (NOLOCK) INNER JOIN
                      dbo.V_MA_ARTICULOS ON dbo.STK_SALDOS_Unidades.IDArticulo = dbo.V_MA_ARTICULOS.IDARTICULO AND 
                      dbo.STK_SALDOS_Unidades.equivalencia = dbo.V_MA_ARTICULOS.IDUNIDAD
GROUP BY dbo.STK_SALDOS_Unidades.IDArticulo
GO




CREATE VIEW [dbo].[wsSysMobileStockArticulosDepositos]
AS
SELECT     LTRIM(dbo.STK_SALDOS_Unidades.IDArticulo) AS idArticulo, LTRIM(dbo.STK_SALDOS_Unidades.IdDeposito) AS idDeposito, SUM(dbo.STK_SALDOS_Unidades.Stock) 
                      AS Stock
FROM         dbo.STK_SALDOS_Unidades WITH (NOLOCK) INNER JOIN
                      dbo.V_MA_ARTICULOS ON dbo.STK_SALDOS_Unidades.IDArticulo = dbo.V_MA_ARTICULOS.IDARTICULO AND 
                      dbo.STK_SALDOS_Unidades.equivalencia = dbo.V_MA_ARTICULOS.IDUNIDAD
GROUP BY dbo.STK_SALDOS_Unidades.IDArticulo, dbo.STK_SALDOS_Unidades.IdDeposito
GO




CREATE VIEW [dbo].[wsSysMobileStockComprometidoArticulos]
AS
SELECT     dbo.V_MV_CpteInsumos.IDARTICULO, SUM(dbo.V_MV_CpteInsumos.CANTIDAD) AS Stock
FROM         dbo.V_NP_Pendientes LEFT OUTER JOIN
                      dbo.V_MV_CpteInsumos ON dbo.V_NP_Pendientes.TC = dbo.V_MV_CpteInsumos.TC AND 
                      dbo.V_NP_Pendientes.IDCOMPROBANTE = dbo.V_MV_CpteInsumos.IDCOMPROBANTE AND 
                      dbo.V_NP_Pendientes.IDCOMPLEMENTO = dbo.V_MV_CpteInsumos.IDCOMPLEMENTO
GROUP BY dbo.V_MV_CpteInsumos.IDARTICULO
GO


CREATE VIEW [dbo].[wsSysMobileTotalRegistrosTablas]
AS
SELECT     'wsSysMobileArticulos' AS TABLA, COUNT(IDARTICULO) AS CANTIDAD
FROM         wsSysMobileArticulos
UNION
SELECT     'wsSysMobileClientes' AS TABLA, COUNT(CODIGO) AS CANTIDAD
FROM         wsSysMobileClientes
UNION
SELECT     'wsSysMobileRubros' AS TABLA, COUNT(IDRUBRO) AS CANTIDAD
FROM         wsSysMobileRubros
UNION
SELECT     'wsSysMobileVendedores' AS TABLA, COUNT(IDVENDEDOR) AS CANTIDAD
FROM         wsSysMobileVendedores
UNION
SELECT     'wsSysMobileDepositos' AS TABLA, COUNT(IDDEPOSITO) AS CANTIDAD
FROM         wsSysMobileDepositos
GO


CREATE VIEW [dbo].[wsSysMobileVendedores]
AS
SELECT     LTRIM(IdVendedor) AS idVendedor, Nombre, ISNULL(CodigoValidacion, '') AS codigoValidacion
FROM         dbo.V_TA_VENDEDORES
GO

