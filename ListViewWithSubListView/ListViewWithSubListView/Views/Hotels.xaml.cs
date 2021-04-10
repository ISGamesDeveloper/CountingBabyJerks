using ListViewWithSubListView.Models;
using ListViewWithSubListView.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Todo;
using Xamarin.Forms;

namespace ListViewWithSubListView.Views
{
    public partial class Hotels : ContentPage
	{

        private HotelsGroupViewModel ViewModel
        {
            get { return (HotelsGroupViewModel)BindingContext; }
            set { BindingContext = value; }
        }

        private List<Hotels> ListHotel = new List<Hotels>();

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                if (ViewModel.Items.Count == 0)
                {
                    ViewModel.listDates1 = await SetListDates();
               
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
            this.ViewModel = viewModel;           
        }

        public async Task<List<ListDate>> SetListDates()
        {
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            //await SaveItemAsync();
            var allTodo = await database.GetItemsAsync();
            Console.WriteLine("allTodo: " + allTodo.Count);
            List<ListDate> listDates = new List<ListDate>();

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
                        listDates[k].Name = allTodo[i].Name;
                        listDates[k].IsVisible = false;
                        listDates[k].Rooms.Add(allTodo[i]);
                        isEquals = true;
                    }
                }

                if (!isEquals)
                {
                    Console.WriteLine("!isEquals: " + i);
                    listDates.Add(new ListDate()
                    {
                        date = allTodo[i].myDate,
                        time = allTodo[i].myTime
                    });

                    Console.WriteLine("added");
                    listDates[listDates.Count - 1].Name = allTodo[i].Name;
                    listDates[listDates.Count - 1].IsVisible = false;
                    listDates[listDates.Count - 1].Rooms.Add(allTodo[i]);
                }
            }

            Console.WriteLine("listDates: " + listDates.Count);
            return listDates;
        }

        public async Task SaveItemAsync()
        {
            TodoItem item = new TodoItem()
            {
                ID = 0,
                IsChecked = false,
                Name = "test",
                Notes = "test note",
                HasText = true,
                myDate = new DateTime(2015,03,15),
                myTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
        };

            TodoItemDatabase database = await TodoItemDatabase.Instance;
            await database.SaveItemAsync(item);
        }
    }

    public class ListDate
    {
        public DateTime date;
        public TimeSpan time;

        public List<TodoItem> Rooms = new List<TodoItem>();

        public string Name { get; set; }
        public bool IsVisible { get; set; } = false;

        public ListDate()
        {
        }

        public ListDate(string name, List<TodoItem> rooms)
        {
            Name = name;
            Rooms = rooms;
        }
    }

    public class TodoItem
    {
        public TodoItem()
        {

        }

        public TodoItem(string name, int typeID)
        {
            Name = name;
            ID = typeID;
        }

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
