using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarDepartamento : ContentPage
    {
        public int _Id;
        DepartamentoDAO d = new DepartamentoDAO();
        Departamento departamentoSelecionado = new Departamento();
        public EditarDepartamento(int Id)
        {
            InitializeComponent();
            _Id = Id;
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                departamentoSelecionado = d.GetByID(_Id);
                setCampos();
            }catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");   
            }
        }
        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Confirmação", "Confirma as alterações ?", "Sim", "Não");
                if (result == true)
                {
                    departamentoSelecionado.nome = txtNome.Text.ToString();
                    departamentoSelecionado.descricao = txtDescricao.Text.ToString();
                    d.Update(departamentoSelecionado);
                    await DisplayAlert("Confirmação", "Alterações realizadas com sucesso!", "Ok");
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro ao realizar alterações!" + ex.Message, "Ok");
            }
            finally
            {
                await Navigation.PopAsync(true);
            }           
        }
        public void setCampos()
        {
            txtNome.Text = departamentoSelecionado.nome;
            txtDescricao.Text = departamentoSelecionado.descricao;
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Confirmação", "Confirma a exclusão ?", "Sim", "Não");
                if (result == true)
                {
                    d.Delete(departamentoSelecionado.id);
                    await DisplayAlert("Confirmação", "Departamento excluido com sucesso!", "Ok");
                }                    
            }
            catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            finally
            {
                await Navigation.PopAsync(true);
            }

        }
    }
}