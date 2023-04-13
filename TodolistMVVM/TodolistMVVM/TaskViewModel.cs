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
        public ObservableCollection<TaskModel.TaskModel> ListTasks { get; set; } = 
            new ObservableCollection<TaskModel.TaskModel>();
        public TaskModel.TaskModel task;
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

            ListTasks.Add(new TaskModel.TaskModel() { TextTask=entrytext,isFinished=false});
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
