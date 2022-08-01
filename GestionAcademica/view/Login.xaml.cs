﻿using DocumentFormat.OpenXml.Spreadsheet;
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

namespace GestionAcademica.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Login : ContentPage
    {       

       string url = "http://"+ModelServidor.url+"/GestionAcademico/postLogin.php";
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
                    var response = await Client.PostAsync(request, contenJson);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var contenido = await response.Content.ReadAsStringAsync();

                        Model.ModelUsuario result = new Model.ModelUsuario();
                        //convertimos los datos a json
                        result = JsonConvert.DeserializeObject<Model.ModelUsuario>(contenido);

                        if (result.success == "true")
                        {
                          int Usuario_Id = result.usuario_Id;
                            perfil(result.usuario_Id);
                            await Navigation.PushAsync(new Menu());
                        }
                        else
                        {
                            await DisplayAlert("Error", "Datos invalidos", "ok");

                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", "Datos invalidos", "ok");
                    }

                }

            }
            catch (Exception ex)
            {

                await DisplayAlert("ok", ex.Message, "ok");

            }
            
        }


        private void btnRecuperar_Clicked(object sender, EventArgs e)
        {

        }

        private async void perfil(int Usuario_Id)
        {
            string urlUsuario = "http://"+ModelServidor.url+"/GestionAcademico/postPerfil.php?Usuario_Id=" + Usuario_Id;
                var content = await client.GetStringAsync(urlUsuario);
                List<ModelPerfil> postUsuario = JsonConvert.DeserializeObject<List<ModelPerfil>>(content);
                _postUsuarios = new ObservableCollection<ModelPerfil>(postUsuario);
               
            
        }
    }
}