-- 1. Eliminar la tabla si ya existe
IF OBJECT_ID('Horarios', 'U') IS NOT NULL
    DROP TABLE Horarios;

-- 2. Crear la tabla de nuevo con solo la columna EmpleadoId
CREATE TABLE Horarios (
    EmpleadoId INT NOT NULL,
    Tipo VARCHAR(50) NOT NULL,           -- entrada, salida, descanso
    Hora TIME NOT NULL,
    Fecha DATE NOT NULL,
    Observaciones NVARCHAR(255),

    CONSTRAINT PK_Horarios PRIMARY KEY (EmpleadoId, Fecha, Tipo) -- Clave compuesta si es necesario permitir varios registros por empleado y día
);

INSERT INTO Horarios (EmpleadoId, Tipo, Hora, Fecha, Observaciones)
VALUES 
(1, 'entrada', '07:00:00', '2025-05-15', 'Llega puntual'),
(1, 'salida',  '15:00:00', '2025-05-15', 'Salida normal'),
(1, 'descanso','11:00:00', '2025-05-15', 'Pausa para almuerzo'),
(2, 'entrada', '08:00:00', '2025-05-15', 'Llegó un poco tarde'),
(2, 'salida',  '16:00:00', '2025-05-15', 'Salida después de hora');