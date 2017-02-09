using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Collections.Generic;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Support.V4.Content;
using BottomNavigationBar;
using Android;
using Android.Locations;
using System.Threading.Tasks;
using Android.Content.PM;
using Geolocator.Plugin;
using System.Linq;
using System.Text;
using Android.Support.V4.Widget;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
namespace RoadWheels.Droid
{
	[Activity (Label = "RoadWheels.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme.NoActionBar")]
	public class HomePage : AppCompatActivity,ViewPager.IOnPageChangeListener, BottomNavigationBar.Listeners.IOnMenuTabClickListener
    {
        DrawerLayout drawerLayout;
        TextView UserName;
        ImageView UserImage;
        Toolbar toolbar;
        const int RequestLocationId = 0;
        readonly string[] PermissionsLocation =
               {
                  Manifest.Permission.AccessCoarseLocation,
                  Manifest.Permission.AccessFineLocation
                };

        

        private BottomBar _bottomNavBar;
        LinearLayout dotslayout;
        private RecyclerView homeRView;
        private ViewPager imagePager;
        private RecyclerView.LayoutManager _layoutManager;
        List<int> imageList = new List<int>();
        List<int> iconList = new List<int>();
        string[] iconNameList = new string[] {"Book Ride","Grab Service","Tips","Dealer near you","Gallery","Todo list" };
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Enable location setting
            

            InternetConnectionState checkNet = new InternetConnectionState();

            string val = checkNet.isConnectedToNetwork();
            if (val != "not connected")
            {
               
                
                

                IntializeUI(bundle);
            }
            else
            {
                
                SetContentView(Resource.Layout.network_failure_message);
            }

           
        }
        private MyProfileTracker mProfileTracker;
        private void IntializeUI(Bundle savestate)
        {
            EnableLocationDialog();

            FacebookSdk.SdkInitialize(this.ApplicationContext);

            mProfileTracker = new MyProfileTracker();

            SetContentView(Resource.Layout.home_page);
            
            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "HOME";

            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_views);

            View headerview = navigationView.InflateHeaderView(Resource.Layout.drawer_header);

            UserName = headerview.FindViewById<TextView>(Resource.Id.userName);

            UserImage = headerview.FindViewById<ImageView>(Resource.Id.userImage);

            

            ISharedPreferences prefs = this.GetSharedPreferences("MYPROFILE", FileCreationMode.Private);

           if( prefs.GetString("username","")!=null && prefs.GetString("profilePic", "")!=null)
            {
                UserImage.Visibility = ViewStates.Gone;
                ProfilePictureView fbProfilePicture = headerview.FindViewById<ProfilePictureView>(Resource.Id.facebook_profile_pic);
                fbProfilePicture.Visibility = ViewStates.Visible;
                string name = prefs.GetString("username","");
                string profilePic = prefs.GetString("profilepic","");

                UserName.Text = name;
                fbProfilePicture.ProfileId = profilePic;



            }


            TextView userLocationView = headerview.FindViewById<TextView>(Resource.Id.userLocation);


