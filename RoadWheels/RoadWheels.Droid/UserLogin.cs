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
using Android.Content.PM;
using Java.Security;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Java.Lang;
using Xamarin.Facebook.Login;

namespace RoadWheels.Droid
{
    [Activity(Label = "UserLogin")]
    public class UserLogin : AppCompatActivity,IFacebookCallback
    {
        private ICallbackManager mCallbackManager;

        private MyProfileTracker mProfileTracker;

        private Button roadWheelsUserLoginBTN;

        private EditText emailOrMobNumber,passwordEditText;

        private Button myFacebookLoginBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //intialize facebook sdk before setcontentview()
            FacebookSdk.SdkInitialize(this.ApplicationContext);


            mProfileTracker = new MyProfileTracker();

            mProfileTracker.mOnProfileChanged += MProfileTracker_mOnProfileChanged;


            //start listenning to the profile
            mProfileTracker.StartTracking();

            
            // checking user is loged in with facebook in android
            if ( AccessToken.CurrentAccessToken !=null && Xamarin.Facebook.Profile.CurrentProfile !=null)
            {

                
                // the user is loged in by face book
                StartActivity(typeof(HomePage));
                this.Finish();


            }

            SetContentView(Resource.Layout.user_login);

            SupportActionBar.Title = "Login";

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //login button RoadWheels
            roadWheelsUserLoginBTN = FindViewById<Button>(Resource.Id.loginSubmitBtn);

            //facebook login Button
            myFacebookLoginBtn = FindViewById<Button>(Resource.Id.loginFacebookBtn);

            //edit text for email and mobile number
            emailOrMobNumber = FindViewById<EditText>(Resource.Id.userEmailIdOrPhone);
            passwordEditText = FindViewById<EditText>(Resource.Id.userPassord);


            

            roadWheelsUserLoginBTN.Click += RoadWheelsUserLoginBTN_Click;



           

            mCallbackManager = CallbackManagerFactory.Create();

            LoginManager.Instance.RegisterCallback(mCallbackManager, this);

            myFacebookLoginBtn.Click += MyFacebookLoginBtn_Click;

        }

        private void MyFacebookLoginBtn_Click(object sender, EventArgs e)
        {
            //invoking the login manger of facebook 
            LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "public_profile", "user_friends" });


        }

        private void RoadWheelsUserLoginBTN_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(HomePage));
            this.Finish();
        }

        private void MProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            if (e.mProfile != null)
            {
                ISharedPreferences prefs = this.GetSharedPreferences("MYPROFILE", FileCreationMode.Private);

                



                string name =  e.mProfile.Name;

                string profilePicId = e.mProfile.Id;

                prefs.Edit().PutString("username", name).PutString("profilePic",profilePicId).Commit();

                //Bundle bundle = new Bundle();

                //bundle.PutString("username", name);

                //bundle.PutString("profilepic", profilePicId);

                //Intent homePage = new Intent(this,typeof(HomePage));
                //homePage.PutExtras(bundle);
                //StartActivity(homePage);

               
                //Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                //alert.SetTitle("User Facebook Info")
                //    .SetMessage("User Name"+name).SetPositiveButton("LOGOUT",(s, a)=>
                //    {
                //        LoginManager.Instance.LogOut();
                //    });
                //alert.Create();
                //alert.Show();


            }
            else
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("User Facebook Info")
                    .SetMessage("Not loged in");
                alert.Create();
                alert.Show();
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            //with the result below activity decides which method should be called
            //oncancle(),onerror(),onsuccess()
            mCallbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            //stop tracking when the activity get destroyed
          //  mProfileTracker.StopTracking();
        }


        public void OnCancel()
        {

        }

        public void OnError(FacebookException p0)
        {

        }
        //only get called when user get loged in 
        public void OnSuccess(Java.Lang.Object p0)
        {
            //casting java object loginresult
            LoginResult loginResult = p0 as LoginResult;
            
            //accesss to user Id which is facebook user id
            // save it in data base =>> loginResult.AccessToken.UserId
            //check if access token is not null grab the user id
            Console.WriteLine(loginResult.AccessToken.UserId);
            StartActivity(typeof(HomePage));
            this.Finish();

        }



    }

    public class MyProfileTracker : ProfileTracker
    {
        //event for when the profile changes
        public  event EventHandler<OnProfileChangedEventArgs> mOnProfileChanged; 

        //listner for profile
        protected override void OnCurrentProfileChanged(Xamarin.Facebook.Profile oldProfile, Xamarin.Facebook.Profile newProfile)
        {
            //calling when profile changes
            if (mOnProfileChanged != null)
            {
                mOnProfileChanged.Invoke(this, new OnProfileChangedEventArgs(newProfile));
            }
        }
    }
    public class OnProfileChangedEventArgs : EventArgs
    {
        public Xamarin.Facebook.Profile mProfile;
        public OnProfileChangedEventArgs(Xamarin.Facebook.Profile profile)
        {
            mProfile = profile;
        }
    }

}