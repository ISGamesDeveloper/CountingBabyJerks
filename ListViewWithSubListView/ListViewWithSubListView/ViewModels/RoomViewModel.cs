using ListViewWithSubListView.Models;
using ListViewWithSubListView.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ListViewWithSubListView.ViewModels
{
    public class RoomViewModel 
    {
        private TodoItem _room;

        public RoomViewModel(TodoItem room)
        {
            this._room = room;
        }

        public string RoomName { get { return _room.myDate.ToString() + "  " + _room.myTime.ToString(); } }
        public int TypeID { get { return _room.ID; } }
        
        public TodoItem Room
        {
            get => _room;
        }

    }
}
