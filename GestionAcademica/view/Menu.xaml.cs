using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionAcademica.Model;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
            if (ModelPerfil.rol == 1)
            {
                logo.IsVisible = false;
                asistencia.IsVisible = true;
                Nuevo.IsVisible = true;
            }
        }

        private async void asistencia_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Asistencia());
        }

        private async void Gamificacion_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Gamificacion());
        }

        private async void Reporte_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Reporte());
        }

        private async void Nuevo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Usuario());
        }

        private async void btnPerfil_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Perfil());
        }

        private void btnSalir_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Info", "Salio del sistema", "ok");
            System.Diagnostics.Process.GetCurrentProcess().Kill();


        }
    }
}