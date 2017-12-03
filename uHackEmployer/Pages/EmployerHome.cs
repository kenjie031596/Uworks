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
using FloatingActionButton = Clans.Fab.FloatingActionButton;
using uHackEmployer.Adapters;
using System.Net.Http;
using System.Threading.Tasks;

namespace uHackEmployer.Pages
{
    [Activity(Label = "Home", MainLauncher = false, Theme = "@style/MyCustomTheme", Icon = "@android:color/transparent", ScreenOrientation = ScreenOrientation.Portrait)]
    public class EmployerHome : AppCompatActivity
    {
        List<JobList> joblist = new List<JobList>();
        ListView job_listview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EmployerHome);
            job_listview = FindViewById<ListView>(Resource.Id.job_listview);
            FloatingActionButton add_btn = FindViewById<FloatingActionButton>(Resource.Id.add_btn);

            add_btn.Click += AddTransaction;

            //joblist.Add(new JobList("Enzo Cruz", "P200 - P300", "Dec 03, 2017 | @10:00 PM", "Nail Technician"));
            //joblist.Add(new JobList("Marvin Dela Cruz", "P500 - P800", "Dec 03, 2017 | @03:00 PM", "Plumber Services"));
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
            joblist.Add(new JobList("Marvin De Guzman", "P300 - P500", "Dec 03, 2017 | @02:00 PM", "Plumber Services"));
            job_listview.Adapter = new JobListAdapter(this, joblist);

            progressDiag.Dismiss();
        }

        private void AddTransaction(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(Employer_Add_Transaction));
            StartActivity(intent);
            this.Finish();
        }
    }
}