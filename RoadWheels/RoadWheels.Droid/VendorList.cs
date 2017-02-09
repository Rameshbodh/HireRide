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
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;

namespace RoadWheels.Droid
{
    [Activity(Label = "VendorList")]
    public class VendorList : AppCompatActivity
    {
        RecyclerView vendorListRecycle;
        RecyclerView.LayoutManager layoutManager;
        List<int> vendorImage = new List<int>();
        List<string> vendorName = new List<string>();
        List<string> vendorLocation = new List<string>();
        List<string> vendorPrice = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vendor_list);
            // Create your application here
            SupportActionBar.Title = "Ride near you";
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);
            vendorImage.Add(Resource.Raw.vendor);

            vendorName.Add("Ashish Kumar Rena");
            vendorName.Add("Gurmeet singh");
            vendorName.Add("Kritika sharma");
            vendorName.Add("Reema Handa");
            vendorName.Add("Shivanshu Kumar Anand");
            vendorName.Add("Subah kesar");
            vendorName.Add("Sohrabh Malik");
            vendorName.Add("Jagjeet Singh yadav");
            vendorName.Add("Manohar Lal");
            vendorName.Add("Pandey Dharam Rajan");

            vendorLocation.Add("Dehradun");
            vendorLocation.Add("Kullu");
            vendorLocation.Add("Dehradun");            
            vendorLocation.Add("Chandigarh");
            vendorLocation.Add("Manali");
            vendorLocation.Add("Dehradun");
            vendorLocation.Add("Dehli");
            vendorLocation.Add("Banglore");
            vendorLocation.Add("Dehradun");
            vendorLocation.Add("Dehradun");

            vendorPrice.Add("200");
            vendorPrice.Add("400");
            vendorPrice.Add("300");
            vendorPrice.Add("200");
            vendorPrice.Add("500");
            vendorPrice.Add("600");
            vendorPrice.Add("900");
            vendorPrice.Add("100");
            vendorPrice.Add("400");
            vendorPrice.Add("200");

            Common.vendorImages = vendorImage;
            Common.vendorName = vendorName;
            Common.vendorLocation = vendorLocation;
            Common.vendorPrice = vendorPrice;

            vendorListRecycle = FindViewById<RecyclerView>(Resource.Id.vendorListRecylceView);
            
            layoutManager = new LinearLayoutManager(this);
            vendorListRecycle.SetLayoutManager(layoutManager);
            VendorListAdpter adapter = new VendorListAdpter(this, vendorImage, vendorName, vendorLocation, vendorPrice);

            vendorListRecycle.SetAdapter(adapter);

            adapter.ItemClick += Adapter_ItemClick;

           
        }

        private void Adapter_ItemClick(object sender, int e)
        {
            Common.selectedVendorName = vendorName[e];
            Common.selectedVendorLocation = "Location  " + vendorLocation[e];
            Common.selectedVendorRate = "Rate   " + vendorPrice[e]+"    /hour";


                StartActivity(typeof(VendorDetails));

           // Toast.MakeText(this, "" + name, ToastLength.Long).Show();
        }

        public class VendorListAdpter : RecyclerView.Adapter
        {

            Context context;
            List<int> Vendorimages; List<string> VendorName;List<string> VendorLocation;List<string> VendorPrice;
            public event EventHandler<int> ItemClick;
            public VendorListAdpter(Context context, List<int> Vendorimages, List<string> VendorName, List<string> VendorLocation, List<string> VendorPrice)
            {
                this.context = context;
                this.Vendorimages = Vendorimages;
                this.VendorName = VendorName;
                this.VendorLocation = VendorLocation;
                this.VendorPrice = VendorPrice;
            }


            public override int ItemCount
            {
                get
                {
                    return VendorName.Count;
                }
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                VendorListHolder vh = holder as VendorListHolder;
                vh.vendorImage.SetImageResource(Vendorimages[position]);
                vh.vendorLocation.Text = "location : " + VendorLocation[position];
                vh.vendorName.Text = VendorName[position];
                vh.vendorRate.Text = "Rate :  "+VendorPrice[position]+"     /hour";
            }

            void OnClick(int position)
            {
                if (ItemClick != null)
                    ItemClick(this, position);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.single_VendorList_Item, parent, false);
                return new VendorListHolder(itemView,OnClick);

            }
        }
        public class VendorListHolder : RecyclerView.ViewHolder
        {
            public ImageView vendorImage { get; set; }
            public TextView vendorName { get; set; }
            public TextView vendorLocation { get; set; }
            public TextView vendorRate     {get; set;}
            public VendorListHolder(View itemView ,Action<int> listner):base(itemView)
            {
                vendorImage = itemView.FindViewById<ImageView>(Resource.Id.vendorImage);
                vendorName = itemView.FindViewById<TextView>(Resource.Id.vendorName);
                vendorLocation = itemView.FindViewById<TextView>(Resource.Id.vendorLocation);
                vendorRate = itemView.FindViewById<TextView>(Resource.Id.vendorPrice);
                itemView.Click += (s, e) => listner(AdapterPosition);
            }

            
        }
    }
}