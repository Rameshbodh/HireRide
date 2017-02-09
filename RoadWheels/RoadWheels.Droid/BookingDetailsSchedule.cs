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
using Android.Graphics;

namespace RoadWheels.Droid
{
    [Activity(Label = "BookingDetailsSchedule")]
    public class BookingDetailsSchedule : AppCompatActivity
    {
        TextView pickUpDate, dropOffDate, pickUpTime, dropOffTime;
        EditText origin, destination;Button getRideBTN;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.booking_details_schedule);
            SupportActionBar.Title = "Booking Detials";
            origin = FindViewById<EditText>(Resource.Id.bdsOrigin);
            destination = FindViewById<EditText>(Resource.Id.bdsDestination);
            getRideBTN = FindViewById<Button>(Resource.Id.bdsGetRideBTN);
            pickUpDate = FindViewById<TextView>(Resource.Id.bdsPickUpDate);
            pickUpTime = FindViewById<TextView>(Resource.Id.bdsPickUpTime);
            dropOffDate = FindViewById<TextView>(Resource.Id.bdsDropOffDate);
            dropOffTime = FindViewById<TextView>(Resource.Id.bdsDropOffTime);

            pickUpTime.Click += PickUpTime_Click;

            dropOffTime.Click += DropOffTime_Click;

            pickUpDate.Click += PickUpDate_Click;

            dropOffDate.Click += DropOffDate_Click;


            getRideBTN.Click += GetRideBTN_Click;
        }

        private void GetRideBTN_Click(object sender, EventArgs e)
        {
            int count = 0;
            if(pickUpTime.Text=="Drop off Time")
            {
                count++;
            }
            if(count==0)
            {

            }
            StartActivity(typeof(BikeList));
        }

        private void DropOffTime_Click(object sender, EventArgs e)
        {
            TimeViewDialog frag = TimeViewDialog.NewInstance(delegate (TimeSpan time)
            {
                dropOffTime.Text = time.ToString();
                dropOffTime.SetTextColor(Color.ParseColor("#000000"));
            });
            frag.Show(FragmentManager, TimeViewDialog.TAG);
        }

        private void PickUpTime_Click(object sender, EventArgs e)
        {
            // string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            // pickUpTime.Text = time;
            //TimeViewDialog frag = new TimeViewDialog();
            TimeViewDialog frag = TimeViewDialog.NewInstance(delegate (TimeSpan time)
            {
                pickUpTime.Text = time.ToString();
                pickUpTime.SetTextColor(Color.ParseColor("#000000"));
            });
            frag.Show(FragmentManager,TimeViewDialog.TAG);
            
        }

       

        private void DropOffDate_Click(object sender, EventArgs e)
        {
            DateViewDialog frag = DateViewDialog.NewInstance(delegate (DateTime time)
            {
                pickUpDate.Text = time.Date.ToString("dd-MMM-yyyy");
                pickUpDate.SetTextColor(Color.ParseColor("#000000"));
                //   dropOffDate = time.Date;
                //dob.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DateViewDialog.TAG);
        }

        private void PickUpDate_Click(object sender, EventArgs e)
        {
            DateViewDialog frag = DateViewDialog.NewInstance(delegate (DateTime time)
            {
                pickUpDate.Text = time.Date.ToString("dd-MMM-yyyy");
                pickUpDate.SetTextColor(Color.ParseColor("#000000"));
                //   dropOffDate = time.Date;
                //dob.Text = time.ToLongDateString();
            });
            frag.Show(FragmentManager, DateViewDialog.TAG);
        }
    }
}