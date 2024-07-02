using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace beadando
{


    class HabitsDataSource : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Habit> _habits;
        public ObservableCollection<Habit> habits
        {
            get => _habits;
            set
            {
                _habits = value;
                OnPropertyChanged(nameof(habits));
            }
        }

        public HabitsDataSource()
        {
            habits = new ObservableCollection<Habit>();
            //initExampleFiles();
            LoadDataFromFile();
            foreach(Habit habit in habits)
            {
                Debug.WriteLine(habit.Text);
                Debug.WriteLine(habit.Id);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        private void LoadDataFromFile()
        { 
            string dateFormat = "yyyy. MM. dd.";
            string[] filenames = Directory.GetFiles(FileSystem.AppDataDirectory);

            
            foreach (string file in filenames)
            {
                if (File.Exists(file) && Path.GetExtension(file).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        Habit habit = new Habit();

                        string id = file.Substring(0, file.Length - 4);
                        Debug.WriteLine(id);
                        habit.Id = id;
                        habit.Text = reader.ReadLine();

                        float red = float.Parse(reader.ReadLine());
                        float green = float.Parse(reader.ReadLine());
                        float blue = float.Parse(reader.ReadLine());
                        habit.Color = new Color(red, green, blue);

                        habit.StartDate = DateOnly.ParseExact(reader.ReadLine(), dateFormat, CultureInfo.InvariantCulture);
                        Debug.WriteLine(habit.StartDate.ToString());

                        habit.AchievementDates = new List<DateOnly>();

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            habit.AchievementDates.Add(DateOnly.ParseExact(line, dateFormat, CultureInfo.InvariantCulture));
                        }

                        habits.Add(habit);
                    }
                }
            }

        }

        private void initExampleFiles()
        {
            string[] filenames = Directory.GetFiles(FileSystem.AppDataDirectory);
            foreach (string filename in filenames)
            {
                if (File.Exists(filename) && Path.GetExtension(filename).Equals(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    File.Delete(filename);
                }
            }

            List<DateOnly> h1list = new List<DateOnly>();
            h1list.Add(new DateOnly(2024, 1, 25));
            h1list.Add(new DateOnly(2024, 1, 28));
            h1list.Add(new DateOnly(2024, 1, 30));
            h1list.Add(new DateOnly(2024, 2, 2));
            h1list.Add(new DateOnly(2024, 2, 4));
            h1list.Add(new DateOnly(2024, 2, 6));
            h1list.Add(new DateOnly(2024, 2, 8));
            h1list.Add(new DateOnly(2024, 2, 10));
            h1list.Add(new DateOnly(2024, 2, 11));
            h1list.Add(new DateOnly(2024, 2, 12));
            h1list.Add(new DateOnly(2024, 2, 14));
            h1list.Add(new DateOnly(2024, 3, 4));
            h1list.Add(new DateOnly(2024, 4, 6));
            h1list.Add(new DateOnly(2024, 5, 1));
            Habit h1 = new Habit(Guid.NewGuid().ToString(), "sport", Color.FromRgb(38, 127, 90), new DateOnly(2024, 1, 25), new List<DateOnly>(h1list));
            Debug.WriteLine("starting to save h1");
            SaveHabit(h1);
            Debug.WriteLine("saved h1");

            List<DateOnly> h2list = new List<DateOnly>();
            h2list.Add(new DateOnly(2024, 5, 21));
            h2list.Add(new DateOnly(2024, 5, 22));
            h2list.Add(new DateOnly(2024, 5, 25));
            h2list.Add(new DateOnly(2024, 5, 28));
            h2list.Add(new DateOnly(2024, 5, 29));
            h2list.Add(new DateOnly(2024, 6, 2));
            h2list.Add(new DateOnly(2024, 6, 4));
            h2list.Add(new DateOnly(2024, 6, 18));
            h2list.Add(new DateOnly(2024, 6, 19));
            h2list.Add(new DateOnly(2024, 6, 22));
            Habit h2 = new Habit(Guid.NewGuid().ToString(), "read", Color.FromRgb(123, 32, 40), new DateOnly(2024, 5, 20), new List<DateOnly>(h2list));
            SaveHabit(h2);
            Debug.WriteLine("saved h2");

            List<DateOnly> h3list = new List<DateOnly>();
            h3list.Add(new DateOnly(2024, 6, 26));
            h3list.Add(new DateOnly(2024, 6, 27));
            h3list.Add(new DateOnly(2024, 6, 28));
            h3list.Add(new DateOnly(2024, 6, 29));
            Habit h3 = new Habit(Guid.NewGuid().ToString(), "draw", Color.FromRgb(123, 152, 40), new DateOnly(2024, 6, 26), new List<DateOnly>(h3list));
            SaveHabit(h3);
            Debug.WriteLine("saved h3");
        }

        public void SaveHabit(Habit newHabit)
        {
            Habit oldHabit = habits.FirstOrDefault(habit => habit.Id == newHabit.Id, null);
            
            if(oldHabit != null)
            {
                oldHabit.Text = newHabit.Text;
                oldHabit.Color = newHabit.Color;
            }
            else
            {
                habits.Add(newHabit);
                Debug.WriteLine("saving habit: ");
                Debug.WriteLine(newHabit.Text);
                Debug.WriteLine(newHabit.Id);
            }
            OnPropertyChanged(nameof(habits));

            string _fileName = Path.Combine(FileSystem.AppDataDirectory, newHabit.Id + ".txt");
            Debug.WriteLine(_fileName);

            using (StreamWriter writer = new StreamWriter(_fileName))
            {
                writer.WriteLine(newHabit.Text);
                writer.WriteLine(newHabit.Color.Red.ToString());
                writer.WriteLine(newHabit.Color.Green.ToString());
                writer.WriteLine(newHabit.Color.Blue.ToString());
                writer.WriteLine(newHabit.StartDate.ToString());
                foreach (var date in newHabit.AchievementDates)
                {
                    writer.WriteLine(date.ToString());
                }
            }
        }

        public void DeleteHabit(Habit habit)
        {
            if (habits.Contains(habit))
            {
                habits.Remove(habit);
            }

            string _fileName = Path.Combine(FileSystem.AppDataDirectory, habit.Id + ".txt");
            if (File.Exists(_fileName))
                File.Delete(_fileName);
        }

        public void SaveCompleteHabit(Habit habit)
        {
            habit.AchievementDates.Add(DateOnly.FromDateTime(DateTime.Today));
            Debug.WriteLine(habit.AchievementDates.Last().ToString());
            OnPropertyChanged(nameof(habits));
            
            string filePath = Path.Combine(FileSystem.AppDataDirectory, habit.Id + ".txt");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(habit.AchievementDates.Last().ToString());
            }
        }
    }

}
