using System;
using System.Collections.Generic;
using System.Text;

namespace GestionAcademica.Model
{
   public class ModelAsistencia
    {
        public int asistencia_Id { get; set; }

        public string asistencia { get; set; }

        public string observacion { get; set; }
        
        public string fecha { get; set; }

        public string hora { get; set; }

        public int curso_Id { get; set; }

        public int usuario_Id { get; set; }
        public string status { get; set; } = "si";

    }
}
