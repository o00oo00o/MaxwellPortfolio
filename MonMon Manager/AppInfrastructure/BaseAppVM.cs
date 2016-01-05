using MaxExperiment.Util;

namespace MaxExperiment.AppInfrastructure
{
    /// <summary>
    /// Add Database Connection Object property and initialized it from AppData.
    /// </summary>
    public abstract class BaseAppVM : BaseVM
    {
        /// <summary>
        /// Database Connector Oject.
        /// </summary>
        protected IDBConnectorEF dbconn;

        /// <summary>
        /// Init database connection and parent window
        /// </summary>
        /// <param name="parentWindow">Parent Window or null.</param>
        protected BaseAppVM()
        {
            dbconn = AppData.DBConn;
        }
    }
}
