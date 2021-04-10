using ListViewWithSubListView.Models;
using ListViewWithSubListView.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static ListViewWithSubListView.Views.Hotels;

namespace ListViewWithSubListView.ViewModels
{
    public class HotelsGroupViewModel : BaseViewModel
    {
        private HotelViewModel _oldHotel;
        public List<ListDate> listDates1 = new List<ListDate>();

        private ObservableCollection<HotelViewModel> items;
        public ObservableCollection<HotelViewModel> Items
        {
            get => items;

            set => SetProperty(ref items, value);
        }
      
        public Command LoadHotelsCommand { get; set; }
        public Command<HotelViewModel> RefreshItemsCommand { get; set; }

        public HotelsGroupViewModel()
        {
            Console.WriteLine("HotelsGroupViewModel");
            items = new ObservableCollection<HotelViewModel>();
            Items = new ObservableCollection<HotelViewModel>();
            LoadHotelsCommand = new Command(async () => await ExecuteLoadItemsCommandAsync());
            RefreshItemsCommand = new Command<HotelViewModel>((item) => ExecuteRefreshItemsCommand(item));
        }
      
        public bool isExpanded = false;
        private void ExecuteRefreshItemsCommand(HotelViewModel item)
        {
            if (_oldHotel == item)
            {
                // click twice on the same item will hide it
                item.Expanded = !item.Expanded;
            }
            else
            {
                if (_oldHotel != null)
                {
                    // hide previous selected item
                    _oldHotel.Expanded = false;
                }
                // show selected item
                item.Expanded = true;
            }

            _oldHotel = item;
        }
        async System.Threading.Tasks.Task ExecuteLoadItemsCommandAsync()
        {
            Console.WriteLine("listDates: " + listDates1.Count);
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                Items.Clear();



                /*
                List<TodoItem> Hotel1rooms = new List<TodoItem>() { new TodoItem("Jasmine", 0), new TodoItem("Flower Suite", 2), new TodoItem("narcissus", 1)
                };
                List<TodoItem> Hotel2rooms = new List<TodoItem>()
                {
                    new TodoItem("Princess", 1), new TodoItem("Royale", 1), new TodoItem("Queen", 1)
                };
                List<TodoItem> Hotel3rooms = new List<TodoItem>()
                {
                    new TodoItem("Marhaba", 1), new TodoItem("Marhaba Salem", 1), new TodoItem("Salem Royal", 1), new TodoItem("Wedding Roome", 1), new TodoItem("Wedding Suite", 2)
                };
                List<ListDate> items = new List<ListDate>() { new ListDate("Yasmine Hammamet", Hotel1rooms), new ListDate("El Mouradi Hammamet,", Hotel2rooms), new ListDate("Marhaba Royal Salem", Hotel3rooms) };
                */
     

                for (int i = 0; i < listDates1.Count; i++)
                {
                    Items.Add(new HotelViewModel(listDates1[i]));
                }

                
               // if (items != null && items.Count > 0)
                //{
                  //  foreach (var hotel in items)
                    //    Items.Add(new HotelViewModel(hotel));
                //}
                //else { IsEmpty = true; }
                

            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
