using System;

namespace TicketTimer.Core.Extensions
{
    static class TimeSpanExtensions
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
    }
}
