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
    [Activity(Label = "Gallery")]
    public class Gallery : AppCompatActivity
    {
        RecyclerView galleryRV;RecyclerView.LayoutManager _linearLayout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.Title = "GALLERY";
            SetContentView(Resource.Layout.gallery);
            List<int> imageList = new List<int>();
            List<string> nameList = new List<string>();

            imageList.Add(Resource.Raw.view1);
            imageList.Add(Resource.Raw.view2);
            imageList.Add(Resource.Raw.view3);
            imageList.Add(Resource.Raw.view4);
            imageList.Add(Resource.Raw.view1);
            imageList.Add(Resource.Raw.view2);
            imageList.Add(Resource.Raw.view3);
            imageList.Add(Resource.Raw.view4);
            imageList.Add(Resource.Raw.view1);
            imageList.Add(Resource.Raw.view2);

            nameList.Add("Paris");
            nameList.Add("New York");
            nameList.Add("London");
            nameList.Add("Dubai");
            nameList.Add("North Carolina");
            nameList.Add("South Carolina");
            nameList.Add("Manhattem");
            nameList.Add("New Jersey");
            nameList.Add("Fran");
            nameList.Add("Manchester City");


            galleryRV = FindViewById<RecyclerView>(Resource.Id.galleryRecylceView);
            _linearLayout = new LinearLayoutManager(this);
            GalleryAdpater adapter = new GalleryAdpater(this, imageList, nameList);
            galleryRV.SetLayoutManager(_linearLayout);
            galleryRV.SetAdapter(adapter);

        }

        private class GalleryAdpater : RecyclerView.Adapter
        {
            Context context;
            List<int> images;
            List<string> name;
            private event EventHandler<int> ItemClick;
            public GalleryAdpater(Context context,List<int> images,List<string>name)
            {
                this.context = context;
                this.images = images;
                this.name = name;
            }
            public override int ItemCount
            {
                get
                {
                    return images.Count;
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
                GalleryViewHolder vh = holder as GalleryViewHolder;
                vh.gimage.SetImageResource(images[position]);
                vh.gname.Text = name[position];
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.single_item_gallery, parent, false);
                return new GalleryViewHolder(itemView, OnClick);
            }


            private class GalleryViewHolder:RecyclerView.ViewHolder
            {
                public ImageView gimage { get; set; }
                public TextView gname { get; set; }
                public GalleryViewHolder(View itemView,Action<int> listner):base(itemView)
                {
                    gimage = itemView.FindViewById<ImageView>(Resource.Id.sigimageview);
                    gname = itemView.FindViewById<TextView>(Resource.Id.sigName);
                    itemView.Click += (s, e) => listner(AdapterPosition);
                }
            }


        }


    }
}