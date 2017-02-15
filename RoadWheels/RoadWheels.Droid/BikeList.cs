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

namespace RoadWheels.Droid
{
    [Activity(Label = "BikeList")]
    public class BikeList : AppCompatActivity
    {
        List<int> bikeImage= new List<int>();
        List<string> bikeNames=new List<string>();
        List<string> bikeVendor=new List<string>();
        List<string> bikeLocation=new List<string>();
        List<string> bikeRatePerHour=new List<string>();
        List<string> bikeRatePerDay=new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RecyclerView bikeListRV;
            RecyclerView.LayoutManager _LayoutManager;
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.bike_list);
            // Create your application here
            bikeListRV = FindViewById<RecyclerView>(Resource.Id.bikeListRecylceView);

            //_LayoutManager = new GridLayoutManager(this, 2);

            _LayoutManager = new LinearLayoutManager(this);

            bikeListRV.SetLayoutManager(_LayoutManager);

            bikeImage.Add(Resource.Raw.bike1);
            bikeImage.Add(Resource.Raw.bike2);
            bikeImage.Add(Resource.Raw.bike3);
            bikeImage.Add(Resource.Raw.bike4);
            bikeImage.Add(Resource.Raw.bike5);
            bikeImage.Add(Resource.Raw.bike6);
            bikeImage.Add(Resource.Raw.bike7);
            bikeImage.Add(Resource.Raw.bike8);
            bikeImage.Add(Resource.Raw.bike9);
            bikeImage.Add(Resource.Raw.bike10);

     
            bikeNames.Add("Ducati 1098S, 169 miles per hour");
            bikeNames.Add("BMW K 1200S, 174 miles per hour");
            bikeNames.Add("Aprilia RSV 1000R Mille, 175 miles per hour");
            bikeNames.Add("MV Agusta F4 1000R, 184 miles per hour");
            bikeNames.Add("Kawasaki Ninja ZX-14R, 186 miles per hour");
            bikeNames.Add("Yamaha YZF  R1, 186 miles per hour");
            bikeNames.Add("Honda CBR1100XX Super Blackbird, 190 miles per hour");
            bikeNames.Add("Suzuki Hayabusa, 194 miles per hour");
            bikeNames.Add("MTT Turbine Superbike Y2K, 227 miles per hour");
            bikeNames.Add("Kawasaki Ninja H2R, Over 249 miles per hour");



            bikeVendor.Add("Ducati");
            bikeVendor.Add("BMW");
            bikeVendor.Add("Aprilia");
            bikeVendor.Add("MV Agustsa");
            bikeVendor.Add("Kawasaki Ninja");
            bikeVendor.Add("Yamaha");
            bikeVendor.Add("Honda");
            bikeVendor.Add("Suzuki");
            bikeVendor.Add("MTT turbine");
            bikeVendor.Add("Kawasaki ninja");

            bikeLocation.Add("Manali");
            bikeLocation.Add("Chnadigarh");
            bikeLocation.Add("Delhi");
            bikeLocation.Add("Manali");
            bikeLocation.Add("Chnadigarh");
            bikeLocation.Add("Delhi");
            bikeLocation.Add("Manali");
            bikeLocation.Add("Chnadigarh");
            bikeLocation.Add("Delhi");
            bikeLocation.Add("Pune");

            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");
            bikeRatePerHour.Add("500");

            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("20000");
            bikeRatePerDay.Add("30000");

            BikeListAdapter adapter = new BikeListAdapter(this, bikeImage, bikeNames, bikeVendor, bikeLocation, bikeRatePerHour, bikeRatePerDay);

            bikeListRV.SetAdapter(adapter);

        }
    }

    public class BikeListAdapter : RecyclerView.Adapter
    {
        Context context;
        List<int> bikeImage;
        List<string> bikeNames;
        List<string> bikeVendor;
        List<string> bikeLocation;
        List<string> bikeRatePerHour;
        List<string> bikeRatePerDay;

        public BikeListAdapter(Context context,
        List<int> bikeImage,
        List<string> bikeNames,
        List<string> bikeVendor,
        List<string> bikeLocation,
        List<string> bikeRatePerHour,
        List<string> bikeRatePerDay)
        {
            this.context = context;
            this.bikeImage = bikeImage;
            this.bikeNames = bikeNames;
            this.bikeVendor = bikeVendor;
            this.bikeLocation = bikeLocation;
            this.bikeRatePerHour = bikeRatePerHour;
            this.bikeRatePerDay = bikeRatePerDay;
        }

        public event EventHandler<int> ItemClick;
        public override int ItemCount
        {
            get
            {
                return bikeNames.Count;
            }
        }
        
        public void OnClick(int position)
        {
            if(ItemClick!=null)
            {
                ItemClick(this, position);
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BikeListViewHolder vh = holder as BikeListViewHolder;
            vh.bikeImage.SetImageResource(bikeImage[position]);
            vh.bikeName.Text = bikeNames[position];
            vh.bikeVendorName.Text = "by "+bikeVendor[position];
            vh.bikeLocation.Text = bikeLocation[position];
            vh.rentChargersPerDay.Text =this.context.Resources.GetString(Resource.String.Rs)+""+ bikeRatePerDay[position]+"  /day";
            vh.rentChargesPerHour.Text = this.context.Resources.GetString(Resource.String.Rs) + "" + bikeRatePerHour[position]+"  /hour";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.single_item_bike_list_relative, parent, false);
            return new BikeListViewHolder(itemView, OnClick);
        }
       
        public class BikeListViewHolder:RecyclerView.ViewHolder
        {
           public ImageView bikeImage { get; set; }
           public TextView bikeName { get; set; }

           public TextView bikeLocation { get; set; }
          
          public TextView bikeVendorName { get; set; }
          public  TextView rentChargesPerHour { get; set; }
          public  TextView rentChargersPerDay { get; set; }
            public BikeListViewHolder(View itemView,Action<int> listner):base(itemView)
            {
                bikeImage = itemView.FindViewById<ImageView>(Resource.Id.siblImage);
                bikeName = itemView.FindViewById<TextView>(Resource.Id.siblBikeName);
                bikeLocation = itemView.FindViewById<TextView>(Resource.Id.siblBikeLocation);
                bikeVendorName = itemView.FindViewById<TextView>(Resource.Id.siblBikeDealerName);
                rentChargesPerHour = itemView.FindViewById<TextView>(Resource.Id.siblBikeRatePerhour);
                rentChargersPerDay = itemView.FindViewById<TextView>(Resource.Id.siblBikeRatePerDay);
                ItemView.Click+= (s, e) => listner(AdapterPosition);
            }
        }

    }


}