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
using Android.Support.Design.Widget;

namespace RoadWheels.Droid
{
    [Activity(Label = "VendorDetails")]
    public class VendorDetails : AppCompatActivity
    {
        private BottomSheetBehavior vendorDetailsSheet;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vendor_detail);

            View bottomSheet = FindViewById<View>(Resource.Id.bottom_sheet);
            // Create your application here
            FindViewById<TextView>(Resource.Id.vendorName).Text = Common.selectedVendorName;
            FindViewById<TextView>(Resource.Id.vendorLocation).Text = Common.selectedVendorLocation;
            FindViewById<TextView>(Resource.Id.vendorPrice).Text = Common.selectedVendorRate;


            vendorDetailsSheet = BottomSheetBehavior.From(bottomSheet);
            vendorDetailsSheet.PeekHeight = 400;

        }
    }
}