            userLocationView.Click += UserLocationView_Click;




            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            var drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, toolbar, Resource.String.drawer_open, Resource.String.drawer_close);

            drawerLayout.AddDrawerListener(drawerToggle);


            drawerToggle.SyncState();
            //SupportActionBar.Show();

            // FindViewById<Button>(Resource.Id.button1).Click += HomePage_Click;
            iconList.Add(Resource.Raw.ic_bike);
            iconList.Add(Resource.Raw.ic_service);
            iconList.Add(Resource.Raw.ic_tip);
            iconList.Add(Resource.Raw.ic_location);
            iconList.Add(Resource.Raw.ic_bike);
            iconList.Add(Resource.Raw.ic_todo);

            dotslayout = FindViewById<LinearLayout>(Resource.Id.HomePageDotsContainer);
            imagePager = FindViewById<ViewPager>(Resource.Id.homePageImagePagerContainer);
            homeRView = FindViewById<RecyclerView>(Resource.Id.homePageRView);
            _layoutManager = new GridLayoutManager(this, 3);
            homeRView.SetLayoutManager(_layoutManager);

            RecycleHomeIconAdapter RVadapter = new RecycleHomeIconAdapter(this, iconList, iconNameList);

            homeRView.SetAdapter(RVadapter);


            RVadapter.ItemClick += RVadapter_ItemClick;



            imageList.Add(Resource.Raw.image1);
            imageList.Add(Resource.Raw.image4);
            imageList.Add(Resource.Raw.image2);

            PageImageAdapter adapter = new PageImageAdapter(this, imageList);

            imagePager.Adapter = adapter;

            imagePager.SetCurrentItem(0, true);

            imagePager.AddOnPageChangeListener(this);

            

            setUiPageViewController();


            Bundle savestates = new Bundle();


            // _bottomNavBar = BottomBar.Attach(this, bundle);
            _bottomNavBar = BottomBar.AttachShy((CoordinatorLayout)FindViewById(Resource.Id.homePageCoordinatorLayout), FindViewById(Resource.Id.homePageScrollingContent), savestates);
            _bottomNavBar.SetItems(Resource.Menu.bottomMenuBar);
            _bottomNavBar.SetOnMenuTabClickListener(this);

           


            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
        }

        private void MProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //drawer layout location click
        private void UserLocationView_Click(object sender, EventArgs e)
        {
           if(string.IsNullOrEmpty(Common.GetUserLocationLocality(this,"locationName")))
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetTitle("location");
                alert.SetMessage("Location");
                alert.Create();
                alert.Show();
            }
            else
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetTitle("verify location");
                alert.SetMessage(""+Common.GetUserLocationLocality(this, "locationName"));
                alert.Create();
                alert.Show();
            }
        }
        //navigation menu item click
        private void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            drawerLayout.CloseDrawers();
        }


        //display dot in viewpager slider images
        ImageView[] dots;
        int dotsCount;
        private void setUiPageViewController()
        {

            dotsCount = imageList.Count;



            dots = new ImageView[dotsCount];

            for (int i = 0; i < dotsCount; i++)
            {
                dots[i] = new ImageView(this);
                dots[i].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.unselectedDot));


                LinearLayout.LayoutParams p = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                p.Gravity = GravityFlags.Center;
                p.SetMargins(5, 0, 5, 0);



                dotslayout.AddView(dots[i], p);

            }
            dots[0].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.selectedDot));

            //dots[0].setImageDrawable(getResources().getDrawable(R.drawable.selecteditem_dot));
        }

        ListView RideTypeLV; Android.Support.V7.App.AlertDialog dialog;List<string> typeRideList;List<int> typeRideImage;
        //RecyclerView gridView item click
        private void RVadapter_ItemClick(object sender, int e)
        {
            Toast.MakeText(this, "" + iconNameList[e], ToastLength.Long).Show();
            if(iconNameList[e]== "Dealer near you")
            {
                StartActivity(typeof(VendorList));
            }
            if(iconNameList[e]== "Book Ride")
            {
                typeRideList = new List<string>();
                typeRideImage = new List<int>();
                typeRideList.Add("Intant Ride");
                typeRideList.Add("Schedule Ride");
                typeRideImage.Add(Resource.Raw.ic_today);
                typeRideImage.Add(Resource.Raw.ic_planner);

                RoadWheelsCustomAdapter adapter = new RoadWheelsCustomAdapter(this, typeRideImage, typeRideList);


                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                LayoutInflater inflater = LayoutInflater;
                View dialogLayout = inflater.Inflate(Resource.Layout.custom_alert_book_ride, null);
                RideTypeLV = dialogLayout.FindViewById<ListView>(Resource.Id.cabrSelectType);
                RideTypeLV.Adapter = adapter;

                RideTypeLV.ItemClick += RideTypeLV_ItemClick;

                alert.SetView(dialogLayout);
                dialog = alert.Create();
                dialog.Show();

            }
        }

        private void RideTypeLV_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            dialog.Dismiss();
            if(e.Position==0)
            {

            }
            if (e.Position == 1)
            {
                StartActivity(typeof(BookingDetailsSchedule));
            }
        }

        public void OnPageScrollStateChanged(int state)
        {
            
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
          
        }
        //view pager item click
        public void OnPageSelected(int position)
        {
            for (int i = 0; i < dotsCount; i++)
            {
                dots[i].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.unselectedDot));
            }

            dots[position].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.selectedDot));
        }


        //bottom navigation bar menu item click
        public void OnMenuTabSelected(int menuItemId)
        {
            switch (menuItemId)
            {
                case Resource.Id.bottomBarhome:
                    //Toast.MakeText(this, "Home", ToastLength.Long).Show();
                    SupportActionBar.Title = "HOME";
                    FindViewById<FrameLayout>(Resource.Id.homeFrameContainer).Visibility = ViewStates.Gone;
                    FindViewById<ViewPager>(Resource.Id.homePageImagePagerContainer).Visibility = ViewStates.Visible;
                    FindViewById<LinearLayout>(Resource.Id.HomePageDotsContainer).Visibility = ViewStates.Visible;
                    FindViewById<RecyclerView>(Resource.Id.homePageRView).Visibility = ViewStates.Visible;
                    break;

                case Resource.Id.bottomBarProfile:
                    // Toast.MakeText(this, "Profile", ToastLength.Long).Show();
                    FindViewById<ViewPager>(Resource.Id.homePageImagePagerContainer).Visibility = ViewStates.Gone;
                    FindViewById<LinearLayout>(Resource.Id.HomePageDotsContainer).Visibility = ViewStates.Gone;
                    FindViewById<RecyclerView>(Resource.Id.homePageRView).Visibility = ViewStates.Gone;
                    SupportActionBar.Title = "Profile settings";

                    Profile profileFragment = new Profile();
                    FindViewById<FrameLayout>(Resource.Id.homeFrameContainer).Visibility = ViewStates.Visible;
                    FindViewById<ViewPager>(Resource.Id.homePageImagePagerContainer).Visibility = ViewStates.Gone;

                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.homeFrameContainer, profileFragment).Commit();

                    break;
                case Resource.Id.bottomBarNotification:
                    // Toast.MakeText(this, "Notification", ToastLength.Long).Show();
                    SupportActionBar.Title = "Notification";
                    FindViewById<ViewPager>(Resource.Id.homePageImagePagerContainer).Visibility = ViewStates.Gone;
                    FindViewById<LinearLayout>(Resource.Id.HomePageDotsContainer).Visibility = ViewStates.Gone;
                    FindViewById<RecyclerView>(Resource.Id.homePageRView).Visibility = ViewStates.Gone;
                    FindViewById<FrameLayout>(Resource.Id.homeFrameContainer).Visibility = ViewStates.Visible;
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.homeFrameContainer, new Notification()).Commit();
                    break;
            }
        }

        public void OnMenuTabReSelected(int menuItemId)
        {
           
        }
        //check for location service enable or not
        private void EnableLocationDialog()
        {
            LocationManager lm = (LocationManager)GetSystemService(LocationService);
            if (!lm.IsProviderEnabled(LocationManager.GpsProvider) || !lm.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Location Service Disabled");
                alert.SetMessage("Please enable location services");

                alert.SetPositiveButton("ENABLE", (senderAlert, args) =>
                {
                    alert.Dispose();
                    Intent intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                    StartActivity(intent);
                    //TryGetLocationAsync();


                });
                alert.Create();
                alert.Show();

            }
            else
            {
                TryGetLocationAsync();
            }
        }

        protected override void OnRestart()
        {
            base.OnRestart();

            //location settings enable
            EnableLocationDialog();
        }



        async void TryGetLocationAsync()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                await GetLocationAsync();
                return;
            }


            await GetLocationPermissionAsync();
        }
        private async Task GetLocationPermissionAsync()
        {
            //Check to see if any permission in our group is available, if one, then all are
            const string permission = Manifest.Permission.AccessFineLocation;
            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                await GetLocationAsync();
                return;



            }
            if (ShouldShowRequestPermissionRationale(permission))
            {
                //Explain to the user why we need to read the contacts
                RequestPermissions(PermissionsLocation, RequestLocationId);
                return;
            }

            //Finally request permissions with the list of permissions and Id
            RequestPermissions(PermissionsLocation, RequestLocationId);
        }

        public async Task GetLocationAsync()
        {
            // textLocation.Text = "Getting Location";
            try
            {

                var locator = CrossGeolocator.Current;

                locator.DesiredAccuracy = 100;

                var position = await locator.GetPositionAsync(200000);

                //latitude = string.Format("{0:f6}", position.Latitude);

                //longitude = string.Format("{0:f6}", position.Longitude);
         double latitudes = Convert.ToDouble(position.Latitude);

         double longitudes = Convert.ToDouble(position.Longitude);

                Toast.MakeText(this, "lat:  " + latitudes + "  long "+ longitudes, ToastLength.Long).Show();

                
                Address address = await ReverseGeocodeCurrentLocation(latitudes, longitudes);

                //user current locality
                 string locality =  address.Locality;

                DisplayAddress(address);
                //   Geocoder geocoder = new Geocoder(this);


                //   IList<Address> addresses = geocoder.GetFromLocation(latitudes, longitudes, 1);


                ////   string city = addresses[0].Locality.ToString();
                //   string state = addresses[0].AdminArea.ToString();
                //   string zip = addresses[0].PostalCode.ToString();
                //   string country = addresses[0].CountryName.ToString();
                //   string city = addresses[0].Locality.ToString();

                //   //Toast.MakeText(this, "area  "+a, ToastLength.Long).Show();

                //   //IoonyApp.Common.latitudes = latitude;
                //   //IoonyApp.Common.longitudes = longitude;
                //   AddressUser(city,state,zip,country);




            }
            catch (Exception ex)
            {

                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert
                  .SetTitle("ooops snap... ")
                    .SetMessage("Grab a bottle of BEER...")
                    .SetPositiveButton("Ok", (s, e) =>
                    {

                    });
                alert.Create();
                alert.Show();
                Toast.MakeText(this, ""+ex.Message, ToastLength.Long).Show();
                //textLocation.Text = "Unable to get location: " + ex.ToString();
            }
        }


        //for displaying the user current address
        private void DisplayAddress(Address address)
        {
            string a;
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                a = deviceAddress.ToString();
            }
            else
            {
                a = "Unable to determine the address. Try again in a few minutes.";
            }

           

            ISharedPreferences prefs = this.GetSharedPreferences("MyPrefss", 0);
            if (!prefs.GetBoolean("firstTime", false))
            {
                // <---- run your one time code here



            if(Common.SaveUserLocationLocality(this, "locationName", a))
                {

                  
                    Toast.MakeText(this, "OK", ToastLength.Long).Show();
                }

                //Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                //alert.SetTitle("Verify Location ")
                //    .SetMessage("address : " + a)
                //    .SetPositiveButton("Ok", (s, e) =>
                //    {

                //    });
                //alert.Create();
                //alert.Show();
                // mark first time has runned.
                //ISharedPreferences.Editor editor = prefs.edit();
                prefs.Edit().PutBoolean("firstTime", true).Commit();

            }





        }

      




        private async Task<Address> ReverseGeocodeCurrentLocation(double latitudes, double longitudes)
        {
            Geocoder geocoder = new Geocoder(this);


            IList<Address> addresses = await geocoder.GetFromLocationAsync(latitudes, longitudes, 20000);

            Address address = addresses.FirstOrDefault();
            return address;
            ////   string city = addresses[0].Locality.ToString();
            //   string state = addresses[0].AdminArea.ToString();
            //   string zip = addresses[0].PostalCode.ToString();
            //   string country = addresses[0].CountryName.ToString();
            //   string city = addresses[0].Locality.ToString();

            //   //Toast.MakeText(this, "area  "+a, ToastLength.Long).Show();

            //   //IoonyApp.Common.latitudes = latitude;
            //   //IoonyApp.Common.longitudes = longitude;
            //   AddressUser(city,state,zip,country);
        }

        private void AddressUser(string city,string state, string zip, string country)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            alert.SetTitle("Verify Location ")
                .SetMessage("city "+city+" State :  " + state + " Pin - Code : " + zip + "  Country : " + country + "   ")
                .SetPositiveButton("Ok", (s, e) =>
                {

                });
            alert.Create();
            alert.Show();
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            Toast.MakeText(this, "Location has been granted", ToastLength.Long).Show();

                            await GetLocationAsync();
                        }
                        else
                        {
                            //Permission Denied :(
                            //Disabling location functionality
                            Toast.MakeText(this, "Location permission is  denied", ToastLength.Long).Show();

                        }
                    }
                    break;
            }
        }







        class RecycleHomeIconAdapter : RecyclerView.Adapter
        {
            Context context;
            List<int> iconList;
            string[] iconNameList;
            public event EventHandler<int> ItemClick;
            LayoutInflater inflater;
            public RecycleHomeIconAdapter(Context context, List<int> iconList, string[] iconNameList)
            {
                this.context = context;
                this.iconList = iconList;
                this.iconNameList = iconNameList;
                inflater = LayoutInflater.From(this.context);
            }

            public override int ItemCount
            {
                get
                {
                    return iconNameList.Length;
                }
            }

            public void OnClick(int position)
            {
                if (ItemClick != null)
                    ItemClick(this, position);

            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                IconViewHolder vh = holder as IconViewHolder;

                vh.icon.SetImageResource(iconList[position]);
                vh.iconName.Text = iconNameList[position];


            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = inflater.Inflate(Resource.Layout.single_grid_home_page_item, parent, false);
                IconViewHolder vh = new IconViewHolder(itemView, OnClick);
                return vh;
            }

            public class IconViewHolder : RecyclerView.ViewHolder
            {
                public ImageView icon { get; set; }
                public TextView iconName { get; set; }
                public IconViewHolder(View itemView, Action<int> Listner) : base(itemView)
                {
                    icon = itemView.FindViewById<ImageView>(Resource.Id.my_image_view);
                    iconName = itemView.FindViewById<TextView>(Resource.Id.my_text_view);

                    itemView.Click += (sender, e) => Listner(AdapterPosition);
                }
            }
        }





    }
}


