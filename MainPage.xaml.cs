namespace beadando
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RotateLogo();
        }

        private async void RotateLogo()
        {
            await Task.Delay(2000);
            await Logo.RelRotateTo(360, 400);
        }

    }

}
