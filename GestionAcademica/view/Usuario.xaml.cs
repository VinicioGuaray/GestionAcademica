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
    public partial class Usuario : ContentPage
    {
        string url = "http://"+ModelServidor.url+"/GestionAcademico/postUsuario.php";
        string urlCurso = "http://"+ModelServidor.url+"/GestionAcademico/postCurso.php";
        string urlRol = "http://"+ ModelServidor.url + "/GestionAcademico/postRol.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<ModelCurso> _post;
        private ObservableCollection<ModelRol> _postRol;
        //DateTime fecha;
        string fecha;
        //variable para el curso 
        int indexCurso;
        int indexRol;
        public Usuario()
        {
            InitializeComponent();
            getCursos();
            getRoles();
            /*var listCurso = new List<string>();
            listCurso.Add("8vo");
            listCurso.Add("9no");
            listCurso.Add("10mo");
            Picker.ItemsSource = listCurso;
            */
        }
        private async void getCursos()
        {
            

            var content = await client.GetStringAsync(urlCurso);
            List<ModelCurso> post = JsonConvert.DeserializeObject<List<ModelCurso>>(content);
            _post = new ObservableCollection<ModelCurso>(post);
           
            Picker.Items.Add("Seleccionar curso");
            foreach (var item in _post)
            {
                Picker.Items.Add(item.Curso.ToString()+' '+item.Paralelo.ToString());
                Picker.SelectedIndex = 0;
            }
           
        }
        //metodo para obtener los roles
        private async void getRoles()
        {


            var content = await client.GetStringAsync(urlRol);
            List<ModelRol> post = JsonConvert.DeserializeObject<List<ModelRol>>(content);
            _postRol = new ObservableCollection<ModelRol>(post); 

            PickerRol.Items.Add("Seleccionar Rol");
            foreach (var item in _postRol)
            {
                PickerRol.Items.Add(item.Tipo.ToString());
                PickerRol.SelectedIndex = 0;
            }

        }
        private void sw_progress_Toggled(object sender, ToggledEventArgs e)
        {

        }


        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Menu());
        }
        //metodo para generar clave aleatoria
        public static string GetRandomPassword(int length=4)
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

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
               
                if (string.IsNullOrEmpty(nombres.Text))
                {
                  await  DisplayAlert("Error", "Debe ingresar un nombre", "ok");
                    nombres.Focus();
                }
                else if (string.IsNullOrEmpty(apellidos.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un apellido", "ok");
                    apellidos.Focus();
                }
                else if (string.IsNullOrEmpty(correo.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un correo", "ok");
                    correo.Focus();
                }
                else if (string.IsNullOrEmpty(cedula.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar una cedula", "ok");
                    cedula.Focus();
                }
                else if (cedula.Text.Length < 10 || cedula.Text.Length > 10)
                {
                    await DisplayAlert("Error", "La cedula no tiene 10 digitos", "ok");
                    cedula.Focus();
                }
                else if (string.IsNullOrEmpty(telefono.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar un teléfono", "ok");
                    telefono.Focus();
                }else if (telefono.Text.Length < 10 || telefono.Text.Length > 10)
                {
                    await DisplayAlert("Error", "El teléfono no tiene 10 digitos", "ok");
                    telefono.Focus();
                }
                else if (string.IsNullOrEmpty(direccion.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar una dirección", "ok");
                    direccion.Focus();
                }
                else if (indexCurso==0)
                {
                    await DisplayAlert("Error", "Debe seleccionar un curso", "ok");
                    Picker.Focus();
                }
                
                else if (indexRol == 0)
                {
                    await DisplayAlert("Error", "Debe seleccionar un Rol", "ok");
                    PickerRol.Focus();
                }
                else if (string.IsNullOrEmpty(fechanacimiento.Text))
                {
                    await DisplayAlert("Error", "Debe ingresar una fecha", "ok");
                    fechanacimiento.Focus();
                }
                else
                {
                    string pass = GetRandomPassword();
                    Model.ModelUsuario datos = new Model.ModelUsuario
                    {
                        nombre = nombres.Text,
                        apellido = apellidos.Text,
                        correo = correo.Text,
                        cedula = cedula.Text,
                        telefono = telefono.Text,
                        direccion = direccion.Text,
                        idCurso = indexCurso,
                        activo = 1,
                        idRol = indexRol,
                        fechaNaci= (fecha),                  
                        password = pass,
                       
                    };

                    Uri request = new Uri(url);
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

                            await DisplayAlert("Registrado", "Se guardo correctamente", "ok");
                            nombres.Text = "";
                            apellidos.Text = "";
                            correo.Text = "";
                            cedula.Text = "";
                            telefono.Text = "";
                            direccion.Text = "";                            
                           
                            Picker.SelectedIndex = 0;
                            PickerRol.SelectedIndex = 0;
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
            }
            catch (Exception ex)
            {

                await DisplayAlert("ok", ex.Message, "ok");

            } 
        }

       //metodo para capturar la fecha;

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
             fecha = e.NewDate.ToString("yyyy/MM/dd"); 
            //cambiar fecha 

            /*TimeSpan tiem = DatePicker.fecha
            DateTime fechaNaci = DateTime.Parse(fecha); */

           
        }

        private async void Validacion()
        {
            if (string.IsNullOrEmpty(nombres.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un nombre", "ok");
                nombres.Focus();
            }
            else if (string.IsNullOrEmpty(apellidos.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un apellido", "ok");
                apellidos.Focus();
            }
            else if (string.IsNullOrEmpty(correo.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un correo", "ok");
                correo.Focus();
            }
            else if (string.IsNullOrEmpty(cedula.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una cedula", "ok");
                cedula.Focus();
            }
            else if (string.IsNullOrEmpty(telefono.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un teléfono", "ok");
                telefono.Focus();
            }
            else if (string.IsNullOrEmpty(direccion.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una dirección", "ok");
                direccion.Focus();
            }
            
            
            else if (string.IsNullOrEmpty(fechanacimiento.Text))
            {
                await DisplayAlert("Error", "Debe ingresar una fecha", "ok");
                fechanacimiento.Focus();

            }
            else
            {

            }
            }

        //metodo para validar el correo 
        public void ValidarCorreo()
        {

        } 

        private  void prueba_Clicked(object sender, EventArgs e)

        { 
           

        }
        

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
             indexCurso = Picker.SelectedIndex;
            //ModelCurso estado = Picker.SelectedItem as ModelCurso;
            
        }

        private void PickerRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexRol = PickerRol.SelectedIndex;
        }
    } 
}