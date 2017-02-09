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

namespace RoadWheels.Droid
{
    [Activity(Label = "UserRegister")]
    public class UserRegister : AppCompatActivity
    {
        private EditText userEmailId, userPassword, userReEnteredPassword, userMobileNumber, userFullName;
        private Button registerBtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.user_register);

            SupportActionBar.Title = "Create your Account";
            SupportActionBar.SetDisplayShowHomeEnabled(true);


            userEmailId = FindViewById<EditText>(Resource.Id.uremailid);
            userPassword = FindViewById<EditText>(Resource.Id.ursetpassword);
            userReEnteredPassword = FindViewById<EditText>(Resource.Id.urreenterpassword);
            userFullName = FindViewById<EditText>(Resource.Id.urenterfullname);
            userMobileNumber = FindViewById<EditText>(Resource.Id.urmobilenumber);

            registerBtn = FindViewById<Button>(Resource.Id.urregister);

            registerBtn.Click += RegisterBtn_Click;
            
            userReEnteredPassword.TextChanged += UserReEnteredPassword_TextChanged;
        }
       
        private void UserReEnteredPassword_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //  FindViewById<TextView>(Resource.Id.dummyTExt).Text = e.Text.ToString();
            
                
                    if (userPassword.Text == e.Text.ToString())
                    {
                userReEnteredPassword.SetCompoundDrawablesWithIntrinsicBounds(0, 0, Resource.Raw.ic_tick, 0);
                FindViewById<TextView>(Resource.Id.urcheckPassword).Visibility = ViewStates.Gone;
            }
                    else
                    {
                userReEnteredPassword.SetCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
                FindViewById<TextView>(Resource.Id.urcheckPassword).Visibility = ViewStates.Visible;
                    }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    StartActivity(typeof(RoadWheels));
                    this.Finish();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}