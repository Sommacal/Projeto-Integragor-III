using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zeit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarProduto : ContentPage
    {
        int _id;
        List<Departamento> departamentos = new List<Departamento>();
        List<Fornecedor> fornecedores = new List<Fornecedor>();
        Produto produtoSelecionado = new Produto();
        ProdutoDAO p = new ProdutoDAO();
        DepartamentoDAO d = new DepartamentoDAO();
        FornecedorDAO f = new FornecedorDAO();
        public EditarProduto(int id)
        {
            InitializeComponent();
            _id = id; 
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                //GET PRODUTO SELECIONADO
                produtoSelecionado = p.GetByID(_id);

                //GET ALL DEPARTAMENTOS
                departamentos = d.listadepartamento();
                pckDepartamento.ItemsSource = departamentos.OrderBy(x => x.nome).ToList();

                //GET ALL FORNECEDORES
                fornecedores = f.listafornecedores();
                pckFornecedor.ItemsSource = fornecedores.OrderBy(x => x.nome).ToList();

                setCampos();
            }catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
                 
        }
        public void setCampos()
        {
            txtNome.Text = produtoSelecionado.nome;
            txtDescricao.Text = produtoSelecionado.descricao;
            txtQuantidade.Text = Convert.ToString(produtoSelecionado.quantidade);
            txtQuantidade.IsEnabled = false;
            if (produtoSelecionado.validade != null)
            {
                dpckValidade.Date = Convert.ToDateTime(produtoSelecionado.validade);
            }
            else
                ckbData.IsChecked = true;        
            //SET PICKER DEPARTAMENTO
            for (int i = 0; i < departamentos.Count; i++)
            {
                if (departamentos[i].id.Equals(produtoSelecionado.id_departamento))
                {
                    pckDepartamento.SelectedIndex = i;
                    break;
                }
            }
            //SET PICKER FORNECEDOR
            for (int i =0; i <fornecedores.Count; i++)
            {
                if (fornecedores[i].id.Equals(produtoSelecionado.id_fornecedor))
                {
                    pckFornecedor.SelectedIndex = i;
                    break;
                }
            }
        }
        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Confirmação", "Confirma as alterações ?", "Sim", "Não");
                if (result == true)
                {
                    var dep = (Departamento)pckDepartamento.SelectedItem;
                    var forn = (Fornecedor)pckFornecedor.SelectedItem;
                    Nullable<DateTime> data = null;

                    produtoSelecionado.nome = txtNome.Text.ToString();
                    produtoSelecionado.descricao = txtDescricao.Text.ToString();
                    produtoSelecionado.id_departamento = dep.id;
                    produtoSelecionado.id_fornecedor = forn.id;
                    produtoSelecionado.validade = ckbData.IsChecked ? data : dpckValidade.Date;
                    p.Update(produtoSelecionado);
                    await DisplayAlert("Confirmação", "Alterações realizadas com sucesso!", "Ok");
                    await Navigation.PopAsync(true);

                }
            }catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
      }
        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await DisplayAlert("Confirmação", "Confirma a exclusão ?", "Sim", "Não");
                if (result == true)
                {
                    p.Delete(produtoSelecionado.id);
                    await DisplayAlert("Confirmação", "Produto excluido com sucesso!", "Ok");
                    await Navigation.PopAsync(true);
                }
               
            }catch (Exception ex)
            {
                await DisplayAlert("Confirmação", ex.Message, "Ok");
            }
            
        }
    }
}