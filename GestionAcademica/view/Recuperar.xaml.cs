using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GestionAcademica.Model;
using System.Net;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Recuperar : ContentPage
    {
        public Recuperar()
        {
            InitializeComponent();
        }

        private async void btnIngresar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Model.ModelUsuario datos = new Model.ModelUsuario
                {
                    correo = correo.Text,
                    password=GetRandomPassword()
                    
                };
                Uri request = new Uri(ModelServidor.url + "/postRecuperar.php");
                var Client = new HttpClient();
                var json = JsonConvert.SerializeObject(datos);
                var contenJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(request, contenJson);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var contenido = await response.Content.ReadAsStringAsync();

                    Model.ModelUsuario result = new Model.ModelUsuario();
                    //convertimos los datos a json
                    result = JsonConvert.DeserializeObject<Model.ModelUsuario>(contenido);

                    if (result.success == "true")
                    {
                        await DisplayAlert("Info", "Se Envio a su correo \n la nueva Clave", "ok");
                    }
                    else
                    {
                        await DisplayAlert("Info", "Error en el sistema, comunicarse con el admin", "ok");

                    }

                }
                else
                {
                    await DisplayAlert("Error", "Error en el sistema, comunicarse con el admin", "ok");

                }

            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Error" + ex, "ok");
            }
        }
        public static string GetRandomPassword(int length = 4)
        {
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = rnd.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
    }
}