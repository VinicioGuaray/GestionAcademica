using GestionAcademica.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Gamificacion : ContentPage
    {
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ModelCurso> _post;
        private ObservableCollection<ReporteAsistencia> _postUsuarios;
        string urlGamificacion = ModelServidor.url+"/postGamificación.php";
        string urlCurso = ModelServidor.url+"/postCurso.php";

        //datos de los estudiantes
        public IList<ModelUsuario> GridUsuario { get; set; }
        //variable para guardar el id del curso  y paralelo. 
        int indexCurso;
        int indexParalelo;
        public Gamificacion()
        {
            InitializeComponent();
            getCursos();
        }

        private async void getCursos()
        {


            var content = await client.GetStringAsync(urlCurso);
            List<ModelCurso> post = JsonConvert.DeserializeObject<List<ModelCurso>>(content);
            _post = new ObservableCollection<ModelCurso>(post);
            PickerGamificacion.Items.Add("--Seleccionar--");
            foreach (var item in _post)
            {
                PickerGamificacion.Items.Add(item.Curso.ToString() + " " + item.Paralelo.ToString());
                PickerGamificacion.SelectedIndex = 0;
            }

        }
        private async void getUsuarios(int idCurso)
        {
            string urlUsuario = ModelServidor.url+"/postReporte.php?Curso_Id=" + idCurso;
            var content = await client.GetStringAsync(urlUsuario);
            List<ReporteAsistencia> postUsuario = JsonConvert.DeserializeObject<List<ReporteAsistencia>>(content);
            _postUsuarios = new ObservableCollection<ReporteAsistencia>(postUsuario);
            MyListView.ItemsSource = _postUsuarios;
        }

        private void PickerGamificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexCurso = PickerGamificacion.SelectedIndex;
            if (indexCurso != 0)
            {
                btnImprimir.IsEnabled = true;
                getUsuarios(indexCurso);
            }
            else
            {
                btnImprimir.IsEnabled = false;
            }
        }

        public class ReporteAsistencia
        {
            public int asistencia_Id { get; set; }

            public string asistencia { get; set; }

            public string fecha { get; set; }

            public int curso_Id { get; set; }

            public int usuario_Id { get; set; }

            public string nombres { get; set; }

            public int puntuacion { get; set; }

        }

        private void btnImprimir_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("info", "No disponible", "ok");
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }
    }
}