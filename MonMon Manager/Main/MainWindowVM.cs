using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MaxExperiment.AppInfrastructure;
using MaxExperiment.Util;

namespace MaxExperiment.Main
{
    public class MainWindowVM : BaseVM
    {
        public ICommand     ExitCommand    { get; set; }
        public Key          ExitCommandKey { get; set; }
        public ModifierKeys ExitCommandMod { get; set; }
        public string       ExitCommandLbl { get; set; }

        public ICommand     AboutCommand    { get; set; }

        private ObservableCollection<NavigatorVMBase> _ContentVM = new ObservableCollection<NavigatorVMBase>();
        public ObservableCollection<NavigatorVMBase> ContentVM
        {
            get { return _ContentVM; }
        }

        public MainWindowVM()
        {
            bool status = false;
            string db_file = "db_file.sqlite";

            //Init and Test Database Connection
            IDBConnectorEF dbconn = new SQLiteConnector(db_file);
            status = dbconn.Open();
            if(status)
                dbconn.Close();

            if(status)
            {
                //Assign to application wide shared variable.
                AppData.DBConn = dbconn;
                
                //Add ViewModel to the Content list
                _ContentVM.Add(new Module.Client.ClientManagerVM());
                _ContentVM.Add(new Module.Service.ClientManagerVM());
                _ContentVM.Add(new Module.Other.OtherManagerVM());

                //Init Menu Bar Commands.
                InitializeCommands();
            }
        }




        #region SERVICES
        /// <summary>
        /// Initialize all Menu Bar Commands.
        /// </summary>
        private void InitializeCommands()
        {
            ExitCommand = new RelayCommand(ExitApplication);
            ExitCommandKey = Key.C;
            ExitCommandMod = ModifierKeys.Control;
            ExitCommandLbl = "Ctrl+C";

            AboutCommand = new RelayCommand(ShowAboutView);
        }

        /// <summary>
        /// Exit Application.
        /// </summary>
        private void ExitApplication()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Display About Dialog Window.
        /// </summary>
        private void ShowAboutView()
        {
            AboutView view = new AboutView();
            view.ShowDialog();
        }
        #endregion
    }
}
