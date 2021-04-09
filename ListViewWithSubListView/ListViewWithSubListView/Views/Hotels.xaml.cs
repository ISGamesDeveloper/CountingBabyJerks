using ListViewWithSubListView.Models;
using ListViewWithSubListView.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using Todo;

namespace ListViewWithSubListView.Views
{
    public partial class Hotels : ContentPage
	{

        private HotelsGroupViewModel ViewModel
        {
            get { return (HotelsGroupViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        public List<ListDate> listDates = new List<ListDate>();

        protected override async void OnAppearing()
        {
            /*
            base.OnAppearing();
            TodoItemDatabase database = await TodoItemDatabase.Instance;

            await SetListDates(database);

            listView.ItemsSource = listDates;
            */

            
            try
            {
                base.OnAppearing();

                if (ViewModel.Items.Count == 0)
                {
                  ViewModel.LoadHotelsCommand.Execute(null);
                }           
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.Message);
            }
            
        }

        public Hotels(HotelsGroupViewModel viewModel)
		{
			InitializeComponent();
            this.ViewModel = viewModel;           //
        }

        public void OnButtonClicked(object s, EventArgs e)
        {
            Console.WriteLine("111");
        }

        public async Task SetListDates(TodoItemDatabase database)
        {
            var allTodo = await database.GetItemsAsync();

            listDates.Clear();

            for (int i = 0; i < allTodo.Count; i++)
            {
                var isEquals = false;

                for (int k = 0; k < listDates.Count; k++)
                {
                    var d1 = listDates[k].date.Date.ToString("d/MM/yyyy");
                    var d2 = allTodo[i].myDate.Date.ToString("d/MM/yyyy");

                    Console.WriteLine("d1: " + d1 + "   d2: " + d2);

                    if (d1.Equals(d2))
                    {
                        listDates[k].todoItem.Add(allTodo[i]);
                        isEquals = true;
                    }
                }

                if (!isEquals)
                {
                    listDates.Add(new ListDate()
                    {
                        date = allTodo[i].myDate,
                        time = allTodo[i].myTime,
                        IsChecked = false
                    });

                    listDates[listDates.Count - 1].todoItem.Add(allTodo[i]);
                }
            }

            Console.WriteLine(listDates.Count);
        }
    }

    public class ListDate : INotifyPropertyChanged
    {
        public DateTime date;
        public TimeSpan time;

        public List<TodoItem> todoItem = new List<TodoItem>();


        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public bool IsChecked = false;
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool HasText { get; set; }
        public DateTime myDate { get { return defaultDate; } set { defaultDate = value; } }
        private DateTime defaultDate = DateTime.Now;
        public TimeSpan myTime { get { return defaultTime; } set { defaultTime = value; } }
        private TimeSpan defaultTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
    }
}
