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

namespace RoadWheels.Droid
{
    class Common
    {
        public static List<int> vendorImages { get; set; }
        public static List<string> vendorName { get; set; }
        public static List<string> vendorLocation { get; set; }
        public static List<string> vendorPrice { get; set; }

        public static string selectedVendorName { get; set; }
        public static string selectedVendorLocation { get; set; }

        public static string selectedVendorRate { get; set; }


        public static bool SaveUserLocationLocality(Context context, string locationName, string locationValue)
        {


            var prefs = context.GetSharedPreferences("RoadWheels", FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString(locationName, locationValue);
            prefEditor.Commit();
            return true;
        }

        public static string GetUserLocationLocality(Context context, string locationName)
        {
            var prefs = context.GetSharedPreferences("RoadWheels", FileCreationMode.Private);
            var value = prefs.GetString(locationName, "");

            return "";
        }

    }
}