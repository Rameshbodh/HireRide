using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Support.V7.App;

namespace RoadWheels.Droid
{
    class RoadWheelsCustomAdapter : BaseAdapter
    {
        private AppCompatActivity context;
        private List<int> imagesList;
        private List<string> nameList;

      public RoadWheelsCustomAdapter(AppCompatActivity context,List<int> imagesList,List<string> nameList)
        {
            this.context = context;
            this.imagesList = imagesList;
            this.nameList = nameList;
        }

        public override int Count
        {
            get
            {
                return imagesList.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
    

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if(view==null)
                view = this.context.LayoutInflater.Inflate(Resource.Layout.single_item_book_ride_type, null);
           ImageView image = view.FindViewById<ImageView>(Resource.Id.sibrtImage);
            image.SetImageResource(imagesList[position]);
          TextView text=  view.FindViewById<TextView>(Resource.Id.sibrtText);
            text.Text = nameList[position];
            return view;
        }
    }
}