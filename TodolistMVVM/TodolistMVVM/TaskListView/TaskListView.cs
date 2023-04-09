using System;
using System.Collections.Generic;
using System.Text;
using TodolistMVVM.TaskViewModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace TodolistMVVM.TaskListView
{
    class TaskListView:INotifyPropertyChanged //Класс, который представляет список задач
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<TaskViewModel.TaskViewModel> Tasks { get; set; }
        public ICommand CreateTaskCommand { protected set; get; }
        public ICommand DeleteTaskCommand { protected set; get; }

        public TaskListView()
        {
            Tasks = new ObservableCollection<TaskViewModel.TaskViewModel>();
            CreateTaskCommand = new Command(Createtask);
        }
        private void Createtask(object Task)
        {
            Tasks.Add(new TaskViewModel.TaskViewModel { TextTask = "test" });
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
