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

namespace uHackEmployer.Pages
{
    [Activity(Label = "Login", MainLauncher = false, Icon = "@android:color/transparent", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Login : Activity
    {
        Android.Support.Design.Widget.TextInputEditText login_txt, password_txt;
        Button login_btn;
        TextView register_txt;

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            login_txt = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.login_txt);
            password_txt = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.password_txt);
            login_btn = FindViewById<Button>(Resource.Id.login_btn);
            register_txt = FindViewById<TextView>(Resource.Id.register_txt);

            register_txt.Click += Register_OnClick;

            login_btn.Click += Login_OnClick;
        }

        private void Login_OnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(EmployerHome));
            StartActivity(intent);
        }

        private void Register_OnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Register));
            StartActivity(intent);
        }
    }
}