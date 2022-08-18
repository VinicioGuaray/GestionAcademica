using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionAcademica.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ModelCurso> _post;
        private ObservableCollection<ModelRol> _postRol;
        string urlRol = ModelServidor.url + "/postRol.php";

        public Perfil()
        {
            InitializeComponent();
            int id = ModelPerfil.Usuario_Id;
            nombres.Text = ModelPerfil.Nombre;
            apellidos.Text = ModelPerfil.Apellido;
            cedula.Text = ModelPerfil.cedula;
            telefono.Text = ModelPerfil.telefono;
            direccion.Text = ModelPerfil.direccion;
           // fechanacimiento.Text = ModelPerfil.fechaNaci;
            correo.Text = ModelPerfil.correo;
            getRoles();
            

           // fechaNaci.DateSelected += ModelPerfil.fechaNaci;
        }

        private async void getRoles()
        {


            var content = await client.GetStringAsync(urlRol);
            List<ModelRol> post = JsonConvert.DeserializeObject<List<ModelRol>>(content);
            _postRol = new ObservableCollection<ModelRol>(post);

            PickerRol.Items.Add("Seleccionar Rol");
            foreach (var item in _postRol)
            {
                PickerRol.Items.Add(item.Tipo.ToString());
            }
            if (ModelPerfil.rol == 1)
            {
                PickerRol.IsVisible = true;
                PickerRol.SelectedIndex = 1;
            }
            else if (ModelPerfil.rol == 2)
            {
                PickerRol.SelectedIndex = 2;
            }
            else if (ModelPerfil.rol == 3)
            {
                PickerRol.SelectedIndex = 3;
            }

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

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }

        private void sw_progress_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

        }
    }
}