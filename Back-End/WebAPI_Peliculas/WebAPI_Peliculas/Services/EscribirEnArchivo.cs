using Microsoft.AspNetCore.Mvc;
using WebAPI_Peliculas.Controllers;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas.Services
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly IWebHostEnvironment env;
        
        private readonly string nombreArchivo = "Peliculas.txt";
        private Timer timer;

        public EscribirEnArchivo(IWebHostEnvironment env)
        {
            this.env = env;
           
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Se ejecuta cuando cargamos la aplicacion 1 vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando detenemos la aplicacion aunque puede que no se ejecute por algun error. 
            timer.Dispose();
            Escribir("Proceso Finalizado");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
             Escribir("Proceso en ejecucion: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }
        private void Escribir(string msg)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";

            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(msg); }
        }

        private void GuardarPeliculas()
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            
            //ActionResult task = DirectoresController.
            /*var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            ActionResult task = DirectoresController.ObtenerGui
            ActionResult task = peliculasController.ObtenerGuid();
            object Pelicula = task.Result.Value;
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) {writer.WriteLine(Pelicula); }*/
        }
    }
}
