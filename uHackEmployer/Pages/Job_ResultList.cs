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
using uHackEmployer.Adapters;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace uHackEmployer.Pages
{
    [Activity(Label = "Home", MainLauncher = false, Icon = "@android:color/transparent", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Job_ResultList : Activity
    {
        List<JobList> joblist = new List<JobList>();
        ListView job_listview;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.Job_Result);
            job_listview = FindViewById<ListView>(Resource.Id.job_listview);

            //Webservice();

            joblist.Add(new JobList("Enzo Cruz", "P200 - P300", "Dec 03, 2017 | @10:00 PM", "Nail Technician"));
            joblist.Add(new JobList("Marvin Dela Cruz", "P500 - P800", "Dec 03, 2017 | @03:00 PM", "Plumber Services"));

            job_listview.Adapter = new JobListAdapter(this, joblist);
        }
    }
}