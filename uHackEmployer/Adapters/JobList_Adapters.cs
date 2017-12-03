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

namespace uHackEmployer.Adapters
{
    public class JobList
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Job { get; set; }


        public JobList(string name, string price, string date, string job)
        {
            Name = name;
            Price = price;
            Date = date;
            Job = job;
        }
    }

    public class JobListAdapter : BaseAdapter<JobList>
    {
        List<JobList> joblist;
        Activity activity;
        public JobListAdapter(Activity activity, IEnumerable<JobList> _joblist)
            : base()
        {
            this.activity = activity;
            this.joblist = _joblist.ToList();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override JobList this[int position]
        {
            get { return joblist[position]; }
        }

        public override int Count
        {
            get { return joblist.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = activity.LayoutInflater.Inflate(Resource.Drawable.Job_List_Row, parent, false);

            TextView name_txt = view.FindViewById<TextView>(Resource.Id.name_txt);
            TextView pricerange_txt = view.FindViewById<TextView>(Resource.Id.pricerange_txt);
            TextView date_txt = view.FindViewById<TextView>(Resource.Id.date_txt);
            TextView job_txt = view.FindViewById<TextView>(Resource.Id.job_txt);

            Button accept_btn = view.FindViewById<Button>(Resource.Id.accept_btn);
            accept_btn.Click += delegate
            {
                this.joblist.RemoveAt(0);
                activity.RunOnUiThread(() => this.NotifyDataSetChanged());

                //Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_Dialog);
                //alertDialog.SetMessage("Please wait for a text message to see if Mr. Torres approve your job application.");
                //alertDialog.SetPositiveButton("OK", (senderAlert, args) => { });
                //alertDialog.Show();

                Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_Dialog);
                alertDialog.SetMessage("Job application was approved!");
                alertDialog.SetPositiveButton("OK", (senderAlert, args) => { });
                alertDialog.Show();
            };

            Button decline_btn = view.FindViewById<Button>(Resource.Id.decline_btn);
            decline_btn.Click += delegate
            {
                this.joblist.RemoveAt(0);
                activity.RunOnUiThread(() => this.NotifyDataSetChanged());

                //Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_Dialog);
                //alertDialog.SetMessage("Job request deleted!");
                //alertDialog.SetPositiveButton("OK", (senderAlert, args) => { });
                //alertDialog.Show();

                Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(activity, Resource.Style.AppTheme_Dialog);
                alertDialog.SetMessage("Job application deleted!");
                alertDialog.SetPositiveButton("OK", (senderAlert, args) => { });
                alertDialog.Show();
            };

            name_txt.Text = joblist[position].Name;
            pricerange_txt.Text = joblist[position].Price;
            date_txt.Text = joblist[position].Date;
            job_txt.Text = joblist[position].Job;

            return view;
        }
    }
}