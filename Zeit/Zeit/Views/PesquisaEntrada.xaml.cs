using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PesquisaEntrada : ContentPage
    {
        public PesquisaEntrada()
        {
            InitializeComponent();                        
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            carregaList(); // CarregandoListView - Evento load Xamarin.Forms
        }
        public void carregaList()
        {
                ProdutoDAO query = new ProdutoDAO();
                ltvProdutos.ItemsSource = query.listaProduto();
                ltvProdutos.Footer = $"Foram encontrados {query.listaProduto().Count} produtos";
        }
        private void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            ProdutoDAO p = new ProdutoDAO();            
            ltvProdutos.ItemsSource = p.listaProduto(txtNome.Text.ToString());
            ltvProdutos.Footer = $"Foram encontrados {p.listaProduto(txtNome.Text.ToString()).Count} produtos";//Exibe a quantidade de itens no footer do list
        }

        private async void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                var produto = button.BindingContext as Produto; // variável que recebe o tipo produto e o contexto (dados) do botão selecionado
                PromptResult result = await UserDialogs.Instance.PromptAsync($"Quantidade Atual: {produto.quantidade}", $"{produto.nome}", "Adicionar", "Cancelar", "Quantidade a inserir", InputType.Number); // Poup up que pega o valor a ser adicionado

                if (result.Ok && !String.IsNullOrEmpty(result.Text)) // validação para ser feito insert
                {
                    ProdutoDAO query = new ProdutoDAO();
                    query.adicionar(produto, Convert.ToInt32(result.Text));
                    await DisplayAlert("Confirmação", "Adição feita com sucesso", "Ok");
                    carregaList(); //Carregar list após update para renovar conexão e atualizar a lista. (caso não seja feito, ira apresentar erro na hora de procurar um produto)
                }else if (result.Ok && result.Text.Equals("")){ //Caso não tenha sido inserido nenhum valor e selecionado adicionar
                    await DisplayAlert("Aviso", "Digite um valor a ser inserido.", "Ok");
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro de banco de dados"+ex.Message, "Ok");
            }
        }

        private void btnRetirada_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}