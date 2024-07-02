using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

namespace beadando
{
    internal class HabitsListViewModel : INotifyPropertyChanged
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

        public HabitsListViewModel(HabitsDataSource habitsDataSource)
        {
            this.habitsDataSource = habitsDataSource;
            habitsList = new ObservableCollection<Habit>(habitsDataSource.habits);
            habitsDataSource.PropertyChanged += OnPropertyChanged;
            ((INotifyCollectionChanged)habitsDataSource.habits).CollectionChanged += ModelListChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.habitsList = new ObservableCollection<Habit>(habitsDataSource.habits);
            foreach(Habit habit in habitsList)
            {
                Debug.WriteLine(habit.Text);
            }
            OnPropertyChanged(nameof(habitsList));
        }


        private void ModelListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            habitsList = new ObservableCollection<Habit>(sender as IList<Habit>);
            OnPropertyChanged(nameof(habitsList));
        }


        public void RemoveHabit(Habit habit)
        {
            habitsDataSource.DeleteHabit(habit);
            OnPropertyChanged(nameof(habitsList));
        }

    }
}
