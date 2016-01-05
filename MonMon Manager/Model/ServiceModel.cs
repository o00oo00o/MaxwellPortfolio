using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace MaxExperiment.Model
{
    public class ServiceModel : BaseModel<ServiceModel>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int ServiceGroupID { get; set; }

        public string Name { get; set; }

        public double Cost { get; set; }

        public int Duration { get; set; }

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
        }

        /// <summary>
        /// Provide deep copy of the model.
        /// </summary>
        /// <returns>Deep Copy of Object.</returns>
        public override ServiceModel Clone()
        {
            return new ServiceModel
            {
                ID = this.ID,
                ServiceGroupID = this.ServiceGroupID,
                Name = this.Name,
                Duration = this.Duration,
                Cost = this.Cost
            };
        }
    }
}
