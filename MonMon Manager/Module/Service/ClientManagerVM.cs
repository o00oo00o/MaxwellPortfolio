using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using MaxExperiment.AppInfrastructure;
using MaxExperiment.Model;
using MaxExperiment.Util;

namespace MaxExperiment.Module.Service
{
    public class ClientManagerVM : NavigatorVMBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        private UserControl _view = null;
        public bool IsContactSelected { get; set; }

        #region NavigatorVMBase
        public override string Name
        {
            get { return "Customer"; }
        }

        public override UserControl Control
        {
            get { return _view; }
        }
        #endregion

        public ClientManagerVM() 
        {
            _view = new ClientManagerView();
            _view.DataContext = this;

            Load();

            LoadCommand = new RelayAsyncCommand(Load);
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);

            IsContactSelected = false;
            OnPropertyChanged("IsContactSelected");
        }

        #region Contact List Data Binding
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

        private Person _SelectedContact = null;
        public Person SelectedContact
        {
            get
            {
                return _SelectedContact;
            }

            set
            {
                _SelectedContact = value;
                bool isSelected = value != null;

                if(IsContactSelected != isSelected)
                    OnPropertyChanged("IsContactSelected");
                IsContactSelected = isSelected;
            }
        }
        #endregion

        #region SERVICES
        public void Load()
        {
            using(var db = new DBContext(dbconn.GetConnection()))
            {
                var query = from t in db.Persons
                            orderby t.Name
                            select t;

                _contacts = new ObservableCollection<Person>(query);
            }

            OnPropertyChanged("Contacts");
        }

        public void Add()
        {
            ClientEditVM vm = new ClientEditVM();
            vm.AddMode();
            vm.ShowDialog();

            if(vm.IsComplete)
            { 
                _contacts.Add(vm.Data);
                OnPropertyChanged("Contacts");
            }
        }

        public void Edit()
        {
            ClientEditVM vm = new ClientEditVM();
            vm.Data = SelectedContact;
            vm.EditMode();
            
            vm.ShowDialog();

            if(vm.IsComplete)
            {
                OnPropertyChanged("Contacts");
            }
        }

        public void Delete()
        {
            ClientEditVM vm = new ClientEditVM();
            vm.Data = SelectedContact;
            vm.DeleteMode();
            
            vm.ShowDialog();

            if(vm.IsComplete)
            {
                _contacts.Remove(SelectedContact);
                SelectedContact = null;
                OnPropertyChanged("Contacts");
            }
        }

        public bool CanDelete()
        {
            return IsContactSelected;
        }
        #endregion


    }
}
