using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace beadando
{
    internal class TodayHabitViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly HabitsDataSource habitsDataSource;
        private ObservableCollection<Habit> _habitsList;

        public ObservableCollection<Habit> habitsList
        {
            get { return _habitsList; }
            set
            {
                if (_habitsList != value)
                {
                    _habitsList = value;
                    OnPropertyChanged(nameof(habitsList));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TodayHabitViewModel(HabitsDataSource habitsDataSource)
        {
            this.habitsDataSource = habitsDataSource;
            habitsList = new ObservableCollection<Habit>(habitsDataSource.habits);
            filterHabitsList();
            habitsDataSource.PropertyChanged += OnPropertyChanged;
            ((INotifyCollectionChanged)habitsDataSource.habits).CollectionChanged += ModelListChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.habitsList = new ObservableCollection<Habit>(habitsDataSource.habits);
            filterHabitsList();
            OnPropertyChanged(nameof(habitsList));
        }

        private void ModelListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            habitsList = new ObservableCollection<Habit>(sender as IList<Habit>);
            filterHabitsList();
            OnPropertyChanged(nameof(habitsList));
        }


        public ICommand DeleteCommand => new Command<Habit>(RemoveHabit);

        void RemoveHabit(Habit habit)
        {
            habit.AchievementDates.Add(new DateOnly());
            habitsDataSource.SaveHabit(habit);
        }


        public void filterHabitsList()
        {
            List<Habit> newHabitsList = new List<Habit>(habitsList);
            newHabitsList.RemoveAll(habit =>
                habit.AchievementDates.Any() && Equals(habit.AchievementDates.Last(), DateOnly.FromDateTime(DateTime.Today)));
            habitsList = new ObservableCollection<Habit>(newHabitsList);
        }

    }
}
