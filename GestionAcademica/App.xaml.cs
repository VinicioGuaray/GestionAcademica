using GestionAcademica.view;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GestionAcademica
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new view.Recuperar());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
