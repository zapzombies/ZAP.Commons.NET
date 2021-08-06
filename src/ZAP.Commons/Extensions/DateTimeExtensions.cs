using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAP.Commons.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// The different between <see cref="DateTime.Now"/> and the provided dateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isUTC">The operation calculated in UTC</param>
        /// <returns>A <see cref="TimeSpan"/> represent the delta between now and the provided <paramref name="dateTime"/></returns>
        public static TimeSpan NowDelta(this DateTime dateTime, bool isUTC = false) => (isUTC ? DateTime.UtcNow : DateTime.Now) - dateTime;

        /// <summary>
        /// Determine is <paramref name="declaredDate"/> has expired
        /// </summary>
        /// <param name="declaredDate"></param>
        /// <param name="expiresIn">How much time <paramref name="declaredDate"/> will live</param>
        /// <param name="isUTC">The operation calculated in UTC</param>
        /// <returns></returns>
        public static bool Expires(this DateTime declaredDate, TimeSpan expiresIn, bool isUTC = false) => declaredDate.NowDelta(isUTC) > expiresIn;

        /// <summary>
        /// Determine is <paramref name="declaredDate"/> has expired
        /// </summary>
        /// <param name="declaredDate"></param>
        /// <param name="expiresInSeconds">How much time <paramref name="declaredDate"/> will live in seconds</param>
        /// <param name="isUTC">The operation calculated in UTC</param>
        /// <returns></returns>
        public static bool Expires(this DateTime declaredDate, long expiresInSeconds, bool isUTC = false) => Expires(declaredDate, TimeSpan.FromSeconds(expiresInSeconds), isUTC);
    }
}
