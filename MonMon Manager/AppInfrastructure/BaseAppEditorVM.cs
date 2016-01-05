using System;
using System.Windows;
using System.Windows.Input;
using MaxExperiment.Util;

namespace MaxExperiment.AppInfrastructure
{
    /// <summary>
    /// Base VM for Editor type, used to perform a single row execution on database.
    /// </summary>
    public abstract class BaseAppEditorVM : BaseAppVM
    {
        /// <summary>
        /// The Window for this VM.
        /// </summary>
        protected Window view;
        protected EditType mode;

        public string OKButtonLabel { get; set; }

        public ICommand OKButtonCommand { get; set; }
        public ICommand CancelButtonCommand { get; set; }

        /// <summary>
        /// Status of window.
        /// true if user press ok button and commit to database.
        /// </summary>
        public bool IsComplete;

        protected BaseAppEditorVM(Window w)
        {
            view = w;
            view.DataContext = this;
            view.Owner = AppData.MainWindowView;

            mode = EditType.NotSet;

            OKButtonCommand = new RelayCommand(OKAction);
            CancelButtonCommand = new RelayCommand(CancelAction);
        }

        /// <summary>
        /// Show Dialog Window.
        /// EditType property must be set before hand.
        /// </summary>
        public void ShowDialog()
        {
            if(mode == EditType.NotSet)
                throw new Exception("You must call setting Mode");

            view.ShowDialog();
        }

        #region SettingMode Methods, BEFORE SHOW DIALOG
        /// <summary>
        /// Set Editor to Add Mode. This method must be called before show dialog.
        /// </summary>
        public void AddMode()
        {
            OKButtonLabel = "_Save";
            OnPropertyChanged("OKButtonLabel");
            IsComplete = false;
            mode = EditType.Add;

            OnModeSet();
        }

        /// <summary>
        /// Set Editor to Edit Mode. This method must be called before show dialog.
        /// </summary>
        public void EditMode()
        {
            OKButtonLabel = "_Save";
            OnPropertyChanged("OKButtonLabel");
            IsComplete = false;
            mode = EditType.Edit;

            OnModeSet();
        }

        /// <summary>
        /// Set Editor to Delete Mode. This method must be called before show dialog.
        /// </summary>
        public void DeleteMode()
        {
            OKButtonLabel = "_Delete";
            OnPropertyChanged("OKButtonLabel");
            IsComplete = false;
            mode = EditType.Delete;

            OnModeSet();
        }

        /// <summary>
        /// Method for Post Processing, will be called by Setting Mode of the editor.
        /// This is usually used for displaying data to the UI before being showed.
        /// </summary>
        protected abstract void OnModeSet();
        #endregion

        /// <summary>
        /// Handler for OK Button.
        /// </summary>
        public virtual void OKAction()
        {
            switch(mode)
            {
                case EditType.Add: AddHandler(); break;

                case EditType.Edit: EditHandler(); break;

                case EditType.Delete: DeleteHandler(); break;
            }

            if(IsComplete)
                view.Close();
        }

        /// <summary>
        /// Handler for Cancel Button.
        /// </summary>
        public virtual void CancelAction()
        {
            IsComplete = false;
            view.Close();
        }

        public abstract void AddHandler();

        public abstract void EditHandler();

        public abstract void DeleteHandler();
    }
}
