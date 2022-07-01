using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.Validation
{
    public class TypeFile:ValidationAttribute
    {
        private readonly string[] tipos;

        public TypeFile(string[] tipos)
        {
            this.tipos = tipos;
        }
        public TypeFile(TipoGrupos tipoGrupos)
        {
            if(tipoGrupos==TipoGrupos.Imagen)
            {
                tipos = new string[]
                {
                    "image/jpeg",
                    "image/png",
                    "image/gif"
                };
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if(!tipos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo de archivo debe ser de los siguientes: {string.Join(", ",tipos)}");
            }
            return ValidationResult.Success;
        }
    }
}
