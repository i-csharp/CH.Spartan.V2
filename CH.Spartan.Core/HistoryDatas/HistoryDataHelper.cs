using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Spartan.HistoryDatas
{
    public static class HistoryDataHelper
    {
        #region Common
        /// <summary>
        /// 获取图标地址
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string GetIconUrl(HistoryData historyData)
        {

            var icon = "6.gif";
            if (historyData.Direction > 27 && historyData.Direction <= 72)
            {
                icon = "5.gif";
            }
            else if (historyData.Direction > 72 && historyData.Direction <= 117)
            {
                icon = "4.gif";
            }
            else if (historyData.Direction > 117 && historyData.Direction <= 162)
            {
                icon = "3.gif";
            }
            else if (historyData.Direction > 162 && historyData.Direction <= 207)
            {
                icon = "2.gif";
            }
            else if (historyData.Direction > 207 && historyData.Direction <= 252)
            {
                icon = "1.gif";
            }
            else if (historyData.Direction > 252 && historyData.Direction <= 297)
            {
                icon = "8.gif";
            }
            else if (historyData.Direction > 297 && historyData.Direction <= 342)
            {
                icon = "7.gif";
            }
            return $"/Content/img/cars/planeCar/green/{icon}";
        }
        #endregion

        public static string WebIconUrl(HistoryData historyData)
        {
            return GetIconUrl(historyData);
        }
    }
}
