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
using Android.Util;
using Java.Util;
using uHackEmployer.Class;
using System.Threading.Tasks;

namespace uHackEmployer.Pages
{
    [Activity(Label = "Home", MainLauncher = false, Theme = "@style/MyCustomTheme", Icon = "@android:color/transparent", ScreenOrientation = ScreenOrientation.Portrait)]
    public class Employer_Add_Transaction : AppCompatActivity
    {
        Button date_pick, time_pick, perm_validate_btn, send_request_btn;
        Spinner expertise_spin;
        Android.Support.Design.Widget.TextInputEditText zipcode_edit,
                                                        muni_edit,
                                                        city_province_edit,
                                                        from_edit,
                                                        to_edit;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Employer_Add_Transaction);

            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            expertise_spin = FindViewById<Spinner>(Resource.Id.expertise_spin);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.expertise_array, Resource.Drawable.spinner_item);

            expertise_spin.Adapter = adapter;

            date_pick = FindViewById<Button>(Resource.Id.date_pick);
            date_pick.Click += DateSelect_OnClick;
            time_pick = FindViewById<Button>(Resource.Id.time_pick);
            time_pick.Click += TimeSelect_OnClick;
            send_request_btn = FindViewById<Button>(Resource.Id.send_request_btn);
            send_request_btn.Click += delegate
            {
                var intent = new Intent(this, typeof(EmployerHome));
                StartActivity(intent);
                this.Finish();
                Toast.MakeText(this, "A new transaction was successully sent!", ToastLength.Short).Show();
            };

            zipcode_edit = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.zipcode_edit);
            muni_edit = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.muni_edit);
            city_province_edit = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.city_province_edit);

            perm_validate_btn = FindViewById<Button>(Resource.Id.perm_validate_btn);
            perm_validate_btn.Click += PermValidate;

            from_edit = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.from_edit);
            from_edit.FocusChange += AddPhp;

            to_edit = FindViewById<Android.Support.Design.Widget.TextInputEditText>(Resource.Id.to_edit);
            to_edit.FocusChange += AddPhp;
        }

        private void AddPhp(object sender, View.FocusChangeEventArgs e)
        {
            Android.Support.Design.Widget.TextInputEditText sent = (Android.Support.Design.Widget.TextInputEditText)sender;
            string php = "Php ";
            if (!sent.HasFocus)
            {
                if (sent.Text.Length > 0)
                {
                    sent.Text = php + sent.Text;
                }
            }
            else
            {
                sent.Text = sent.Text.Replace("Php ", "");
            }
        }

        private async void PermValidate(object sender, EventArgs e)
        {
            Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this, Resource.Style.AppTheme_Dialog);

            ProgressDialog progressDiag = new ProgressDialog(this);
            progressDiag.Indeterminate = true;
            progressDiag.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDiag.SetMessage("Validating your zipcode... Please wait...");
            progressDiag.SetCancelable(false);
            progressDiag.Show();

            List<Zipcode> zipcodeList = await Task.Run(() => GetAddress(zipcode_edit.Text.Trim()));

            try
            {
                if (zipcodeList.Count > 0)
                {
                    muni_edit.Text = zipcodeList[0].Municipalities;
                    city_province_edit.Text = zipcodeList[0].CityProvince;

                    Toast.MakeText(this, "Your zipcode is valid!", ToastLength.Short).Show();

                    muni_edit.RequestFocus();
                }

                else
                {
                    alertDialog.SetMessage("Invalid zipcode! Please try again...");
                    alertDialog.SetPositiveButton("OK", (senderAlert, args) => { });
                    alertDialog.Show();
                }
            }
            catch
            {
                alertDialog.SetMessage("Invalid zipcode! Please try again...");
                alertDialog.SetPositiveButton("OK", (senderAlert, args) => { });
                alertDialog.Show();
            }

            progressDiag.Dismiss();
        }

        private async Task<List<Zipcode>> GetAddress(string zipcodeStr)
        {
            ZipcodeMaster zipcodeMaster = new ZipcodeMaster();
            List<Zipcode> zipcodeList = zipcodeMaster.zipcodes.Where(x => x.ZipCodes == zipcodeStr).ToList();
            return zipcodeList;
        }

        void DateSelect_OnClick(object sender, EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                date_pick.Text = time.ToString("MM/dd/yyyy");
            });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
        void TimeSelect_OnClick(object sender, EventArgs eventArgs)
        {
            TimePickerFragment frag = TimePickerFragment.NewInstance(delegate (TimeSpan time)
            {
                time_pick.Text = time.ToString();
            });
            frag.Show(FragmentManager, TimePickerFragment.TAG);
        }
    }
}

//datepicker
public class DatePickerFragment : DialogFragment,
                                  DatePickerDialog.IOnDateSetListener
{
    // TAG can be any string of your choice.
    public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();

    // Initialize this value to prevent NullReferenceExceptions.
    Action<DateTime> _dateSelectedHandler = delegate { };

    public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
    {
        DatePickerFragment frag = new DatePickerFragment();
        frag._dateSelectedHandler = onDateSelected;
        return frag;
    }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
        DateTime currently = DateTime.Now;
        DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                       this,
                                                       currently.Year,
                                                       currently.Month - 1,
                                                       currently.Day);
        return dialog;
    }

    public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
    {
        // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
        DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
        Log.Debug(TAG, selectedDate.ToLongDateString());
        _dateSelectedHandler(selectedDate);
    }
}

//timepicker

public class TimePickerFragment : DialogFragment,
                          TimePickerDialog.IOnTimeSetListener
{
    // TAG can be any string of your choice.
    public static readonly string TAG = "Y:" + typeof(TimePickerFragment).Name.ToUpper();

    // Initialize this value to prevent NullReferenceExceptions.
    Action<TimeSpan> _timeSelectedHandler = delegate { };

    public static TimePickerFragment NewInstance(Action<TimeSpan> onTimeSet)
    {
        TimePickerFragment frag = new TimePickerFragment();
        frag._timeSelectedHandler = onTimeSet;
        return frag;
    }

    public override Dialog OnCreateDialog(Bundle savedInstanceState)
    {
        Calendar c = Calendar.Instance;
        int hour = c.Get(CalendarField.HourOfDay);
        int minute = c.Get(CalendarField.Minute);
        bool is24HourView = true;
        TimePickerDialog dialog = new TimePickerDialog(Activity,
                                                       this,
                                                       hour,
                                                       minute,
                                                       is24HourView);
        return dialog;
    }

    public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
    {
        //Do something when time chosen by user
        TimeSpan selectedTime = new TimeSpan(hourOfDay, minute, 00);
        Log.Debug(TAG, selectedTime.ToString());
        _timeSelectedHandler(selectedTime);
    }
}