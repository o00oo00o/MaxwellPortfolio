using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MaxExperiment.Model
{
    /// <summary>
    /// Base class of Model (MVVM).
    /// Provide Implementation of INotifyDataErrorInfor for Data Validation.
    /// </summary>
    public abstract class BaseModel<T> : INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected void OnPropertyErrorsChanged(string p)
        {
            if(ErrorsChanged != null)
                ErrorsChanged.Invoke(this, new DataErrorsChangedEventArgs(p));
        }

        /// <summary>
        /// Dictionary to store list of error for each property.
        /// </summary>
        protected Dictionary<string, List<string>> ErrorDictionary = new Dictionary<string, List<string>>();

        /// <summary>
        /// Return list of error for given property name.
        /// </summary>
        /// <param name="propertyName">Property Name.</param>
        /// <returns>list of error if exist, null otherwise.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errors = null;

            if(propertyName != null)
            {
                errors = new List<string>();
                ErrorDictionary.TryGetValue(propertyName, out errors);
            }

            return errors;
        }

        /// <summary>
        /// Return true if there are errors, false otherwise.
        /// </summary>
        public bool HasErrors
        {
            get
            {
                bool result = false;

                try
                {
                    var count = ErrorDictionary.Values.FirstOrDefault(r => r.Count > 0);
                    result = count != null;
                }
                catch
                {
                }

                return result;
            }
        }

        /// <summary>
        /// Provide a Deep Copy Clone.
        /// </summary>
        /// <returns>Deep Copy Clone.</returns>
        public abstract T Clone();
    }
}
