using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;

namespace Zeit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        List<Microcharts.Entry> entries = new List<Microcharts.Entry>
        {
            new Microcharts.Entry(200)
            {
                Label = "Janeiro",
                ValueLabel = "200",
                Color = SKColor.Parse("#266489")
            },
            new Microcharts.Entry(250)
            {
                Label = "Fevereiro",
                ValueLabel = "250",
                Color = SKColor.Parse("#68B9C0")
            },
            new Microcharts.Entry(100)
            {
                Label = "Março",
                ValueLabel = "100",
                Color = SKColor.Parse("#90D585")
            },
            new Microcharts.Entry(150)
            {
                Label = "Abril",
                ValueLabel = "150",
                Color = SKColor.Parse("#90D585")
            },
        };
        public MainPage()
        {
            InitializeComponent();
            this.Title = "ZEIT";
            produtos.Chart = new Microcharts.DonutChart() { Entries = entries };
            produtos.Chart.LabelTextSize = 40;
            produtos.HeightRequest = 250;


            entradas.Chart = new Microcharts.RadialGaugeChart() { Entries = entries };
            entradas.Chart.LabelTextSize = 40;
            entradas.HeightRequest = 250;

            saidas.Chart = new Microcharts.BarChart() { Entries = entries };
            saidas.Chart.LabelTextSize = 40;
            saidas.HeightRequest = 250;
        }
        private async void btnDepartamento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroDepartamento());
        }
        private async void btnFornecedor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroFornecedor());
        }
        private async void btnPesquisa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Adicionar_Retirar());
        }
    }
}
