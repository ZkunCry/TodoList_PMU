using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
namespace TodolistMVVM.TaskModel
{
    public class TaskModel : INotifyPropertyChanged 
    {
        private string textTask;
        private bool isFinished1;
        private string countelements;
        public string CountElements { get=>countelements; set { countelements = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountElements))); } }
        public string TextTask { get => textTask; set { textTask = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextTask))); } } //сама задача
        public bool isFinished { get => isFinished1; set { isFinished1 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(isFinished))); } } //завершена ли она или нет

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
