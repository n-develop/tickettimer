using System;

namespace TicketTimer.Core.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToShortString(this TimeSpan duration)
        {
            var hours = PadLeftWithTwoZeros(duration.Hours);
            var minutes = PadLeftWithTwoZeros(duration.Minutes);
            var seconds = PadLeftWithTwoZeros(duration.Seconds);

            return $"{hours}:{minutes}:{seconds}";
        }

        private static string PadLeftWithTwoZeros(int value)
        {
            return value.ToString().PadLeft(2, '0');
        }

        public static TimeSpan RoundUp(this TimeSpan timeSpan)
        {
            var differenceFromStep = timeSpan.Minutes % 5;
            if (differenceFromStep != 0)
            {
                return timeSpan.Add(new TimeSpan(0, 5 - differenceFromStep, 0));
            }
            return timeSpan;
        }
    }
}
