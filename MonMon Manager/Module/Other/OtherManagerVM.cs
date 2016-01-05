using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MaxExperiment.AppInfrastructure;
using MaxExperiment.Model;
using MaxExperiment.Util;

namespace MaxExperiment.Module.Other
{
    public class OtherManagerVM : NavigatorVMBase
    {
        private UserControl _view = null;

        public OtherManagerVM()
        {
            _view = new OtherManagerView();
            _view.DataContext = this;
        }

        public override string Name
        {
            get { return "Contacts"; }
        }

        public override UserControl Control
        {
            get { return _view; }
        }

        private ObservableCollection<Person> _contacts = new ObservableCollection<Person>();
        public ObservableCollection<Person> Contacts
        {
            get
            {
                return _contacts;
            }

            set
            {
                _contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        /// <summary>
        /// Load Person List
        /// </summary>
        public void Load()
        {
            using(var db = new DBContext(dbconn.GetConnection()))
            {
                var query = from t in db.Persons
                            orderby t.Name
                            select t;

                _contacts = new ObservableCollection<Person>(query);
                OnPropertyChanged("Persons");
            }
        }
    }
}
