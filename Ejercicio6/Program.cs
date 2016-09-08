using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Ejercicio6
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

            //Primero indico el CustomerID que deseo modificar
            Console.Write("Escriba el CustomerID que desea modificar: ");
            string custid = Console.ReadLine();

            //luego me traigo una coleccion de datarows que contenga ese CustomerID
            DataRow[] rwempresas = dtEmpresas.Select("CustomerID = '" + custid + "'");
            if (rwempresas.Length != 1) //si no encuentro nada entonces salgo
            {
                Console.WriteLine("CustomerID no encontrado");
                Console.ReadLine();
                return; //se finaliza la ejecucion dl programa
            }

            //me traigo el primer datarow de la coleccion
            DataRow rowMiEmpresa = rwempresas[0];
            string nombreactual = rowMiEmpresa["CompanyName"].ToString();
            //muestro en consola el nombre del CustomerID seleccionado
            Console.WriteLine("Nombre actual de la empresa: " + nombreactual );
            //solicito q escriba un nuevo nombre
            Console.Write("escriba nuevo nombre para esa emrpesa: ");
            string nuevonombre = Console.ReadLine();

            //llamamos al metodo BeginEdit del datarow para iniciar los cambios
            rowMiEmpresa.BeginEdit();
            //modifico el valor del campo CompanyName
            rowMiEmpresa["CompanyName"] = nuevonombre;
            //finalizo la edicion llamando al metodo EndEdit
            rowMiEmpresa.EndEdit();

            //ahora creo un objeto command que utilizare para guardar los cambios en la base de datos
            SqlCommand updcommand = new SqlCommand();
            //le indico la conexion
            updcommand.Connection = myconn;
            //le indico la cadena TSQL
            updcommand.CommandText = "UPDATE Customers SET CompanyName = @CompanyNAme WHERE CustomerID = @CustomerID";
            //indico los parametros que estoy utilizando.
            //como asi tambien el tipo de dato, la longitud del dato y el nombre del campo del datatable
            updcommand.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 50, "CompanyName");
            updcommand.Parameters.Add("@CustomerID", SqlDbType.NVarChar, 5, "CustomerID");

            //ahora adjunto el objeto updcommand al dataadapter
            myadap.UpdateCommand = updcommand;
            //por ultimo llamamos al metodo Update del dataadapter
            myadap.Update(dtEmpresas);
            


            Console.ReadLine();


        }
    }
}
