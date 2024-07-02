using System.Diagnostics;
using System.Windows.Input;

namespace beadando;

public partial class HabitsListPage : ContentPage
{
    private HabitsListViewModel habitsListViewModel;

    public ICommand DeleteHabit => new Command<Habit>(onRemoveHabit);
    public ICommand ModifyHabit => new Command<Habit>(onModifyHabit);
    public ICommand OpenHabitDetails => new Command<Habit>(onOpenHabitDetails);
    public ICommand AddNewHabit => new Command(onAddNewHabit);

    public HabitsListPage()
    {
        InitializeComponent();
        var dataModel = App.GetService<HabitsDataSource>();
        habitsListViewModel = new HabitsListViewModel(dataModel);
        BindingContext = habitsListViewModel;
    }

    private void onRemoveHabit(Habit habit)
    {
        habitsListViewModel.RemoveHabit(habit);
    }

    private async void onModifyHabit(Habit habit)
    {
        if (habit != null)
        {
            var modifyHabitPage = new ModifyHabitPage(habit);
            await Navigation.PushAsync(modifyHabitPage);
        }
    }

    private async void onOpenHabitDetails(Habit habit)
    {
        if (habit != null)
        {
            var habitDetailsPage = new HabitDetailsPage();
            habitDetailsPage.BindingContext = habit;
            await Navigation.PushAsync(habitDetailsPage);
        }
    }

    private async void onAddNewHabit()
    {
        var addHabitPage = new AddNewHabitPage();
        await Navigation.PushAsync(addHabitPage);
    }

}