using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionAcademica.Model;
using System.Text;
using System.Net.Http.Headers;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Login : ContentPage
    {       

       //string url = ModelServidor.url+"/GestionAcademico/postLogin.php";
        string url = ModelServidor.url + "/postLogin.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ModelPerfil> _postUsuarios;

        public object Constants { get; private set; }
        public Login()
        {
            InitializeComponent();


            
        }
        
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }


        private async void btnIngresar_Clicked(object sender, EventArgs e)

        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar su usuario", "ok");
                    usuario.Focus();
                }
                else if (string.IsNullOrEmpty(password.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar su Clave", "ok");
                    password.Focus();
                }
                else
                {

                    Model.ModelUsuario datos = new Model.ModelUsuario
                    {
                        correo = usuario.Text,
                        password = password.Text
                    };

                   
                    Uri request = new Uri(url);
                    var Client = new HttpClient();
                    var json = JsonConvert.SerializeObject(datos); 
                    var contenJson = new StringContent(json, Encoding.UTF8, "application/json");                   
                   var response = await Client.PostAsync(request,contenJson);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var contenido = await response.Content.ReadAsStringAsync();

                        Model.ModelUsuario result = new Model.ModelUsuario();
                        //convertimos los datos a json
                        result = JsonConvert.DeserializeObject<Model.ModelUsuario>(contenido);

                        if (result.success == "true")
                        {
                            ModelPerfil.Usuario_Id = result.usuario_Id;
                            ModelPerfil.Nombre = result.nombre;
                            ModelPerfil.Apellido = result.apellido;
                            ModelPerfil.cedula = result.cedula;
                            ModelPerfil.correo = usuario.Text;
                            ModelPerfil.telefono = result.telefono;
                            ModelPerfil.direccion = result.direccion;
                            ModelPerfil.fechaNaci = result.fechaNaci;
                            ModelPerfil.rol = result.idRol;
                            ModelPerfil.IdCurso = result.idCurso;
                            perfil(result.usuario_Id);
                            await Navigation.PushAsync(new Menu());
                        }
                        else
                        {
                            await DisplayAlert("Error", "Usuario o Clave incorrectos", "ok");

                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Error en la aplicación \n comunicarse con el admin", "ok");
                    }

                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("ok", ex.Message, "ok");

            }
            
        }


        private async void btnRecuperar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Recuperar());
        }

        private async void perfil(int Usuario_Id)
        {
            string urlUsuario = ModelServidor.url+"/postPerfil.php?Usuario_Id=" + Usuario_Id;
                var content = await client.GetStringAsync(urlUsuario);
                List<ModelPerfil> postUsuario = JsonConvert.DeserializeObject<List<ModelPerfil>>(content);
                _postUsuarios = new ObservableCollection<ModelPerfil>(postUsuario);
               
            
        }
    }
}
