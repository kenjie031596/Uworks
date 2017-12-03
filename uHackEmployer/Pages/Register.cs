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
using Android.Support.V7.App;

namespace uHackEmployer.Pages
{
    [Activity(Label = "Register", MainLauncher = false, Theme = "@style/MyCustomTheme", Icon = "@android:color/transparent", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Register : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Register);

            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            //Spinner expertise_spin = FindViewById<Spinner>(Resource.Id.expertise_spin);
            //var adapter = new ArrayAdapter<String>(this, Resource.Drawable.spinner_item, items);
            //adapter.SetDropDownViewResource(Resource.Drawable.spinner_item);
            //expertise_spin.Adapter = adapter;

            Spinner expertise_spin = FindViewById<Spinner>(Resource.Id.expertise_spin);
            var adapter = ArrayAdapter.CreateFromResource(
            this, Resource.Array.expertise_array, Resource.Drawable.spinner_item);

            Button done_btn = FindViewById<Button>(Resource.Id.done_btn);
            done_btn.Click += Done_OnClick;

            expertise_spin.Adapter = adapter;
        }

        private void Done_OnClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(EmployerHome));
            StartActivity(intent);
            this.Finish();
        }
    }
}