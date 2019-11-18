using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace Zeit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        string _Cpf;
        public MainPage(string Cpf)
        {
            InitializeComponent();
            _Cpf = Cpf;
            this.Title = "ZEIT";
            IsLoading = false;
            BindingContext = this;
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
           try
            {
                //RELATÓRIO DAS ULTIMAS 5 ENTRADAS
                EntradaDAO entrada = new EntradaDAO();                
                entradas.Chart = new Microcharts.RadialGaugeChart { Entries = entrada.Relatorio1() };
                entradas.Chart.LabelTextSize = 25;
                entradas.HeightRequest = 200;

                //RELATÓRIO DAS ULTIMAS 5 SAIDAS
                RetiradaDAO retirada = new RetiradaDAO();                
                List<Microcharts.Entry> r = new List<Microcharts.Entry>(); 
                r = retirada.Relatorio1();
                saidas.Chart = new Microcharts.RadialGaugeChart { Entries = r};
                saidas.Chart.LabelTextSize = 25;
                saidas.HeightRequest = 200;

                ProdutoDAO produtos = new ProdutoDAO();

                produtosCadastrados.Text = $"Totas de itens cadastrados: {Convert.ToString(produtos.listaProduto().Count)}";
                totalItens.Text = $"Total de itens em estoque: {Convert.ToString(produtos.listaProduto().Sum(x => x.quantidade))}";
                totalEntradas.Text = $"Total de Entradas: {Convert.ToString(entrada.GetAll().ToList().Count)}";
                totalRetiradas.Text = $"Total de retiradas: {Convert.ToString(retirada.GetAll().Count)}";

            }catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro de banco de dados: " + ex.Message, "Ok");
            }
        }   
        private async void btnDepartamento_Clicked(object sender, EventArgs e)
        {
            IsLoading = true;
            await Task.Delay(1500);
            await Navigation.PushAsync(new ListaDepartamento());
            IsLoading = false;
        }
        private async void btnFornecedor_Clicked(object sender, EventArgs e)
        {
            IsLoading = true;
            await Task.Delay(1500);
            await Navigation.PushAsync(new ListaFornecedor());
            IsLoading = false;
        }
        private async void btnPesquisa_Clicked(object sender, EventArgs e)
        {
            IsLoading = true;
            await Task.Delay(1500);
            await Navigation.PushAsync(new Adicionar_Retirar(_Cpf));           
            IsLoading = false;
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }
            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
