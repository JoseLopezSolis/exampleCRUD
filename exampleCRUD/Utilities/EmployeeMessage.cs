namespace exampleCRUD.Utilities;
using exampleCRUD.DTOs;

/// <summary>
/// Clase que representa un mensaje relacionado con una operación sobre un empleado.
/// </summary>
public class EmployeeMessage
{
    /// <summary>
    /// Indica si la operación es de creación de un nuevo empleado.
    /// </summary>
    public bool isCreate { get; set; }

    /// <summary>
    /// Datos del empleado asociados al mensaje.
    /// </summary>
    public EmployeeDTO employeeDto { get; set; }
}
