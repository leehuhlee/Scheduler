using Prism.Commands;
using Scheduler.Models;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Input;

namespace Scheduler
{
    public class SchedulerViewModel : BaseViewModel
    {
        public static string fileName = "schedules.bin";

        public string TodayMonth { get; set; } = DateTime.Now.ToString("MMMM", new CultureInfo("de-DE"));
        public string TodayDay { get; set; } = DateTime.Now.Day.ToString();
        public string TodayDayOfWeek { get; set; } = DateTime.Now.DayOfWeek.ToString();

        public string TxtNote { get; set; }
        public string TxtTime { get; set; }

        public ICommand AddScheduleCommand { get; set; }

        private DelegateCommand<Schedule> _deleteCommand;

        public DelegateCommand<Schedule> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Schedule>(DeleteSchedule));

        private ObservableCollection<Schedule> schedules;
        public ObservableCollection<Schedule> Schedules
        {
            get { return schedules; }
            set
            {
                if (value != schedules)
                {
                    schedules = value;
                    OnPropertyChanged("Schedules");
                }
            }
        }

        private Schedule selectedSchedule;
        public Schedule SelectedSchedule
        {
            get { return selectedSchedule; }
            set
            {
                if (value != selectedSchedule)
                {
                    selectedSchedule = value;
                    OnPropertyChanged("SelectedSchedule");
                }
            }
        }

        public SchedulerViewModel()
        {
            Schedules = new ObservableCollection<Schedule>();
            SelectedSchedule = null;
            deserializeTasks();

            AddScheduleCommand = new RelayCommand(AddSchedule);
        }

        public void AddSchedule()
        {
            Schedules.Add(new Schedule { Check = false, Note = TxtNote, Deadline = TxtTime });
            serializeTasks();
            TxtNote = string.Empty;
            TxtTime = string.Empty;
        }

        public void DeleteSchedule(Schedule schedule)
        {
            Schedules.Remove(schedule);
            OnPropertyChanged("Schedules");
            serializeTasks();
        }

        public void serializeTasks()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open("schedules.bin", FileMode.OpenOrCreate);
                formatter.Serialize(stream, Schedules);
            }
            catch
            {
                throw new DriveNotFoundException();
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        public void deserializeTasks()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(fileName))
            {
                try
                {
                    stream = File.Open(fileName, FileMode.Open);
                    Schedules = (ObservableCollection<Schedule>)formatter.Deserialize(stream);
                }
                catch
                {
                    throw new FileNotFoundException();
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }
        }
    }
}
