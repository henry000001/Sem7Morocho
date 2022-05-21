using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Sem7Morocho.Models;
using System.IO;

namespace Sem7Morocho
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Elemento : ContentPage
    {
        private SQLiteAsyncConnection con;
        public int idseleccionado;
        IEnumerable<Estudiante> rUpdate;
        IEnumerable<Estudiante> rdELETE;
        private ObservableCollection<Models.Estudiante> tablaEstudiante;
        public Elemento(int id)
        {
            InitializeComponent();
            idseleccionado = id;
            con = DependencyService.Get<Database>().GetConnection();
        }

        public static IEnumerable<Estudiante> Update(SQLiteConnection db,string nombre,string usuario, string contrasena, int id)
        {
            return db.Query<Estudiante>("Update Estudiante SET Nombre =?, Usuario =?, contrasena=? Where id =? ", nombre, usuario, contrasena, id);
        }

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante where id = ?", id);
        }
        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasepath);
                rUpdate = Update(db, txtNombre.Text, txtUsuario.Text, txtContrasena.Text, idseleccionado);
                DisplayAlert("Alerta", "Se actualizo Correctamente", "Cerrar");

                Navigation.PushAsync(new ConsultaRegistro());


            }
            catch (Exception ex)
            {
                DisplayAlert("alerta", "ERROR" + ex.Message, "OK");
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {

            try
            {
                var databasepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasepath);
                rdELETE = Delete(db, idseleccionado);
                Navigation.PushAsync(new ConsultaRegistro());
            }
            catch (Exception ex)
            {
                DisplayAlert("alerta", "ERROR" + ex.Message, "OK");

            }


        }
    }
}