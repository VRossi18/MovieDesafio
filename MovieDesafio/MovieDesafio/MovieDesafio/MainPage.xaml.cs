using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SQLite;

namespace MovieDesafio
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void ButtonCadastrar(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string entry = movieName.Text;
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLiteConnection db = new SQLiteConnection(dbPath);
            Itens stock = new Itens();

            var request = (HttpWebRequest)WebRequest.Create($"http://www.omdbapi.com/?i=tt3896198&apikey=6ab36c9c&t={entry}");
            request.ContentType = "application/json";

            var requestResponse = (HttpWebResponse)request.GetResponse();
            using (var response = new StreamReader(requestResponse.GetResponseStream()))
            {
                var objMovie = JsonConvert.DeserializeObject<JsonMovies>(response.ReadToEnd());
                string message = $"Titulo:{objMovie.Title} \n Data de lançamento:{objMovie.Released} \n Diretor:{objMovie.Director}";
                await DisplayAlert("Sucesso", message, "cancel");
                db.CreateTable<Itens>();
                db.Insert(new Itens
                {
                    Nome = objMovie.Title,
                    DataLancamento = objMovie.Released
                });

            }

        }

        async void ButtonList(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            string message = string.Empty;
            SQLiteConnection db = new SQLiteConnection(dbPath);
            List<Itens> getAllStocks = db.Query<Itens>("SELECT * FROM Itens");
            foreach (var item in getAllStocks)
            {
                message = message + item.Nome + '\n';
            }
            await DisplayAlert("Lista de filmes", message, "cancel");
        }
    }
}
