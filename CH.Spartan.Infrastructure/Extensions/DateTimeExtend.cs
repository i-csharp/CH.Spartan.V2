using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Spartan.Infrastructure
{
    public static class DateTimeExtend
    {
        /// <summary>
        /// 一天时间的开始
        /// </summary>
        public const string TIME_BEGIN_FOR_DAY = " 00:00:00";

        /// <summary>
        /// 一天时间的结束
        /// </summary>
        public const string TIME_END_FOR_DAY = " 23:59:59";

        #region 获取本月最后一天的时间 例如 2011-03-12 16:08 返回  2011-03-31 23:59

        /// <summary>
        /// 获取本月最后一天的时间 例如 2011-03-12 16:08 返回  2011-03-31 23:59
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetLastDayEndTime(this DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            int day = DateTime.DaysInMonth(year, month);
            return DateTime.Parse(new DateTime(year, month, day).ToShortDateString() + TIME_END_FOR_DAY);
        }

        #endregion 获取本月最后一天的时间 例如 2011-03-12 16:08 返回  2011-03-31 23:59

        #region 获取本月第一天的时间 例如 2011-03-12 16:08 返回  2011-03-01 00:00

        /// <summary>
        /// 获取本月第一天的时间 例如 2011-03-12 16:08 返回  2011-03-01 00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayStartTime(this DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            return DateTime.Parse(new DateTime(year, month, 1).ToShortDateString() + TIME_BEGIN_FOR_DAY);
        }

        #endregion 获取本月第一天的时间 例如 2011-03-12 16:08 返回  2011-03-01 00:00

        #region 获一天的时间开始时间 例如 2011-03-12 16:08 返回  2011-03-12 00:00

        /// <summary>
        /// 获一天的时间开始时间 例如 2011-03-12 16:08 返回  2011-03-12 00:00
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDayStartTime(this DateTime dt)
        {
            int year = dt.Year;
            int month = dt.Month;
            return DateTime.Parse(dt.ToShortDateString() + TIME_BEGIN_FOR_DAY);
        }

        #endregion 获一天的时间开始时间 例如 2011-03-12 16:08 返回  2011-03-12 00:00

        #region 获取一天的时间最后时间 例如 2011-03-12 16:08 返回  2011-03-12 23:59

        /// <summary>
        /// 获取一天的时间最后时间 例如 2011-03-12 16:08 返回  2011-03-12 23:59
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetDayEndTime(this DateTime dt)
        {
            return DateTime.Parse(dt.ToShortDateString() + TIME_END_FOR_DAY);
        }

        #endregion 获取一天的时间最后时间 例如 2011-03-12 16:08 返回  2011-03-12 23:59

        #region 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前

        /// <summary>
        /// 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateStringFromNow(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.TotalDays > 60)
            {
                return dt.ToShortDateString();
            }
            else if (span.TotalDays > 30)
            {
                return "1个月前";
            }
            else if (span.TotalDays > 14)
            {
                return "2周前";
            }
            else if (span.TotalDays > 7)
            {
                return "1周前";
            }
            else if (span.TotalDays > 1)
            {
                return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
            }
            else if (span.TotalSeconds >= 1)
            {
                return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
            }
            else
            {
                return "1秒前";
            }
        }

        #endregion 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前

        #region 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前

        /// <summary>
        /// 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateStringLabel(this DateTime dt)
        {
            int day = DateTime.Now.Day - dt.Day;
            if (day == 0)
            {
                return dt.ToString("HH:mm");
            }
            else if (day == 1)
            {
                return "昨天 " + dt.ToString("HH:mm");
            }
            else if (day == 2)
            {
                return "前天 " + dt.ToString("HH:mm");
            }
            else
            {
                return dt.ToString("MM月dd日 HH:mm");
            }
        }

        #endregion 根据时间返回几个月前,几天前,几小时前,几分钟前,几秒前

        #region 时间差计算

        /// <summary>
        /// 计算两个时间的差值,返回的是x天x小时x分钟x秒
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string GetDateDiff(this DateTime from, DateTime to)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(from.Ticks);
            TimeSpan ts2 = new TimeSpan(to.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        /// <summary>
        /// 时间相差值,返回时间差
        /// 调用时,isTotal为true时,返回的时带小数的天数,否则返回的是整数
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="isTotal"></param>
        /// <returns></returns>
        public static string GetDateDiff(this DateTime from, DateTime to, bool isTotal)
        {
            TimeSpan ts = to - from;
            if (isTotal)
            {
                //带小数的天数，比如1天12小时结果就是1.5
                return ts.TotalDays.ToString();
            }
            else
            {
                //整数天数，1天12小时或者1天20小时结果都是1
                return ts.Days.ToString();
            }
        }

        #endregion 时间差计算

        #region 根据英文的星期几返回中文的星期几

        /// <summary>
        /// 根据英文的星期几返回中文的星期几
        /// 如GetWeek("Sunday"),返回星期日
        /// </summary>
        /// <param name="enWeek"></param>
        /// <returns></returns>
        public static string GetWeek(this DateTime dt)
        {
            switch (dt.DayOfWeek.ToString())
            {
                case "Sunday":
                    return "星期日";
                case "Monday":
                    return "星期一";
                case "Tuesday":
                    return "星期二";
                case "Wednesday":
                    return "星期三";
                case "Thursday":
                    return "星期四";
                case "Friday":
                    return "星期五";
                case "Saturday":
                    return "星期六";
                default:
                    return "";
            }
        }

        #endregion 根据英文的星期几返回中文的星期几

        #region 根据出生年月进行生日提醒

        /// <summary>
        /// 根据出生年月进行生日提醒
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public static string GetBirthdayTip(this DateTime birthday)
        {
            DateTime now = DateTime.Now;
            int nowMonth = now.Month;
            int birtMonth = birthday.Month;
            if (nowMonth == 12 && birtMonth == 1)
            {
                return string.Format("下月{0}号", birthday.Day);
            }

            if (nowMonth == 1 && birtMonth == 12)
            {
                return string.Format("上月{0}号", birthday.Day);
            }

            int months = now.Month - birthday.Month;
            if (months == 1)
            {
                return string.Format("上月{0}号", birthday.Day);
            }
            else if (months == -1)
            {
                return string.Format("下月{0}号", birthday.Day);
            }
            else if (months == 0)
            {
                if (now.Day == birthday.Day)
                {
                    return "今天";
                }
                return string.Format("本月{0}号", birthday.Day);
            }
            else if (months > 1)
            {
                return string.Format("已过{0}月", months);
            }
            else
            {
                return string.Format("{0}月{1}日", birthday.Month, birthday.Day);
            }
        }

        #endregion 根据出生年月进行生日提醒

        #region 根据时间推算出年龄

        /// <summary>
        /// 根据时间推算出年龄
        /// </summary>
        /// <returns>Year + 岁 + Month + 个月 + Day + 天</returns>
        public static string GetAge(this DateTime dt)
        {
            int nYear = DateTime.Now.Year;
            int nMonth = DateTime.Now.Month;
            int nDay = DateTime.Now.Day;
            int oYear = dt.Year;
            int oMonth = dt.Month;
            int oDay = dt.Day;
            int sYear = 0;
            int sMonth = 0;
            int sDay = 0;

            //存大小月天数
            int dayofmonth = 30;
            if ("1,3,5,7,8,10,12".IndexOf(oDay.ToString()) != -1)
            {
                dayofmonth = 31;
            }

            if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(dt.ToString("yyyy-MM-dd")))
            {
                sYear = nYear - oYear;

                if (nMonth >= oMonth)
                {
                    sMonth = nMonth - oMonth;
                }
                else
                {
                    sYear = sYear - 1;
                    sMonth = 12 - oMonth + nMonth;
                }

                if (oDay > nDay)
                {
                    if ((sMonth - 1) < 0)
                    {
                        sMonth = 12 + (sMonth - 1);
                        sYear = sYear - 1;
                    }
                    else
                    {
                        sMonth = sMonth - 1;
                    }
                    sDay = dayofmonth - oDay + nDay;
                }
                else
                {
                    sDay = nDay - oDay;
                }
            }

            string dateDiff = null;
            dateDiff = sYear + "岁" + sMonth + "个月" + sDay + "天";
            return dateDiff;
        }

        #endregion 根据时间推算出年龄

        #region 根据时间推算出年龄

        /// <summary>
        /// 根据时间推算出年龄
        /// </summary>
        /// <returns>X + 岁</returns>
        public static int GetAgeByInt(this DateTime dt)
        {
            int nYear = DateTime.Now.Year;
            int nMonth = DateTime.Now.Month;
            int nDay = DateTime.Now.Day;
            int oYear = dt.Year;
            int oMonth = dt.Month;
            int oDay = dt.Day;
            int sYear = 0;
            int sMonth = 0;
            int sDay = 0;

            //存大小月天数
            int dayofmonth = 30;
            if ("1,3,5,7,8,10,12".IndexOf(oDay.ToString()) != -1) dayofmonth = 31;

            if (Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")) >= Convert.ToDateTime(dt.ToString("yyyy-MM-dd")))
            {
                sYear = nYear - oYear;

                if (nMonth >= oMonth)
                {
                    sMonth = nMonth - oMonth;
                }
                else
                {
                    sYear = sYear - 1;
                    sMonth = 12 - oMonth + nMonth;
                }

                if (oDay > nDay)
                {
                    if ((sMonth - 1) < 0)
                    {
                        sMonth = 12 + (sMonth - 1);
                        sYear = sYear - 1;
                    }
                    else
                    {
                        sMonth = sMonth - 1;
                    }
                    sDay = dayofmonth - oDay + nDay;
                }
                else
                {
                    sDay = nDay - oDay;
                }
            }

            return sYear;
        }

        #endregion 根据时间推算出年龄

        #region 根据时间推算出星座

        /// <summary>
        /// 根据时间推算出星座
        /// </summary>
        /// <returns>"水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座"</returns>
        public static string GetAtom(this DateTime dt)
        {
            float birthdayF = 0.00F;

            if (dt.Month == 1 && dt.Day < 20)
            {
                if (dt.Day < 10)
                {
                    birthdayF = float.Parse(string.Format("13.0{0}", dt.Day));
                }
                else
                {
                    birthdayF = float.Parse(string.Format("13.{0}", dt.Day));
                }
            }
            else
            {
                if (dt.Day < 10)
                {
                    birthdayF = float.Parse(string.Format("{0}.0{1}", dt.Month, dt.Day));
                }
                else
                {
                    birthdayF = float.Parse(string.Format("{0}.{1}", dt.Month, dt.Day));
                }
            }

            float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
            string[] atoms = { "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };

            string ret = "";
            for (int i = 0; i < atomBound.Length - 1; i++)
            {
                if (atomBound[i] <= birthdayF && atomBound[i + 1] > birthdayF)
                {
                    ret = atoms[i];
                    break;
                }
            }
            return ret;
        }

        #endregion 根据时间推算出星座
    }
}
