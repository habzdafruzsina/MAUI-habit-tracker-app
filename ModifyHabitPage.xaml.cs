using System.Diagnostics;
using System.Windows.Input;

namespace beadando;

public partial class ModifyHabitPage : ContentPage
{
    private HabitFormViewModel habitFormViewModel;

    public ModifyHabitPage(Habit habit)
	{
        InitializeComponent();
        var dataModel = App.GetService<HabitsDataSource>();
        habitFormViewModel = new HabitFormViewModel(dataModel, habit);
        BindingContext = habitFormViewModel;
    }

    public ICommand ModifyHabit => new Command(onModifyHabit);

    private async void onModifyHabit()
    {
        habitFormViewModel.saveHabit();
        await Navigation.PopAsync();
    }

}