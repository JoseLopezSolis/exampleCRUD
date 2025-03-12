namespace exampleCRUD.Utilities;

/// <summary>
/// Clase estática que proporciona la ruta de la base de datos dependiendo de la plataforma del dispositivo.
/// </summary>
public static class ConnectionDB
{
    /// <summary>
    /// Obtiene la ruta de la base de datos según la plataforma del dispositivo.
    /// </summary>
    /// <param name="databaseName">Nombre del archivo de la base de datos.</param>
    /// <returns>Ruta completa del archivo de la base de datos.</returns>
    public static string returnRoute(string databaseName)
    {
        string routeDataBase = string.Empty;
        
        // Verifica si la plataforma es Android
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            routeDataBase = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            routeDataBase = Path.Combine(routeDataBase, databaseName);
        }
        // Verifica si la plataforma es iOS
        else if (DeviceInfo.Platform == DevicePlatform.iOS)
        {
            routeDataBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            routeDataBase = Path.Combine(routeDataBase, "..", "Library", databaseName);
        } 
        
        return routeDataBase;
    }
}
