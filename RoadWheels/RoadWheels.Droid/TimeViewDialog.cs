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
using Android.Util;
using Java.Util;

namespace RoadWheels.Droid
{
    class TimeViewDialog : DialogFragment,TimePickerDialog.IOnTimeSetListener
    {
        Action<TimeSpan> _timeSelectedHandler = delegate { };
        public static readonly string TAG = "Y:" + typeof(TimeViewDialog).Name.ToUpper();
        public static TimeViewDialog NewInstance(Action<TimeSpan> onTimeSelected)
        {
            TimeViewDialog dailogView = new TimeViewDialog();
            dailogView._timeSelectedHandler = onTimeSelected;
            return dailogView;
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
            TimeSpan seletedTime = new TimeSpan(hourOfDay, minute, 00);
            Log.Debug(TAG, seletedTime.ToString());
            _timeSelectedHandler(seletedTime);
        }

        
    }
}