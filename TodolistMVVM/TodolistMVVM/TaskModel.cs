using System;
using System.Collections.Generic;
using System.Text;
namespace TodolistMVVM.TaskModel
{
    public class TaskModel //Модель данных задачи в листе
    {
        public string TextTask { get; set; } //сама задача
        public bool isFinished { get; set; } //завершена ли она или нет

    }
}
