using System.Windows.Input;

namespace beadando;

public partial class TodayPage : ContentPage
{
    public ICommand CompleteHabitForToday => new Command<Habit>(onCompleteHabitForToday);
    private HabitsDataSource dataModel;

    public TodayPage()
    {
        InitializeComponent();
        dataModel = App.GetService<HabitsDataSource>();
        BindingContext = new TodayHabitViewModel(dataModel);
    }

    private void onCompleteHabitForToday(Habit habit)
    {
        dataModel.SaveCompleteHabit(habit);
    }

    void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        //ezt nem értem
        //((ListView)sender).SelectedItem = null;
    }

}