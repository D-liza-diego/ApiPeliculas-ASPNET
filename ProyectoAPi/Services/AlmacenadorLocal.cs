namespace ProyectoAPi.Services
{
    public class AlmacenadorLocal : IAlmacenarArchivos
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor http;

        public AlmacenadorLocal(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.env = webHostEnvironment;
            this.http = httpContextAccessor;
        }
        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await EliminarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido, extension, contenedor, contentType);
        }

        public Task EliminarArchivo(string ruta, string contenedor)
        {
            if(ruta!=null)
            {
                var nombreArchivo = Path.GetFileName(ruta);
                string directorioArchivo = Path.Combine(env.WebRootPath, contenedor, nombreArchivo);
                if (File.Exists(directorioArchivo))
                {
                    File.Delete(directorioArchivo);
                }
            }
            return Task.FromResult(0);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
           var nombreArchivo = $"{ Guid.NewGuid()}{extension}";
           string folder = Path.Combine(env.WebRootPath, contenedor);
           if(!Directory.Exists(folder))
            {   
                Directory.CreateDirectory(folder);
            }
            string ruta = Path.Combine(folder, nombreArchivo);
            await File.WriteAllBytesAsync(ruta, contenido);
            var ActualUrl= $"{http.HttpContext.Request.Scheme}://{http.HttpContext.Request.Host}";
            var BdUrl= Path.Combine(ActualUrl, contenedor, nombreArchivo).Replace("\\", "/");
            return BdUrl;
        }
    }
}
