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

namespace RoadWheels.Droid
{
    class DateViewDialog : DialogFragment, DatePickerDialog.IOnDateSetListener
    {

        Action<DateTime> _dateSelectedHandler = delegate { };
        public static readonly string TAG = "Y:" + typeof(DateViewDialog).Name.ToUpper();

        public static DateViewDialog NewInstance(Action<DateTime> onDateSelected)
        {
            DateViewDialog dailogView = new DateViewDialog();
            dailogView._dateSelectedHandler = onDateSelected;
            return dailogView;
        }

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
           // Log.Debug(TAG, seletedDate.ToString());
            _dateSelectedHandler(selectedDate);
        }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime currently = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity,
                                                           this,
                                                           currently.Year,
                                                           currently.Month,
                                                           currently.Day);
            return dialog;
        }

    }
}