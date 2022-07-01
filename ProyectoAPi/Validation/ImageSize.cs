using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.Validation
{
    public class ImageSize:ValidationAttribute
    {
        private readonly int maxMB;

        public ImageSize(int maxMB)
        {
            this.maxMB = maxMB;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
           if(value == null)
            {
                return ValidationResult.Success;
            }
           IFormFile formFile = value as IFormFile;
            if(formFile==null)
            {
                return ValidationResult.Success;
            }
            if(formFile.Length> maxMB *1024 *1024)
            {
                return new ValidationResult($"El peso maximo no debe ser a {maxMB}mb");
            }
            return ValidationResult.Success;
        }
    }
}
