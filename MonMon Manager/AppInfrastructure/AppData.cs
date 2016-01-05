using System.Windows;
using MaxExperiment.Util;

namespace MaxExperiment.AppInfrastructure
{
    /// <summary>
    /// Static Class to hold shared variable for Application Wide Scope.
    /// </summary>
    public static class AppData
    {
        /// <summary>
        /// Application Database connection.
        /// This property will be accessed by all ViewModel in their constructor.
        /// </summary>
        public static IDBConnectorEF DBConn;

        /// <summary>
        /// MainWindow View for centering popup window.
        /// </summary>
        public static Window MainWindowView;
    }
}
