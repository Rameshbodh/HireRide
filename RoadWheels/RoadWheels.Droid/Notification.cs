using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Fragment = Android.Support.V4.App.Fragment;
using Android.Support.V7.Widget;

namespace RoadWheels.Droid
{
    public class Notification : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
        RecyclerView notificationListRecycle;
        RecyclerView.LayoutManager _layoutManager;
        List<int> notificationImage = new List<int>();
        List<string> notificationTitle = new List<string>();
        List<string> notificationDate = new List<string>();
        List<string> notificationDiscription = new List<string>();
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
          View itemView= inflater.Inflate(Resource.Layout.user_notification, container, false);

            notificationListRecycle = itemView.FindViewById<RecyclerView>(Resource.Id.notificationListRecylceView);

            _layoutManager = new LinearLayoutManager(this.Context);

            notificationListRecycle.SetLayoutManager(_layoutManager);

            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);
            notificationImage.Add(Resource.Raw.vendor);

            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");
            notificationDate.Add("12-jan-2017");

            notificationTitle.Add("Get Free Ride");
            notificationTitle.Add("Get Discount with 5 people");
            notificationTitle.Add("Get Free Ride");
            notificationTitle.Add("Get Discount with 5 people");
            notificationTitle.Add("Get Free Ride");
            notificationTitle.Add("Get Discount with 5 people");
            notificationTitle.Add("Get Free Ride");
            notificationTitle.Add("Get Discount with 5 people");
            notificationTitle.Add("Get Free Ride");
            notificationTitle.Add("Get Discount with 5 people");
            notificationTitle.Add("Get Discount with 5 people");

            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");
            notificationDiscription.Add("Ride With RoadWheels will make you feel ride in paradise explore more nature is beautifull ");

            NotificationListAdapter adapter = new NotificationListAdapter(this.Context,notificationImage, notificationTitle, notificationDate, notificationDiscription);

            notificationListRecycle.SetAdapter(adapter);

            adapter.ItemClick += Adapter_ItemClick;

            return itemView;
        }

        private void Adapter_ItemClick(object sender, int position)
        {
            //notificationDiscription[position];
        }

        public class NotificationListAdapter : RecyclerView.Adapter
        {
            public event EventHandler<int> ItemClick;
            private List<string> titleList, notificationTitleList, notificationDate, notificationDescription;
            private List<int> notificationImage;
            Context context;

            LayoutInflater inflater;
            public NotificationListAdapter(Context context,List<int> notificationImage,List<string> notificationTitleList,List<string> notificationDate,List<string> notificationDescription)
            {
                this.context = context;
                this.notificationImage = notificationImage;
                this.notificationTitleList = notificationTitleList;
                this.notificationDescription = notificationDescription;
                this.notificationDate = notificationDate;
                inflater= LayoutInflater.From(this.context);
            }

            public override int ItemCount
            {
                get
                {
                    return notificationImage.Count;
                }
            }

            public void Onclick(int position)
            {
                if (ItemClick != null)
                    ItemClick(this, position);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                NotificationListHolder vh = holder as NotificationListHolder;

                vh.notificationImageView.SetImageResource(notificationImage[position]);
                vh.notificationTitle.Text = notificationTitleList[position];
                vh.notificationDate.Text = notificationDate[position];
                vh.notificationDiscription.Text = notificationDescription[position];

            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = inflater.Inflate(Resource.Layout.single_notification_item, parent, false);

                NotificationListHolder view = new  NotificationListHolder(itemView, Onclick);

                return view;

            }


            public class NotificationListHolder : RecyclerView.ViewHolder
            {
                public ImageView notificationImageView { get; set; }
                public TextView notificationTitle { get; set; }

                public TextView notificationDate { get; set; }
                public TextView notificationDiscription { get; set; }
               


                public NotificationListHolder(View itemView, Action<int> listner):base(itemView)
                {
                    notificationImageView = itemView.FindViewById<ImageView>(Resource.Id.notificationImageView);
                    notificationTitle = itemView.FindViewById<TextView>(Resource.Id.notificationTitle);
                    notificationDate = itemView.FindViewById<TextView>(Resource.Id.notificationDate);
                    notificationDiscription = itemView.FindViewById<TextView>(Resource.Id.notificationDiscription);


                    itemView.Click += (sender, e) => listner(AdapterPosition);
                }

            }
        }


    }
}