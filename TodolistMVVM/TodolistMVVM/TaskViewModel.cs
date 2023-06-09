﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using TodolistMVVM.TaskModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace TodolistMVVM.TaskViewModel
{
    public class TaskViewModel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged;
       
       public ICommand CreateTaskCommand {set; get; }
        public ICommand DeleteTaskCommand { set; get; }
        public ICommand EditTaskCommand { get; set; }
        public ObservableCollection<TaskModel.TaskModel> ListTasks { get; set; } =
            new ObservableCollection<TaskModel.TaskModel>();

        private TaskModel.TaskModel task;
        public TaskModel.TaskModel SelectedTask { 
            get=> task;
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
            CreateTaskCommand = new Command<string>(Createtask, (current) =>  current != null ? !string.IsNullOrEmpty(ParseString(current)[0]) : false);
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

        private string[] ParseString(string current)
        {
            int index = current.IndexOf(':');
            if (index > -1)
            {
                string lhs = current.Substring(0, index);
                if(string.IsNullOrWhiteSpace(lhs))
                    return new string[] { null,null };
                string rhs = current.Substring(index + 1);
                rhs = rhs.Replace(':'.ToString(), string.Empty);
               return new[] { lhs, rhs };
            }
            else
                return new [] {current,null };
        }
        private void Createtask(string entrytext)
        {
            var result = ParseString(entrytext);
            if (result == null)
                return;
            else if(string.IsNullOrEmpty(result[1]))
                ListTasks.Add(new TaskModel.TaskModel() { TextTask = result[0], isFinished = false });
            else
                ListTasks.Add(new TaskModel.TaskModel() { TextTask = result[0], isFinished = false, CountElements = result[1] });
        }
        public async void EditTask()
        {
            if (!SelectedTask.isFinished)
            {
                string text = await Application.Current.MainPage.DisplayPromptAsync("Editing", "Current task", initialValue: SelectedTask.TextTask);
                if (string.IsNullOrWhiteSpace(text))
                {
                    if (text is null)
                        SelectedTask = null;
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert(title: "Edit task", message: "You didn't enter anything", cancel: "Cancel");
                        SelectedTask = null;
                    }

                }
                else
                {
                    var str = ParseString(text);
                    if (str[0] != null)
                        SelectedTask.TextTask = str[0];
                    if (str[1] != null)
                        SelectedTask.CountElements = str[1];
                    SelectedTask = null;
                }
            }
            else
                SelectedTask = null;
        }
        private void DeleteTask(TaskModel.TaskModel CurrentTask)=>ListTasks.Remove(CurrentTask);

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
