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
using uHackEmployer.Adapters;
using Android.Support.V7.App;
using Android.Content.PM;
using System.Threading.Tasks;

namespace uHackEmployer.Pages
{
    [Activity(Label = "Home", MainLauncher = false, Theme = "@style/MyCustomTheme", Icon = "@android:color/transparent", ScreenOrientation = ScreenOrientation.Portrait)]
    public class EmployeeHome : AppCompatActivity
    {
        public static List<JobList> joblist = new List<JobList>();
        public static ListView job_listview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmployeeHome);
            job_listview = FindViewById<ListView>(Resource.Id.job_listview);

            job_listview.Adapter = new JobListAdapter(this, joblist);

            Button add_row_btn = FindViewById<Button>(Resource.Id.add_row_btn);
            add_row_btn.Click += Add_Row;
        }

        private async void Add_Row(object sender, EventArgs e)
        {
            joblist.Clear();

            ProgressDialog progressDiag = new ProgressDialog(this);
            progressDiag.Indeterminate = true;
            progressDiag.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDiag.SetCancelable(false);
            progressDiag.SetMessage("Please wait...");
            progressDiag.Show();

            await Task.Delay(2000);
            joblist.Add(new JobList("Brandon Jake Torress", "P300 - P500", "Dec 03, 2017 | @02:00 PM", "Plumber Services"));
            job_listview.Adapter = new JobListAdapter(this, joblist);

            progressDiag.Dismiss();
        }
    }
}