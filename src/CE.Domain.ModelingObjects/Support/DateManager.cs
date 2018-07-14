// DateManager.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using System;

namespace CE.Domain.Support
{
    public static class DateManager
    {
        #region Constructor
        static DateManager()
        {
            Current = DateTime.Today;
        }
        #endregion

        #region Public Methods
        public static void SetCurrentDate(DateTime date)
        {
            Current = date;
        }
        public static int DaysRemaining(this DateTime date)
        {
            TimeSpan ts = date.Subtract(Current);
            int daysRemaning = ts.Days;
            int minutesRemaining = ts.Minutes;

            if (minutesRemaining > 0)
                daysRemaning += 1;

            return daysRemaning;
        }
        public static int DaysOverdue(this DateTime date)
        {
            TimeSpan ts = date.Subtract(Current);
            int daysRemaining = ts.Days;
            int minutesRemaining = ts.Minutes;

            if (minutesRemaining > 0)
                daysRemaining += 1;

            return daysRemaining;
        }
        #endregion

        #region Public Properties
        public static DateTime Current
        {
            get;
            private set;
        }
        #endregion
    }
}
