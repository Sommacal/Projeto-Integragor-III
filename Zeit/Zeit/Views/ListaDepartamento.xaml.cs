using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDepartamento : ContentPage
    {
        DepartamentoDAO d = new DepartamentoDAO();
        public ListaDepartamento()
        {
            InitializeComponent();
        }
        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            try
            {
              ltvDepartamentos.ItemsSource = d.listadepartamento().OrderBy(x => x.nome).ToList();
            }catch(Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "Ok");
            }
            }
        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var departamento = (Departamento)button.BindingContext;
            await Navigation.PushAsync(new EditarDepartamento(departamento.id));
        }
        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroDepartamento());
        }      
    }
}