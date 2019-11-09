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
    public partial class CadastroDepartamento : ContentPage
    {
        public CadastroDepartamento()
        {
            InitializeComponent();
            this.Title = "Cadastrar Departamento";
        }
        public void limpar()
        {
            txtNome.Text = "";
            txtDescricao.Text = "";           
            txtNome.Focus();
        }
        public Departamento getDepartamento()
        {
            Departamento d = new Departamento();
            d.nome = txtNome.Text.ToString();
            d.descricao = txtDescricao.Text.ToString();
            return d;
        }
        private void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                DepartamentoDAO query = new DepartamentoDAO();
                query.inserir(getDepartamento());
                limpar();
                DisplayAlert("Confirmação", "Departamento Cadastrado com sucesso!", "Ok");
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", "Erro ao cadastrar; " +ex.Message, "Ok");
            }
          
        }
    }
}
    
