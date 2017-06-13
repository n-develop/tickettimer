using System;

namespace TicketTimer.Youtrack.Extensions
{
    public static class YoutrackDate
    {
        private static readonly DateTime BaseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long ToYoutrackDate(this DateTime date)
        {
            var utcDate = date.ToUniversalTime();
            return (long)utcDate.Subtract(BaseDate).TotalMilliseconds;
        }

        public static DateTime ToDateTime(long youtrackDate)
        {
            var utcDate = BaseDate.AddMilliseconds(youtrackDate);
            return utcDate;
        }
    }
}
