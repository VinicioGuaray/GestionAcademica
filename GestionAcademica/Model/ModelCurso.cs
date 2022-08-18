using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Model
{
    public class ModelCurso
    {
        public int Curso_Id { get; set; }
        
        public string Curso { get; set; }

        public string Paralelo { get; set; }

        public string Aula { get; set; }

        public string ciclo { get; set; }

        public string success { get; set; }
        public static explicit operator ModelCurso(int v)
        {
            throw new NotImplementedException();
        }
    }
}
