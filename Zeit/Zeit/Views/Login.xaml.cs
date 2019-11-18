using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace Zeit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        UsuarioDAO usuario = new UsuarioDAO();
        public Login()
        {
            InitializeComponent();
        }
        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                {
                  if (String.IsNullOrWhiteSpace(txtCpf.Text) || String.IsNullOrWhiteSpace(txtSenha.Text))
                    {
                        await DisplayAlert("Erro", "Os campos de CPF e senha precisam estar preenchidos !", "Ok");
                    }
                    else
                    {
                        if (usuario.ValidarLogin(txtCpf.Text.ToString(), txtSenha.Text.ToString()))
                        {
                            
                            using (var dialog = UserDialogs.Instance.Loading($"Bem vindo {usuario.GetByID(txtCpf.Text.ToString())}\n Estamos preparando tudo para você !", null, null, true, MaskType.Gradient))
                            {
                                await Task.Delay(4000);
                                Application.Current.MainPage = new NavigationPage(new MainPage(txtCpf.Text.ToString()));
                            }
                                                       
                        }
                        else
                        {
                            await DisplayAlert("Erro", "CPF ou senha estão incorretos!", "Ok");
                            txtSenha.Text = "";
                            txtCpf.Text = "";
                        }
                    }
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Erro", "Erro ao se conectar: "+ex.Message, "Ok");
            }   
        }
    }
}