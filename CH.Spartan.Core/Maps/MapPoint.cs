using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Maps
{
    public class MapPoint
    {

        public MapPoint()
        {

        }

        public MapPoint(string latlng, EnumCoordinates coordinates = EnumCoordinates.Wgs84)
        {
            var ll = latlng.Split(',');
            Lat = double.Parse(ll[0]);
            Lng = double.Parse(ll[1]);
            Coordinates = coordinates;
        }


        public MapPoint(double lat, double lng, EnumCoordinates coordinates = EnumCoordinates.Wgs84)
        {
            Lat = lat;
            Lng = lng;
            Coordinates = coordinates;
        }

        public EnumCoordinates Coordinates { get; set; }

        /// <summary>
        /// 经度 以度为单位的经度值乘以10的6次方,精确到百万分之一度
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// 纬度 以度为单位的纬度值乘以10的6次方,精确到百万分之一度
        /// </summary>
        public double Lat { get; set; }

        public override string ToString()
        {
            return $"{Lat},{Lng}";
        }

    }
}
