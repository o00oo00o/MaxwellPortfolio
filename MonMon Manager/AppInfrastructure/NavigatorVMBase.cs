
using System.Windows.Controls;
namespace MaxExperiment.AppInfrastructure
{
    public abstract class NavigatorVMBase : BaseAppVM
    {
        /// <summary>
        /// Button Name for navigation.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Return the UserControl.
        /// </summary>
        public abstract UserControl Control { get; }
    }
}
