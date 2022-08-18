using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Net.Http;
using GestionAcademica.Model;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.ComponentModel;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Asistencia : ContentPage
    {
        string urlAsistencia = ModelServidor.url+"/postAsistencia.php";

        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ModelCurso> _post;
        private ObservableCollection<ModelUsuario> _postUsuarios;
        string urlCurso = ModelServidor.url+"/postCurso.php";

        //datos de los estudiantes
        public IList<ModelUsuario> GridUsuario { get; set; }
        //variable para guardar el id del curso  y paralelo. 
        int indexCurso;
      
        public Asistencia()
        {
            InitializeComponent();
            getCursos();
            if (ModelPerfil.rol == 1)
            {
                btnNuevo.IsVisible = true;
            }

        }
        //metodo para obtener los cursos
        private async void getCursos()
        {
             

            var content = await client.GetStringAsync(urlCurso);
            List<ModelCurso> post = JsonConvert.DeserializeObject<List<ModelCurso>>(content);
            _post = new ObservableCollection<ModelCurso>(post);
            PickerAsistencia.Items.Add("--Seleccionar--");
            foreach (var item in _post)
            {
               
                    PickerAsistencia.Items.Add(item.Curso.ToString() + " " + item.Paralelo.ToString());
                    PickerAsistencia.SelectedIndex = 0;
               
            }

        }
            
        //obtenemos los usuarios por curso
        private async void getUsuarios(int idCurso) 
        {
            string urlUsuario = ModelServidor.url+"/postUsuario.php?Curso_Id=" + idCurso;
            var content = await client.GetStringAsync(urlUsuario);
            List<ModelUsuario> postUsuario = JsonConvert.DeserializeObject<List<ModelUsuario>>(content);
            _postUsuarios = new ObservableCollection<ModelUsuario>(postUsuario);
            MyListView.ItemsSource = _postUsuarios;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
           
            _postUsuarios = new ObservableCollection<ModelUsuario>((IEnumerable<ModelUsuario>)MyListView.ItemsSource);
            Model.ModelAsistencia datos = new Model.ModelAsistencia
            {
               

            }; 
            //Array Asistencia = new Array();
           

            Uri request = new Uri(urlAsistencia);
            var Client = new HttpClient();
            var json = JsonConvert.SerializeObject(_postUsuarios);
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

                    await DisplayAlert("Registrado", "Se guardo correctamente", "ok");
                    await Navigation.PushAsync(new Menu());
                   
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

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }

        private void PickerAsistencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexCurso = PickerAsistencia.SelectedIndex;
            if (indexCurso != 0)
            {
                btnGuardar.IsEnabled = true;
                getUsuarios(indexCurso);
            }
            else
            {
                btnGuardar.IsEnabled = false;
            }
        }

        private void PickerCurso_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void datos_Clicked(object sender, EventArgs e)
        {

        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           
           bool es= e.Value;
            //var chec= (checked)sender;
           // _postUsuarios = new ObservableCollection<ModelUsuario>((IEnumerable<ModelUsuario>)MyListView.ItemsSource);
            
            var check = (CheckBox)sender;
            check.BindingContext =  new ModelUsuario() { status="No" };
          
            if (es != true)
            {
                var item = _postUsuarios.FirstOrDefault(i => i.status == "Si"); 
                if (item != null)
                {
                    item.status = "No";
                    
                }
               }
            else
            {
                int i = 0;
            }
                    

        }

        private void CheckBox_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        public  void clickNews(object sender, SelectedItemChangedEventArgs args)
        {
            var NewsItem = args.SelectedItem;
            var n = args.SelectedItemIndex;

        }

        private void btnNuevo_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}