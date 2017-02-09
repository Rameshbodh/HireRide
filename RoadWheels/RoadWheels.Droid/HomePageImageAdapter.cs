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
using Android.Support.V4.View;
using Java.Lang;

namespace RoadWheels.Droid
{
    class PageImageAdapter : PagerAdapter
    {
        Context context;
        List<int> imagesList;
        LayoutInflater inflater;
        public PageImageAdapter(Context context,List<int> imagesList)
        {
            this.context = context;
            this.imagesList = imagesList;
            inflater = (LayoutInflater)this.context.GetSystemService(Context.LayoutInflaterService);
        }

        public override int Count
        {
            get
            {
                return imagesList.Count;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object objectValue)
        {
            return view == objectValue;
        }
        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View ItemView = inflater.Inflate(Resource.Layout.single_Image_Item, container ,false);

            ImageView image = ItemView.FindViewById<ImageView>(Resource.Id.imageId);

            image.SetImageResource(this.imagesList[position]);
            container.AddView(ItemView);
            return ItemView;
        }

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object objectValue)
        {
            container.RemoveView((LinearLayout)objectValue);
        }

        public void startScroll(ViewPager viewPager,int page)
        {
            //        Thread runnable = new Thread((s, e) =>
            //             {
            //             public void run()
            //    {
            //        if (imagesList.Count == page)
            //        {
            //            page = 0;
            //        }
            //        else
            //        {
            //            page++;
            //        }
            //        viewPager.setCurrentItem(page, true);
            //        handler.postDelayed(this, delay);
            //    }
            //});

            

            }
    }
}