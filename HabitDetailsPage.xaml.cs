using System.Windows.Input;

namespace beadando;

public partial class HabitDetailsPage : ContentPage
{
    public ICommand ModifyHabit => new Command<Habit>(onModifyHabit);

    private int lastSevenDaysRate;
    private int lastMonthRate;
    private int allTimeRate;

    public HabitDetailsPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        countLastSevenDaysRate();
        countLastMonthRate();
        countAllTimeRate();
    }

    private void countAllTimeRate()
    {
        if (BindingContext is Habit habit)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int daysBetween = (today.ToDateTime(TimeOnly.MinValue) - habit.StartDate.ToDateTime(TimeOnly.MinValue)).Days +1;
            if (daysBetween > 0)
            {
                allTimeRate = (int)Math.Round((double)habit.AchievementDates.Count / daysBetween * 100);
            }
            allTimeRateLbl.Text = allTimeRate.ToString() + "%";
        }
    }

    private void countLastMonthRate()
    {
        if (BindingContext is Habit habit)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly begginingOfLastMonth = new DateOnly(today.AddMonths(-1).Year, today.AddMonths(-1).Month, 1);
            DateOnly endOfLastMonth = new DateOnly(begginingOfLastMonth.Year, begginingOfLastMonth.Month,
                DateTime.DaysInMonth(begginingOfLastMonth.Year, begginingOfLastMonth.Month));
            int periodLength = DateTime.DaysInMonth(begginingOfLastMonth.Year, begginingOfLastMonth.Month);

            if (habit.StartDate > begginingOfLastMonth && begginingOfLastMonth.Month == habit.StartDate.Month)
            {
                begginingOfLastMonth = habit.StartDate;
                periodLength = (endOfLastMonth.ToDateTime(TimeOnly.MinValue) - habit.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;
            }

            List<DateOnly> lastMonthAchievementDates = habit.AchievementDates.Where(date => date >= begginingOfLastMonth && date <= endOfLastMonth).ToList();

            if (lastMonthAchievementDates.Count > 0)
            {
                lastMonthRate = (int)Math.Round((double)lastMonthAchievementDates.Count / periodLength * 100);
                lastMonthRateLbl.Text = lastMonthRate.ToString() + "%";
            }
        }
    }

    private void countLastSevenDaysRate()
    {
        if (BindingContext is Habit habit)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly begginingOfPeriod = today.AddDays(-7);
            int periodLength = 7;

            if (habit.StartDate > begginingOfPeriod)
            {
                begginingOfPeriod = habit.StartDate;
                periodLength = (today.ToDateTime(TimeOnly.MinValue) - habit.StartDate.ToDateTime(TimeOnly.MinValue)).Days +1;
            }

            List<DateOnly> lastSevenDaysAchievementDates = habit.AchievementDates.Where(date => date >= begginingOfPeriod && date <= today).ToList();
            if (periodLength > 0)
            {
                lastSevenDaysRate = (int)Math.Round((double)lastSevenDaysAchievementDates.Count / periodLength * 100);
                sevenDaysRateLbl.Text = lastSevenDaysRate.ToString() + "%";
            }
        }
    }

    private async void onModifyHabit(Habit habit)
    {
        if (habit != null)
        {
            var modifyHabitPage = new ModifyHabitPage(habit);
            await Navigation.PushAsync(modifyHabitPage);
        }
    }
}