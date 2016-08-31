using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ejercicio4
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
            //creo un objeto sqlcommand
            SqlCommand mycomando = new SqlCommand();
            //le indico la cadena TSQL que utilizara
            mycomando.CommandText = "SELECT CustomerID, CompanyName FROM Customers";
            //indico el objeto connection que utiliziara
            mycomando.Connection = myconn;

            //creo un adaptador del tipo SQLDataAdapter y le indico el commandtext que utilizara para realizar la consulta
            //y el objeto sqlconnection que utiliza
            SqlDataAdapter myadap = new SqlDataAdapter("SELECT CustomerID, CompanyName FROM Customers", myconn);
            //SqlDataAdapter myadap2 = new SqlDataAdapter(mycomando);

            //abro la coneccion
            myconn.Open();
            //creo un objeto DataReader y ejecuto el metodo ExecuteReader dl objeto mycomando
            SqlDataReader mydr = mycomando.ExecuteReader();
            //cargo los datos en el datatable utilizando el objeto DataReader
            dtEmpresas.Load(mydr);
            //cierro la coneccion
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
