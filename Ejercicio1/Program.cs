using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;    

namespace Ejercicio1
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable dtAlumnos = new DataTable();
            DataRow rwAlumnos = dtAlumnos.NewRow();
            DataColumn colNombre = new DataColumn("Nombre", typeof(string) );
            DataColumn colApellido = new DataColumn("Apellido", typeof(string));

            dtAlumnos.Columns.Add(colApellido);
            dtAlumnos.Columns.Add(colNombre);
            rwAlumnos[colNombre] = "adolfo";
            rwAlumnos[colApellido] = "cia";
            
            dtAlumnos.Rows.Add(rwAlumnos);

            DataRow rwAlumnos2 = dtAlumnos.NewRow();
            rwAlumnos2["Nombre"] = "esteban";
            rwAlumnos2["Apellido"] = "suarez garcia";
            dtAlumnos.Rows.Add(rwAlumnos2);

            Console.WriteLine("Listado de alumnos:");
            foreach ( DataRow row in dtAlumnos.Rows)
            {
                Console.WriteLine(row[colApellido] + ", " + row[colNombre]);
            }
            Console.ReadLine();

        }
    }
}
