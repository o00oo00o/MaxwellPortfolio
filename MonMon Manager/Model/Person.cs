using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MaxExperiment.Util;

namespace MaxExperiment.Model
{
    public class Person : BaseModel<Person>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Perform Model Validation.
        /// </summary>
        private void Validate()
        {
            Task.Run(() => ModelValidation());
        }

        public void ModelValidation()
        {
            List<string> errors;

            //Validate Name
            errors = new List<string>();
            if(string.IsNullOrEmpty(Name))
                errors.Add("Name cannot be empty.");
            ErrorDictionary["Name"] = errors;

            if(errors.Count > 0)
                OnPropertyErrorsChanged("Name");

            //Validate Email
            errors = new List<string>();
            if(!string.IsNullOrEmpty(Email) && !FormatValidator.IsValidEmail(Email))
                errors.Add("Email is not in the correct format.");
            ErrorDictionary["Email"] = errors;

            if(errors.Count > 0)
                OnPropertyErrorsChanged("Email");
        }

        /// <summary>
        /// Provide deep copy of the model.
        /// </summary>
        /// <returns>Deep Copy of Object.</returns>
        public override Person Clone()
        {
            return new Person
                {
                    ID = this.ID,
                    Name = this.Name,
                    Mobile = this.Mobile,
                    Email = this.Email
                };
        }
    }
}
