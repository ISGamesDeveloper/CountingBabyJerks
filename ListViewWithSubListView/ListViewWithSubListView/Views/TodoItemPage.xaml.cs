﻿using System;
using ListViewWithSubListView.Views;
using Xamarin.Forms;

namespace Todo
{
    public partial class TodoItemPage : ContentPage
    {
        public TodoItemPage()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            await database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            await database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void TextChange(Object obj, TextChangedEventArgs args)
        {
            var todoItem = (TodoItem)BindingContext;
            TodoItemDatabase database = await TodoItemDatabase.Instance;

            if(args.NewTextValue.Trim().Length > 0)
            {
                todoItem.HasText = true;
            }
            else
            {
                todoItem.HasText = false;
            }

            await database.SaveItemAsync(todoItem);
        }
    }
}