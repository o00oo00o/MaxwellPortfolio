using System;
using System.Data.Entity;
using System.Reflection;
using System.Windows;
using MaxExperiment.AppInfrastructure;
using MaxExperiment.Model;

namespace MaxExperiment.Module.Client
{
    public class ClientEditVM : BaseAppEditorVM
    {
        public Person Data { get; set; }

        private Person OriginalData = null;

        public ClientEditVM()
            : base(new ClientEditView())
        {
        }

        protected override void OnModeSet()
        {
            switch(mode)
            {
                case EditType.Add:
                    Data = new Person();
                    Data.CreatedDate = DateTime.Now;
                    OnPropertyChanged("Data");
                    break;

                case EditType.Edit:
                    OriginalData = Data.Clone();
                    OnPropertyChanged("Data");
                    break;

                case EditType.Delete:
                    OnPropertyChanged("Data");
                    break;
            }
        }

        public override void AddHandler()
        {
            Data.ModelValidation();
            IsComplete = !Data.HasErrors;

            if(IsComplete)
            {
                using(var db = new DBContext(dbconn.GetConnection()))
                {
                    db.Persons.Add(Data);
                    IsComplete = db.SaveChanges() == 1;
                }
            }
        }

        public override void EditHandler()
        {
            Data.ModelValidation();
            IsComplete = !Data.HasErrors;

            if(IsComplete)
            {
                using(var db = new DBContext(dbconn.GetConnection()))
                {
                    db.Persons.Attach(Data);

                    PropertyInfo[] properties = typeof(Person).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
                    foreach(PropertyInfo property in properties)
                    {
                        if(property.GetValue(Data) != property.GetValue(OriginalData))
                            db.Entry(Data).Property(property.Name).IsModified = true;
                    }

                    IsComplete = db.SaveChanges() == 1;
                }
            }
        }

        public override void DeleteHandler()
        {
            using(var db = new DBContext(dbconn.GetConnection()))
            {
                db.Entry(Data).State = EntityState.Deleted;
                IsComplete = db.SaveChanges() == 1;
            }
        }
    }
}
