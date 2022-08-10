using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionAcademica.Model;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            int id = ModelPerfil.Usuario_Id;
            nombres.Text = ModelPerfil.Nombre;
            apellidos.Text = ModelPerfil.Apellido;
            cedula.Text = ModelPerfil.cedula;
            telefono.Text = ModelPerfil.telefono;
            direccion.Text = ModelPerfil.direccion;
            fechanacimiento.Text = ModelPerfil.fechaNaci;
            correo.Text = ModelPerfil.correo;
           // fechaNaci.DateSelected += ModelPerfil.fechaNaci;
        }

        private void PickerRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {

        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {

        }

        private void sw_progress_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}