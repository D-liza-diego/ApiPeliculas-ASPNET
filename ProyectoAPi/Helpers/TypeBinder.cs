using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ProyectoAPi.Helpers
{
    public class TypeBinder<t> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var proveedorPropiedad= bindingContext.ValueProvider.GetValue(nombrePropiedad);
            if(proveedorPropiedad==ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var valorDeserializado = JsonConvert.DeserializeObject<t>(proveedorPropiedad.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad, "Valor invalido para tipo de List<int>");
            }
            return Task.CompletedTask;
        }
    }
}
