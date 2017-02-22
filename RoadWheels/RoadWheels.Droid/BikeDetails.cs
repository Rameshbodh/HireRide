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
using Android.Support.V7.App;

namespace RoadWheels.Droid
{
    [Activity(Label = "BikeDetails")]
    public class BikeDetails : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.Title = "BIKE INFO";
            // Create your application here
            SetContentView(Resource.Layout.bike_details);

            TextView view;
            view = FindViewById<TextView>(Resource.Id.bdbrand);
            view.Text = Common.selectedBikeName;
            view = FindViewById<TextView>(Resource.Id.bdbookingperday);
            view.Text = Common.selectedBikeRatePerDay;
            view = FindViewById<TextView>(Resource.Id.bdrentprhour);
            view.Text = Common.selectedBikeRatePerHour;
        }
    }
}