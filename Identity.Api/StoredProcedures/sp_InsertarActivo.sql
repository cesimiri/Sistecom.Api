CREATE OR ALTER PROCEDURE dbo.sp_InsertarActivo
    @IdProducto INT,
    @NumeroSerie VARCHAR(100),
    @NumeroParte VARCHAR(100) = NULL,
    @FechaAdquisicion DATE,
    @FechaGarantiaFin DATE = NULL,
    @IdFacturaCompra INT = NULL,
    @ValorCompra DECIMAL(10,2),
    @ValorResidual DECIMAL(10,2) = 0.00,
    @VidaUtilMeses INT = 36,
    @UbicacionActual VARCHAR(200) = NULL,
    @EstadoActivo VARCHAR(15) = 'DISPONIBLE',
    @CondicionFisica VARCHAR(15) = 'NUEVO',
    @EsServidor BIT = 0,
    @Observaciones NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Prefijo VARCHAR(50);
    DECLARE @NextNumber INT = 1;
    DECLARE @NuevoCodigo VARCHAR(50);
    DECLARE @UltimoCodigo VARCHAR(50);
    DECLARE @LastNumberStr VARCHAR(10);

    -- 🔹 Obtener prefijo desde la categoría del producto
    SELECT TOP 1 
           @Prefijo = UPPER(REPLACE(LTRIM(RTRIM(cp.nombre)), ' ', '-'))
    FROM productos p
    INNER JOIN marcas m ON p.id_marca = m.id_marca
    INNER JOIN categorias_productos cp ON m.id_categoria = cp.id_categoria
    WHERE p.id_producto = @IdProducto;

    -- 🔹 Buscar último código con ese prefijo
    SELECT TOP 1 @UltimoCodigo = codigo_activo
    FROM activos
    WHERE codigo_activo LIKE @Prefijo + '-%'
    ORDER BY id_activo DESC;

    -- 🔹 Si existe, incrementar el número
    IF @UltimoCodigo IS NOT NULL
    BEGIN
        SET @LastNumberStr = RIGHT(@UltimoCodigo, 4);
        IF ISNUMERIC(@LastNumberStr) = 1
            SET @NextNumber = CAST(@LastNumberStr AS INT) + 1;
    END

    -- 🔹 Generar el nuevo código
    SET @NuevoCodigo = @Prefijo + '-' + RIGHT('0000' + CAST(@NextNumber AS VARCHAR(4)), 4);

    -- 🔹 Insertar registro
    INSERT INTO activos (
        codigo_activo, id_producto, numero_serie, numero_parte, 
        fecha_adquisicion, fecha_garantia_fin, id_factura_compra, 
        valor_compra, valor_residual, vida_util_meses, ubicacion_actual, 
        estado_activo, condicion_fisica, es_servidor, observaciones, fecha_registro
    )
    VALUES (
        @NuevoCodigo, @IdProducto, @NumeroSerie, @NumeroParte,
        @FechaAdquisicion, @FechaGarantiaFin, @IdFacturaCompra,
        @ValorCompra, @ValorResidual, @VidaUtilMeses, @UbicacionActual,
        @EstadoActivo, @CondicionFisica, @EsServidor, @Observaciones, GETDATE()
    );

    -- 🔹 Retornar el ID y el nuevo código
    SELECT SCOPE_IDENTITY() AS IdActivo, @NuevoCodigo AS CodigoActivo;
END
GO
