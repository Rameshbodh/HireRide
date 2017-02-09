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
using Android.Net;

namespace RoadWheels.Droid
{
    class InternetConnectionState
    {
        //<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"></uses-permission>
        ConnectivityManager connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

        // connected  to the network
        public string isConnectedToNetwork()
        {
            NetworkInfo mobileInfo = connectivityManager.ActiveNetworkInfo;
           string val="";
               if (mobileInfo != null)
                { // connected to the internet
                    if (mobileInfo.GetType() == (object)ConnectivityType.Wifi)
                    {
                        // connected to wifi
                        val = "wifi";
                        return val;


                        // Toast.makeText(context, activeNetwork.getTypeName(), Toast.LENGTH_SHORT).show();
                    }
                    else if (mobileInfo.GetType() == (object)ConnectivityType.Mobile)
                    {
                        if (mobileInfo.IsRoaming && mobileInfo.IsConnected)
                        {
                            val = "roaming";
                            return val;
                        }
                        else
                        {

                            val = "not roaming";
                            return val;
                        }

                    }

                 
                    
                }
               
            else
            {
                    // not connected to the internet
                    val = "not connected";
                    return val;
             }

            return val;

        }
    }
}