using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using TodolistMVVM.TaskModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TodolistMVVM.TaskViewModel
{
    public class TaskViewModel:INotifyPropertyChanged 
    {
       public event PropertyChangedEventHandler PropertyChanged;
       
       public ICommand CreateTaskCommand {set; get; }
        public ICommand DeleteTaskCommand { set; get; }
        public ICommand EditTaskCommand { get; set; }
        public ObservableCollection<TaskModel.TaskModel> ListTasks { get; set; } = 
            new ObservableCollection<TaskModel.TaskModel>();

        private TaskModel.TaskModel task;
        public TaskModel.TaskModel SelectedTask { 
            get
            {
                return task;
            }
            set
            {
                if(task !=value)
                {
                    task = value;
                    if(task != null)
                        EditTask();
                    OnPropertyChanged(nameof(SelectedTask));
                }
            }
        }

        public TaskViewModel()
        {
            task = new TaskModel.TaskModel();
            CreateTaskCommand = new Command<string>(Createtask,(current)=>!string.IsNullOrEmpty(current));
            DeleteTaskCommand = new Command<TaskModel.TaskModel>(DeleteTask);
            
        }
        public string Texttask
        {
            get=>task.TextTask;
            set 
            {
                task.TextTask = value;
                OnPropertyChanged(nameof(Texttask));
            }
        }
            
        public bool Finished { get => task.isFinished;
            set
            {
                task.isFinished = value != false ? true : false;
                OnPropertyChanged(nameof(Finished));
            }
        }
        
        private void Createtask(string entrytext)
        {
            int index= entrytext.IndexOf(':');
            if(index >-1)
            {
                string lhs = entrytext.Substring(0, index);
                string rhs = entrytext.Substring(index + 1);
                rhs = rhs.Replace(':'.ToString(), String.Empty);
                ListTasks.Add(new TaskModel.TaskModel() { TextTask = lhs, isFinished = false, CountElements = rhs });
            }
            else
            {
                ListTasks.Add(new TaskModel.TaskModel() { TextTask = entrytext, isFinished = false });
            }
           
            

        }
        public async void EditTask()
        {
            string text = await Application.Current.MainPage.DisplayPromptAsync("Редактирование","Текущая задача",initialValue:SelectedTask.TextTask);
            if(string.IsNullOrWhiteSpace(text))
            {
              
                if (text is null)
                    SelectedTask = null;
                else
                {
                    await Application.Current.MainPage.DisplayAlert(title: "Edit task", message: "Вы ничего не ввели", cancel: "Отмена");
                    SelectedTask = null;
                }
                
            }
            else
            {
                SelectedTask.TextTask = text;
                SelectedTask = null;
            }

        }
        private void DeleteTask(TaskModel.TaskModel CurrentTask)
        {
            ListTasks.Remove(CurrentTask);
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
