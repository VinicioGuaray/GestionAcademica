using GestionAcademica.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Curso : ContentPage
    {
        string url = ModelServidor.url + "/postUsuario.php";
        string urlCurso = ModelServidor.url + "/postCurso.php";
        string urlRol = ModelServidor.url + "/postRol.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ModelCurso> _post;
        private ObservableCollection<ModelRol> _postRol;
        //DateTime fecha;
        public Curso()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Cursos.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un curso", "ok");
                    Cursos.Focus();
                }
                else if (string.IsNullOrEmpty(Paralelo.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un paralelo", "ok");
                    Paralelo.Focus();
                }
                else if (string.IsNullOrEmpty(Aula.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar una Aula", "ok");
                    Aula.Focus();
                }
                else if (string.IsNullOrEmpty(Ciclo.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un Ciclo escolar", "ok");
                    Ciclo.Focus();
                }
                else
                {
                    
                    Model.ModelCurso datos = new Model.ModelCurso
                    {
                        Curso = Cursos.Text,
                        Paralelo = Paralelo.Text,
                        ciclo = Ciclo.Text,
                        Aula = Aula.Text,                       

                    };

                    Uri request = new Uri(urlCurso);
                    var Client = new HttpClient();
                    var json = JsonConvert.SerializeObject(datos);
                    var contenJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await Client.PostAsync(request, contenJson);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var contenido = await response.Content.ReadAsStringAsync();

                        Model.ModelCurso result = new Model.ModelCurso();
                        //convertimos los datos a json
                        result = JsonConvert.DeserializeObject<Model.ModelCurso>(contenido);

                        if (result.success == "true")
                        {
                            await DisplayAlert("Info", "Se guardo correctamente", "ok");
                            this.Clear();
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se puede guardar los datos", "ok");

                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se puede conectar con la base de datos", "ok");
                    }

                }
            

            } catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "ok");
            }
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Asistencia());
        }

        private void Clear()
        {
            Cursos.Text = "";
            Paralelo.Text = "";
            Aula.Text = "";
            Ciclo.Text = "";
        }
    }
}