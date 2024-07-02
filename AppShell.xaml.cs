namespace beadando
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("home", typeof(TodayPage));
            Routing.RegisterRoute("habitslist", typeof(HabitsListPage));


            Navigated += OnNavigated;

            //ez vmiért nem működik..........
            var toolbarItem = new ToolbarItem
            {
                Text = "Info",
                IconImageSource = "info_icon.png",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            toolbarItem.Clicked += ToolbarItem_Clicked;
            ToolbarItems.Add(toolbarItem);
        }

        private async void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            if (e.Source == ShellNavigationSource.ShellSectionChanged)
            {
                var shellSection = Shell.Current.CurrentItem.CurrentItem as ShellSection;
                if (shellSection != null)
                {
                    var currentContent = shellSection.CurrentItem as ShellContent;
                    if (currentContent != null)
                    {
                        string route = currentContent.Route;

                        if (!string.IsNullOrEmpty(route))
                        {
                            await Shell.Current.GoToAsync($"//{route}");
                        }
                    }
                }
            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Instructions", "\nOn the Home page you can see the list of habits, that you have not yet done today. By sliding them to the side you are able to set them to \"done\". By tapping on one, you can see the details of each Habit, including rates of your achievements.\nOn the My habits page, the whole list of habits is displayed. You can delete, modify and add new habits to the list.", "OK");
        }
    }
}
