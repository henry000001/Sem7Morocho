using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Sem7Morocho.Models;
using System.IO;

namespace Sem7Morocho
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private SQLiteAsyncConnection con;
        public Login()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();

        }

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {

            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = Select_where(db, txtUsuario.Text, txtcontrasena.Text);
                if(resultado.Count() > 0)
                {
                    Navigation.PushAsync(new ConsultaRegistro());

                }
                else
                {
                    DisplayAlert("Alerta", "Usuario o Contrasena", "Ok");
                }
            }
            catch (Exception ex)
            {
                   DisplayAlert("Alerta", ex.Message, "OK");
            }

        }
         

        public static IEnumerable<Estudiante>  Select_where(SQLiteConnection db, string usuario, string contrasena)
        {
            return db.Query<Estudiante>("SELECT * FROM Estudiante where Usuario = ? and Contrasena=?", usuario, contrasena);
        }

        private async void btnRegistar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Registro());
        }
    }
}
