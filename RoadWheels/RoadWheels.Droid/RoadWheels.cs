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
using Android.Support.V7.App;
using Android.Support.V4.Content;

namespace RoadWheels.Droid
{
    [Activity(Label = "RoadWheels",MainLauncher = true,Theme= "@style/MyTheme.NoActionBar")]
    public class RoadWheels : AppCompatActivity,ViewPager.IOnPageChangeListener
    {
        private ViewPager roadwheelsPager;
        private LinearLayout dotsLayout;
        private Button registerbtn, loginbtn;
        private List<int> imagesList = new List<int>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.roadwheels);

            roadwheelsPager = FindViewById<ViewPager>(Resource.Id.roadwheelsImagePagerContainer);
            dotsLayout = FindViewById<LinearLayout>(Resource.Id.roadwheelsDotsContainer);
            registerbtn = FindViewById<Button>(Resource.Id.registerUser);
            loginbtn = FindViewById<Button>(Resource.Id.loginUser);

            imagesList.Add(Resource.Raw.image1);
            imagesList.Add(Resource.Raw.image2);
            imagesList.Add(Resource.Raw.image4);

            PageImageAdapter adapter = new PageImageAdapter(this, imagesList);

            roadwheelsPager.Adapter = adapter;

            roadwheelsPager.SetCurrentItem(0, true);

            roadwheelsPager.AddOnPageChangeListener(this);

            setUpUiDots();

            registerbtn.Click += Registerbtn_Click;
            loginbtn.Click += Loginbtn_Click;
        }
        ImageView[] dots;
        private void setUpUiDots()
        {
            dots = new ImageView[imagesList.Count];

            for(int i= 0;i<imagesList.Count;i++)
            {
                dots[i] = new ImageView(this);

                dots[i].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.unselectedDot));

                LinearLayout.LayoutParams para = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                para.Gravity = GravityFlags.Center;
                para.SetMargins(5, 0, 5, 0);
                dotsLayout.AddView(dots[i], para);


            }

            dots[0].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.selectedDot));
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(UserLogin));
           
        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(UserRegister));
          
        }

        public void OnPageScrollStateChanged(int state)
        {
           
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
          
        }

        public void OnPageSelected(int position)
        {
            for (int i = 0; i < imagesList.Count; i++)
            {
                dots[i].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.unselectedDot));
            }

            dots[position].SetImageDrawable(ContextCompat.GetDrawable(this, Resource.Drawable.selectedDot));
        }
    }
}