using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaFornecedor : ContentPage
    {
        
        public ListaFornecedor()
        {
            InitializeComponent();
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                FornecedorDAO f = new FornecedorDAO();
                ltvFornecedores.ItemsSource = f.listafornecedores();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroFornecedor());
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var fornecedor = (Fornecedor)button.BindingContext;
            await Navigation.PushAsync(new EditarFornecedor(fornecedor.id));
        }       
    }
}