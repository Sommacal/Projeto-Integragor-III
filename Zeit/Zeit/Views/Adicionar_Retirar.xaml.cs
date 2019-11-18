using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using Zeit.Views;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adicionar_Retirar : ContentPage
    {
        public Adicionar_Retirar()
        {
            InitializeComponent();
        }
        //EVENTO LOAD XAMARIN - CARREGANDO LIST QUANDO INICIA A PAGE
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            carregaList();
            this.Title = "Adicionar e Retirar";
        }

        // LISTANDO POR NOME DIGITADO 
        private async void txtNome_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ProdutoDAO p = new ProdutoDAO();

                //VERIFICA SE A LISTA ESTA VAZIA PARA NÃO DAR ERRO.
                if (p.listaProduto(txtNome.Text.ToString()) != null)
                {
                    ltvProdutos.ItemsSource = p.listaProduto(txtNome.Text.ToString());
                    ltvProdutos.Footer = $"Foram encontrados {p.listaProduto(txtNome.Text.ToString()).Count} produtos";//Exibe a quantidade de itens no footer do list
                }
                else
                {
                    ltvProdutos.ItemsSource = null;
                    ltvProdutos.Footer = "Não foi encontrado nenhum produto";
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
        private async void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //GET NO OBJETO SELECIONADO
                var button = sender as Button;
                var produto = (Produto)button.BindingContext;

                //POUP-UP QUE PEGA O VALOR A SER ADICIONADO
                PromptResult result = await UserDialogs.Instance.PromptAsync($"Quantidade Atual: {produto.quantidade}", $"{produto.nome}", "Adicionar", "Cancelar", "Quantidade a inserir", InputType.Number);
                if (result.Ok && !String.IsNullOrWhiteSpace(result.Text))
                {
                    ProdutoDAO query = new ProdutoDAO();

                    //ADICIONANDO QUANTIDADE AO PRODUTO
                    query.adicionar(produto, Convert.ToInt32(result.Text));
                    EntradaDAO _query = new EntradaDAO();

                    //INCLUSAO NA TABELA DE ENTRADA DE PRODUTOS
                    _query.entrada(getEntrada(produto, Convert.ToInt32(result.Text)));
                    await DisplayAlert("Confirmação", "Adição feita com sucesso", "Ok");
                    //ATUALIZA LIST APÓS ATUALIZAR O PRODUTO
                    carregaList();
                }
                //CASO USUÁRIO NÃO DIGITE NENHUM VALOR
                else if (result.Ok && string.IsNullOrWhiteSpace(result.Text))
                {
                    await DisplayAlert("Aviso", "Digite um valor a ser inserido.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
        private async void btnRetirada_Clicked(object sender, EventArgs e)
        {
            try
            {
                var button = sender as Button;
                var produto = button.BindingContext as Produto;
                PromptResult result = await UserDialogs.Instance.PromptAsync($"Quantidade Atual: {produto.quantidade}", $"{produto.nome}", "Retirar", "Cancelar", "Quantidade a retirar", InputType.Number);
                if (result.Ok && !String.IsNullOrWhiteSpace(result.Text))
                {
                    ProdutoDAO query = new ProdutoDAO();
                    query.retirar(produto, Convert.ToInt32(result.Text));
                    RetiradaDAO _query = new RetiradaDAO();
                    _query.retirada(getRetirada(produto, Convert.ToInt32(result.Text)));

                    await DisplayAlert("Confirmação", "Retirada feita com sucesso", "Ok");
                    carregaList();
                }
                else if (result.Ok && string.IsNullOrWhiteSpace(result.Text))
                {
                    await DisplayAlert("Aviso", "Digite um valor a ser inserido.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
        }
        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroProduto());
        }
        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var produto = (Produto)button.BindingContext;
            await Navigation.PushAsync(new EditarProduto(produto.id));
        }

        #region GET ENTRADA/GET RETIRADA/ CARREGALIST
        public Entrada getEntrada(Produto produto, int quantidade)
        {
            Entrada entrada = new Entrada();
            entrada.quantidade = quantidade;
            entrada.id_produto = produto.id;
            entrada.data = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            entrada.horario = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            return entrada;
        }
        public Retirada getRetirada(Produto produto, int quantidade)
        {
            Retirada retirada = new Retirada();
            retirada.quantidade = quantidade;
            retirada.id_produto = produto.id;
            retirada.data = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            retirada.horario = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
            return retirada;
        }
        public async void carregaList()
        {
            try
            {
                ProdutoDAO query = new ProdutoDAO();
                if (query.listaProduto() != null)
                {
                    ltvProdutos.ItemsSource = query.listaProduto();
                    ltvProdutos.Footer = $"Foram encontrados {query.listaProduto().Count} produtos";
                }
                else
                {
                    ltvProdutos.Footer = "Nenhum produto cadastrado";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro de banco de dados" + ex.Message, "Ok");
            }
        }
        #endregion
    }
}