using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GestionAcademica.Model
{
    public class ModelUsuario: INotifyPropertyChanged
    {
        public int usuario_Id { get; set; }
        public string nombre { get; set; }

        public string apellido { get; set; }

        public string correo { get; set; }

        public string fechaNaci { get; set; } 

        public string cedula { get; set; }

        public string telefono { get; set; }

        public string direccion { get; set; }

        public int activo { get; set; }

        public string password { get; set; }

        public int idRol { get; set; }

        public int idCurso { get; set; }

        public string success { get; set; }

        public string status { get; set; } = "Si";

        public event PropertyChangedEventHandler PropertyChanged;


        public static implicit operator ModelUsuario(view.Login v)
        {
            throw new NotImplementedException();
        }
    }
}
