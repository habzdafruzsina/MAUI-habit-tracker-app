using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace beadando
{
    public class Habit : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private string _id;
        public string Id {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }


        private string _text;
        public string Text
        {
            get => _text;
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        private Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }


        private DateOnly _startDate;
        public DateOnly StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private List<DateOnly> _achievementDates;
        public List<DateOnly> AchievementDates
        {
            get => _achievementDates;
            set
            {
                if (_achievementDates != value)
                {
                    _achievementDates = value;
                    OnPropertyChanged(nameof(AchievementDates));
                }
            }
        }

        public Habit(string Id, string Text, Color color, DateOnly StartDate, List<DateOnly> AchievementDates)
        {
            this.Id = Id;
            this.Text = Text;
            this.Color = color;
            this.StartDate = StartDate;
            this.AchievementDates = AchievementDates;
        }

        public Habit(Habit habit)
        {
            this.Id = habit.Id;
            this.Text = habit.Text;
            this.Color = habit.Color;
            this.StartDate = habit.StartDate;
            this.AchievementDates = habit.AchievementDates;
        }

        public Habit()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
