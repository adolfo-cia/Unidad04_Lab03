using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ejercicio5
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dtEmpresas = new DataTable();
            dtEmpresas.Columns.Add("CustomerID", typeof(string));
            dtEmpresas.Columns.Add("CompanyName", typeof(string));

            SqlConnection myconn = new SqlConnection();
            myconn.ConnectionString = "Data Source=LOCALHOST;Initial Catalog=Northwind;User ID=sa;Pwd=algy"; //;User ID=identificacion_ de_ la_ DB;Pwd=pass_d_la_DB

            //Creamos un adaptador del tipo SQLDataAdapter y le indicamos al command text que utilizara para realizar la consulta y el objeto SQLConnection que utiliza
            SqlDataAdapter myadap = new SqlDataAdapter("SELECT CustomerID, CompanyName FROM Customers", myconn);

            //Abro la conexion
            myconn.Open();
            //Cargo el contenido del result set obtenido de la base de datos en el objeto datatable
            myadap.Fill(dtEmpresas);
            //cierro la conexion
            myconn.Close();


            //recorro la lista de empresas obtenidas y lo muestro en consola
            Console.WriteLine("Listado de empresas: ");
            foreach (DataRow rowEmpresa in dtEmpresas.Rows)
            {
                string idempresa = rowEmpresa["CustomerID"].ToString();
                string nombreempresa = rowEmpresa["CompanyName"].ToString();
                Console.WriteLine(idempresa + " - " + nombreempresa);
            }
            Console.ReadLine();




        }
    }
}
