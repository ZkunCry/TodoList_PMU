using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using TodolistMVVM.TaskModel;

namespace TodolistMVVM.TaskViewModel
{
    class TaskViewModel:INotifyPropertyChanged //Класс, который представляет 
    {
       public event PropertyChangedEventHandler PropertyChanged;

       private TaskModel.TaskModel task;
       public TaskViewModel()
        {
            task = new TaskModel.TaskModel { isFinished = false };
        }
        public string TextTask { get=>task.TextTask; 
            set 
            { 
                task.TextTask = value;
                OnPropertyChanged("TextTask");
            } 
        }

        public bool isFinished { get => task.isFinished;
            set
            {
                task.isFinished = task.isFinished != false ? true : false;
                OnPropertyChanged("Finished");
            }
            
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
