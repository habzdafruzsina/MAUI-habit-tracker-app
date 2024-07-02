namespace beadando
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            Task.Run(async () =>
            {
                await Task.Delay(4000);
                Device.BeginInvokeOnMainThread(() =>
                {
                    MainPage = new AppShell(); ;
                });
            });
        }

        public static T GetService<T>() where T : class
        {
            return MauiProgram.AppInstance.Services.GetService(typeof(T)) as T;
        }
    }

}
