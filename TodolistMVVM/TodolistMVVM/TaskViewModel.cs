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
        public ICommand CompleteTaskCommand { get; set; }
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
            CompleteTaskCommand = new Command<CheckBox>(CompleteTask);
            
        }
        public void CompleteTask(CheckBox check)
        {
            if (!check.IsChecked)
                check.IsChecked = true;
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
            ListTasks.Add(new TaskModel.TaskModel() { TextTask=entrytext,isFinished=false});
        }
        public async void EditTask()
        {
            string text = await Application.Current.MainPage.DisplayPromptAsync("Редактирование", "Enter your new task", "OK", "Cancel",
               "Input your task", initialValue: "Example", keyboard: Keyboard.Default);
            if(!string.IsNullOrEmpty(text))
            {
                int newIndex = ListTasks.IndexOf(SelectedTask);
                ListTasks.Remove(SelectedTask);
                task.TextTask = text;
                ListTasks.Add(task);
                int oldindex = ListTasks.IndexOf(task);
                ListTasks.Move(oldindex, newIndex);
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
