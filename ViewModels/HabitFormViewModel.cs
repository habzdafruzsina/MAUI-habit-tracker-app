using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace beadando
{
    class HabitFormViewModel : INotifyPropertyChanged
    {
        private readonly HabitsDataSource habitsDataSource;

        public event PropertyChangedEventHandler PropertyChanged;

        private string id;

        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private double _red;
        public double Red
        {
            get => _red;
            set
            {
                _red = value;
                OnPropertyChanged(nameof(Red));
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        private double _green;
        public double Green
        {
            get => _green;
            set
            {
                _green = value;
                OnPropertyChanged(nameof(Green));
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        private double _blue;
        public double Blue
        {
            get => _blue;
            set
            {
                _blue = value;
                OnPropertyChanged(nameof(Blue));
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        public Color SelectedColor => Color.FromRgb((int)Red, (int)Green, (int)Blue);


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SubmitCommand { get; }

        public HabitFormViewModel(HabitsDataSource habitsDataSource, Habit habit)
        {
            this.habitsDataSource = habitsDataSource;
            SubmitCommand = new Command(saveHabit);
            if(habit != null)
            {
                this.id = habit.Id;

                _text = habit.Text;

                Green = (habit.Color.Green * 255);
                OnPropertyChanged(nameof(Green));

                Red = (habit.Color.Red * 255);
                OnPropertyChanged(nameof(Red));

                Blue = (habit.Color.Blue * 255);
                OnPropertyChanged(nameof(Blue));

                OnPropertyChanged(nameof(SelectedColor));
            }

        }


        public async void saveHabit()
        {
            if(id == null)
            {
                id = Guid.NewGuid().ToString();
            }
            Habit newHabit = new Habit(id, _text, SelectedColor, DateOnly.FromDateTime(DateTime.Today), new List<DateOnly>());
            habitsDataSource.SaveHabit(newHabit);
        }
    }

}