using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using TodolistMVVM;
using System.Collections.ObjectModel;

namespace TodolistMVVM
{
    public partial class MainPage : ContentPage
    {
 
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new TaskListView.TaskListView();
           
        }
    }
}
