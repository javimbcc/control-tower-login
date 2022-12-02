﻿using DAL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;

namespace ControlTower.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public DlkCatAccEmpleado empleado { get; set; }


        // https://stackoverflow.com/questions/66711696/razor-pages-form-is-not-hitting-the-post-method
        [HttpPost]
        [ActionName("MiLogin")]
        public void OnPostSubmit(DlkCatAccEmpleado empleado)
        {
            //Recogemos la información de la vista
            //Hacemos la conexion
            var connection = new NpgsqlConnection("Host=localhost;Port=5432;Pooling=true;Database=CSPharma;UserId=postgres;Password=root;");
            Console.WriteLine("ABRIENDO CONEXION");
            connection.Open();

            NpgsqlCommand consulta = new NpgsqlCommand($"SELECT * FROM \"dlk_informacional\".\"dlk_cat_acc_empleados\" WHERE cod_empleado='{empleado.CodEmpleado}' AND clave_empleado='{empleado.ClaveEmpleado}'", connection);
            NpgsqlDataReader resultadoConsulta = consulta.ExecuteReader();


            if (resultadoConsulta.HasRows)
            {
                Console.WriteLine("Ha iniciado sesion con exito");
            }
            else
            {
                Console.WriteLine("Recuerde sus credenciales");
            }

            Console.WriteLine("Cerrando conexion");
            connection.Close();
        }

    }
}
