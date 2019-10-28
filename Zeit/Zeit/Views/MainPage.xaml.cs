using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Zeit
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void btnDepartamento_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroDepartamento());
        }
        private async void btnFornecedor_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroFornecedor());
        }
        private async void btnProduto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroProduto());
        }
        private async void btnPesquisa_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Adicionar_Retirar());
        }
    }
}
