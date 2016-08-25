using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;

namespace Ejercicio2
{
    class Program
    {
        static void Main(string[] args)
        {
            // se crea la tabla Alumnos
            DataTable dtAlumnos = new DataTable();
            DataColumn colIDAlumno = new DataColumn("IDAlumno", typeof(int));
            colIDAlumno.ReadOnly= true;
            colIDAlumno.AutoIncrement = true;
            colIDAlumno.AutoIncrementSeed = 0; // el primer numero será 0
            colIDAlumno.AutoIncrementStep = 1; // se incrementara de a 1
            DataColumn colNombre = new DataColumn("Nombre", typeof(string));
            DataColumn colApellido = new DataColumn("Apellido", typeof(string));
            dtAlumnos.Columns.Add(colApellido);
            dtAlumnos.Columns.Add(colNombre);
            dtAlumnos.Columns.Add(colIDAlumno);
            dtAlumnos.PrimaryKey =  new DataColumn[] { colIDAlumno }; // se establece la primary key
            DataRow rwAlumnos = dtAlumnos.NewRow();
            rwAlumnos[colNombre] = "adolfo";
            rwAlumnos[colApellido] = "cia";
            dtAlumnos.Rows.Add(rwAlumnos);
            DataRow rwAlumnos2 = dtAlumnos.NewRow();
            rwAlumnos2["Nombre"] = "esteban";
            rwAlumnos2["Apellido"] = "suarez garcia";
            dtAlumnos.Rows.Add(rwAlumnos2);
            
            // se crea la tabla Cursos
            DataTable dtCursos = new DataTable("Cursos");
            DataColumn colIDCurso = new DataColumn("IDCurso", typeof(int));
            colIDCurso.ReadOnly = true;
            colIDCurso.AutoIncrement = true;
            colIDCurso.AutoIncrementSeed = 1;
            colIDCurso.AutoIncrementStep = 1;
            DataColumn colCurso = new DataColumn("Curso", typeof(string));
            dtCursos.Columns.Add(colIDCurso);
            dtCursos.Columns.Add(colCurso);
            dtCursos.PrimaryKey = new DataColumn[] { colIDCurso };
            DataRow rwCurso = dtCursos.NewRow();
            rwCurso[colCurso] = "Informatica";
            dtCursos.Rows.Add(rwCurso);

            // se crea el DataSet o contendor, Universidades
            DataSet dsUniversidad = new DataSet();
            dsUniversidad.Tables.Add(dtAlumnos);
            dsUniversidad.Tables.Add(dtCursos);

            // se crea las tablas de la relacion, ya q la relacion es N a N
            DataTable dtAlumnos_Cursos = new DataTable("Alumnos_Cursos");
            DataColumn col_ac_IDAlumno = new DataColumn("IDAlumno", typeof(int));
            DataColumn col_ac_IDCurso = new DataColumn("IDCurso", typeof(int));
            dtAlumnos_Cursos.Columns.Add(col_ac_IDAlumno);
            dtAlumnos_Cursos.Columns.Add(col_ac_IDCurso);
            dsUniversidad.Tables.Add(dtAlumnos_Cursos);

            // se crean las relaciones y se agregan al dataset
            DataRelation relAlumno_ac = new DataRelation("Alumno_Cursos", colIDAlumno, col_ac_IDAlumno);
            DataRelation relCurso_ac = new DataRelation("Cursos_Alumnos", colIDCurso, col_ac_IDCurso);
            dsUniversidad.Relations.Add(relAlumno_ac);
            dsUniversidad.Relations.Add(relCurso_ac);

            //se agregan registros a la tabla de relaciones
            DataRow rwAlumnosCursos = dtAlumnos_Cursos.NewRow();
            rwAlumnosCursos[col_ac_IDAlumno] = 0; //alumno: adolfo cia
            rwAlumnosCursos[col_ac_IDCurso] = 1; //curso: informatica
            dtAlumnos_Cursos.Rows.Add(rwAlumnosCursos);

            rwAlumnosCursos = dtAlumnos_Cursos.NewRow(); //la nueva row generada se le asigna a la variable ya usada, pisando el contenido
            rwAlumnosCursos[col_ac_IDAlumno] = 1; //alumno: esteban suarez garcia
            rwAlumnosCursos[col_ac_IDCurso] = 1; //curso: informatica
            dtAlumnos_Cursos.Rows.Add(rwAlumnosCursos);

            //**************************
            Console.Write("ingrese el nombre del curso: ");
            string materia = Console.ReadLine();
            Console.WriteLine("Listado de alumnos del curso " + materia);
            DataRow[] row_CursoInf = dtCursos.Select("Curso = '" + materia + "'");
            foreach (DataRow rowCu in row_CursoInf)
            {
                DataRow[] row_AlumnosInf = rowCu.GetChildRows(relCurso_ac);
                foreach (DataRow rowAl in row_AlumnosInf)
                {
                    Console.WriteLine(rowAl.GetParentRow(relAlumno_ac)[colApellido]
                        + ", " +
                        rowAl.GetParentRow(relAlumno_ac)[colNombre]    
                    );

                }
            }
            Console.ReadLine();




        }
    }
}
