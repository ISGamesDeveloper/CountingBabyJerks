using System;
using System.Collections.Generic;
using System.Text;
using ListViewWithSubListView.Views;

namespace ListViewWithSubListView.Models
{
    public class Hotel
    {
        public string Name { get; set; }

        public List<TodoItem> Rooms { get; set; }

        public bool IsVisible { get; set; } = false;

        public Hotel()
        {
        }

        public Hotel(string  name, List<TodoItem> rooms)
        {
            Name = name;
            Rooms = rooms;
        }
    }
}
