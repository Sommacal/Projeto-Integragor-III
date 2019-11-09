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
    public partial class CadastroFornecedor : ContentPage
    {
        public CadastroFornecedor()
        {
            InitializeComponent();
            this.Title = "Cadastrar Fornecedor";
        }

        public void limpar()
        {
            txtNome.Text = "";
            txtCpnj.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
            txtEndereco.Text = "";
            txtNome.Focus();
        }
        public Fornecedor getFornecedor()
        {
            Fornecedor f = new Fornecedor();
            f.nome = txtNome.Text.ToString();
            f.cnpj = txtCpnj.Text.ToString();
            f.email = txtEmail.Text.ToString();
            f.telefone = txtTelefone.Text.ToString();
            f.endereco = txtEndereco.Text.ToString();
            
            return f;
        }
        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                FornecedorDAO query = new FornecedorDAO();
                query.inserir(getFornecedor());
                limpar();
                DisplayAlert("Confirmação", "Fornecedor Cadastrado com sucesso!", "Ok");
            } catch  (Exception ex)
            {
                DisplayAlert("Erro", "Erro ao cadastrar"+ ex.Message, "Ok");
            }
           

        }
    }
}