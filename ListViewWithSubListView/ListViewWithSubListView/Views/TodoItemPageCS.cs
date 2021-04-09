using System;
using ListViewWithSubListView.Views;
using Xamarin.Forms;

namespace Todo
{
    public class TodoItemPageCS : ContentPage
    {
        public TodoItemPageCS()
        {
            Title = "Todo Item";

            var notesEntry = new Entry();
            notesEntry.SetBinding(Entry.TextProperty, "Notes");

            var date = new DatePicker();
            date.SetBinding(DatePicker.DateProperty, "myDate");
            date.DateSelected += (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                todoItem.myDate = date.Date;
         
                Console.WriteLine(date.Date);
            };

            var time = new TimePicker();
            time.SetBinding(TimePicker.TimeProperty, "myTime");
            time.PropertyChanged += (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                var ts = new TimeSpan(time.Time.Hours, time.Time.Minutes, 0);
                todoItem.myTime = ts;
            };

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.SaveItemAsync(todoItem);
                await Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var todoItem = (TodoItem)BindingContext;
                TodoItemDatabase database = await TodoItemDatabase.Instance;
                await database.DeleteItemAsync(todoItem);
                await Navigation.PopAsync();
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Notes" },
                    notesEntry,
                    saveButton,
                    deleteButton
                }
            };
        }
    }
}
