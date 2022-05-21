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

namespace Sem7Morocho
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> tablaEstudiante;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            get();
        }
        public async void get()
        {
            var resultado = await con.Table<Estudiante>().ToListAsync();
            tablaEstudiante = new ObservableCollection<Estudiante>(resultado);
            listausuarios.ItemsSource = tablaEstudiante;
        } 
        private void listausuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
            
        {
            var obj = (Estudiante)e.SelectedItem;
            var idItem = obj.Id.ToString();
            int id = Convert.ToInt32(idItem);
            Navigation.PushAsync(new Elemento(id));
        }
    }
}