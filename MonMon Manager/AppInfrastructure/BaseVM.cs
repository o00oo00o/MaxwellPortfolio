using System.ComponentModel;

namespace MaxExperiment.AppInfrastructure
{
    /// <summary>
    /// Implements INotifyPropertyChanged Interface.
    /// </summary>
    public abstract class BaseVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyname)
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion
    }
}
