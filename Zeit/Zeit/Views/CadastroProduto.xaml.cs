using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroProduto : ContentPage
    {
        string _Cpf;
        public CadastroProduto(string Cpf)
        {
            InitializeComponent();
            loadPicker();
            this.Title = "Cadastrar Produto";
            _Cpf = Cpf;
        }  
        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                
                ProdutoDAO query = new ProdutoDAO();
                query.inserir(getProduto());
                DisplayAlert("Confirmação", "Produto Cadastrado com sucesso!", "Ok");
                EntradaDAO _query = new EntradaDAO();
                _query.entrada(new Entrada
                {
                    id_produto = query.listaProduto().OrderByDescending(x => x.id).Select(x => x.id).First(),
                    quantidade = Convert.ToInt32(txtQuantidade.Text),
                    data = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd")),
                    horario = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")),
                    cpf_usuario = _Cpf,
                });
                limpar();

            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Ok");
            }             
        }

        #region GETPRODUTO /LOADPICKER /LIMPAR 
        public Produto getProduto()
        {
            var dep = (Departamento)pckDepartamento.SelectedItem;
            var forn = (Fornecedor)pckFornecedor.SelectedItem;
            Nullable<DateTime> data = null;

            Produto p = new Produto();
            p.nome = txtNome.Text.ToString();
            p.descricao = txtDescricao.Text.ToString();
            p.quantidade = Convert.ToInt32(txtQuantidade.Text);
            p.validade = ckbData.IsChecked ? data : dpckValidade.Date;
            p.id_fornecedor = forn.id;
            p.id_departamento = dep.id;
            return p;
        }
        public void loadPicker()
        {
            try
            {
                DepartamentoDAO d = new DepartamentoDAO();
                pckDepartamento.ItemsSource = d.listadepartamento();
                
                FornecedorDAO f = new FornecedorDAO();
                pckFornecedor.ItemsSource = f.listafornecedores();
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "Ok") ;
            }
        }
        public void limpar()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtQuantidade.Text = "";
            pckDepartamento.SelectedIndex = -1;
            pckFornecedor.SelectedIndex = -1;
            txtNome.Focus();
            ckbData.IsChecked = false;
        }
        #endregion 
    }
}
