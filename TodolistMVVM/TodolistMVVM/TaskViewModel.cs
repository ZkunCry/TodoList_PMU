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
        public ObservableCollection<TaskModel.TaskModel> ListTasks { get; set; } = new ObservableCollection<TaskModel.TaskModel>();
        public TaskModel.TaskModel task;
        public TaskViewModel()
        {
            task = new TaskModel.TaskModel();
            CreateTaskCommand = new Command(Createtask);
            DeleteTaskCommand = new Command(DeleteTask);
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
        private void Createtask()
        {
            ListTasks.Add(task);
        }
        private void DeleteTask(object CurrentTask)
        {
            TaskModel.TaskModel Task = CurrentTask as TaskModel.TaskModel;
            ListTasks.Remove(Task);
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
