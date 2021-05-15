using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Domain
{
    public static class Clock
    {
        private static DateTime? _customDate;
        public static DateTime Now => _customDate ?? DateTime.UtcNow;

        public static void SetCustomDate(DateTime dateTime)
        {
            _customDate = dateTime;
        }
        public static void ResetCustomDate()
        {
            _customDate = null;
        }
    }
}
