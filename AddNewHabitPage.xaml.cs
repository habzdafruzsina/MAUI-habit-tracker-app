using System.Windows.Input;

namespace beadando;

public partial class AddNewHabitPage : ContentPage
{
    private HabitFormViewModel habitFormViewModel { get; set; }
	public AddNewHabitPage()
	{
		InitializeComponent();
        var dataModel = App.GetService<HabitsDataSource>();
        habitFormViewModel = new HabitFormViewModel(dataModel, null);
        BindingContext = habitFormViewModel;
    }

    public ICommand AddHabit => new Command(onAddHabit);

    private async void onAddHabit()
    {
        habitFormViewModel.saveHabit();
        await Navigation.PopAsync();
    }
}