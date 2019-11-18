using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarFornecedor : ContentPage
    {
        int _Id;
        FornecedorDAO f = new FornecedorDAO();
        Fornecedor fornecedorSelecionado = new Fornecedor();
        public EditarFornecedor(int Id)
        {
            InitializeComponent();
            _Id = Id;
        }
        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            fornecedorSelecionado = f.GetByID(_Id);
            setCampos();
        }
        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Confirmação", "Confirma as alterações ?", "Sim", "Não");
                if (result == true)
                {
                    fornecedorSelecionado.nome = txtNome.Text.ToString();
                    fornecedorSelecionado.telefone = txtTelefone.Text.ToString();
                    fornecedorSelecionado.email = txtEmail.Text.ToString();
                    fornecedorSelecionado.endereco = txtEmail.Text.ToString();
                    fornecedorSelecionado.cnpj = txtCpnj.Text.ToString();
                    f.Update(fornecedorSelecionado);
                    await DisplayAlert("Confirmação", "Alterações realizadas com sucesso!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro ao realizar alterações!" + ex.Message, "Ok");
            }
            finally
            {
                await Navigation.PopAsync(true);
            }
        }
       
        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Confirmação", "Confirma a exclusão ?", "Sim", "Não");
                if (result == true)
                {
                    f.Delete(fornecedorSelecionado.id);
                    await DisplayAlert("Confirmação", "Fornecedor excluido com sucesso!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                await Navigation.PopAsync(true);
            }
        }
        public void setCampos()
        {
            txtNome.Text = fornecedorSelecionado.nome;
            txtCpnj.Text = fornecedorSelecionado.cnpj;
            txtEmail.Text = fornecedorSelecionado.email;
            txtEndereco.Text = fornecedorSelecionado.endereco;
            txtTelefone.Text = fornecedorSelecionado.telefone;
        }
    }
}
