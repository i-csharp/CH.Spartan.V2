using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Json;
using Abp.Runtime.Caching;
using CH.Spartan.Domain;
using CH.Spartan.Infrastructure;

namespace CH.Spartan.Maps
{
    /** 
  * 各地图API坐标系统比较与转换; 
  * WGS84坐标系：即地球坐标系，国际上通用的坐标系。设备一般包含GPS芯片或者北斗芯片获取的经纬度为WGS84地理坐标系, 
  * 谷歌地图采用的是WGS84地理坐标系（中国范围除外）; 
  * GCJ02坐标系：即火星坐标系，是由中国国家测绘局制订的地理信息系统的坐标系统。由WGS84坐标系经加密后的坐标系。 
  * 谷歌中国地图和搜搜中国地图采用的是GCJ02地理坐标系; BD09坐标系：即百度坐标系，GCJ02坐标系经加密后的坐标系; 
  * 搜狗坐标系、图吧坐标系等，估计也是在GCJ02基础上加密而成的。 
  */
    public class MapManager : ManagerBase
    {

        private readonly double _r = 6378245.0;
        private double _e = 0.00669342162296594323;
        private readonly string _aMapAk_Api;
        public MapManager(ISettingManager settingManager, ICacheManager cacheManager, IIocResolver iocResolver, IUnitOfWorkManager unitOfWorkManager) :
            base(settingManager, cacheManager, iocResolver, unitOfWorkManager)
        {
            _aMapAk_Api = settingManager.GetSettingValueForApplication(SpartanSettingKeys.General_Map_AMapAk_Api);
        }

        #region 坐标转换

        public MapPoint Gcj02ToWgs84(MapPoint mapPoint)
        {
            var mapPoint1 = TransForm(mapPoint);
            var lontitude = mapPoint.Lng * 2 - mapPoint1.Lng;
            var latitude = mapPoint.Lat * 2 - mapPoint1.Lat;
            return new MapPoint(latitude, lontitude) { Coordinates = EnumCoordinates.Wgs84 };
        }

        public MapPoint Wgs84ToGcj02(MapPoint mapPoint)
        {
            if (mapPoint.Lng == 0 || mapPoint.Lng == 0||IsOutOfChina(mapPoint))
            {
                return mapPoint;
            }
            var dLat = TransFormLat(mapPoint.Lng - 105.0, mapPoint.Lat - 35.0);
            var dLon = TransFormLng(mapPoint.Lng - 105.0, mapPoint.Lat - 35.0);
            var radLat = mapPoint.Lat / 180.0 * Math.PI;
            var magic = Math.Sin(radLat);
            magic = 1 - _e * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((_r * (1 - _e)) / (magic * sqrtMagic) * Math.PI);
            dLon = (dLon * 180.0) / (_r / sqrtMagic * Math.Cos(radLat) * Math.PI);
            var mgLat = mapPoint.Lat + dLat;
            var mgLon = mapPoint.Lng + dLon;
            return new MapPoint(mgLat, mgLon) { Coordinates = EnumCoordinates.Gcj02 };
        }

        public MapPoint[] Gcj02ToWgs84(MapPoint[] mapPoints)
        {
            for (var i = 0; i < mapPoints.Length; i++)
            {
                mapPoints[i] = Gcj02ToWgs84(mapPoints[i]);
            }
            return mapPoints;
        }

        public MapPoint[] Wgs84ToGcj02(MapPoint[] mapPoints)
        {
            for (var i = 0; i < mapPoints.Length; i++)
            {
                mapPoints[i] = Wgs84ToGcj02(mapPoints[i]);
            }
            return mapPoints;
        }

        private MapPoint TransForm(MapPoint mapPoint)
        {
            if (mapPoint.Lng == 0 || mapPoint.Lng == 0 || IsOutOfChina(mapPoint))
            {
                return mapPoint;
            }
            var dLat = TransFormLat(mapPoint.Lng - 105.0, mapPoint.Lat - 35.0);
            var dLon = TransFormLng(mapPoint.Lng - 105.0, mapPoint.Lat - 35.0);
            var radLat = mapPoint.Lat / 180.0 * Math.PI;
            var magic = Math.Sin(radLat);
            magic = 1 - _e * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((_r * (1 - _e)) / (magic * sqrtMagic) * Math.PI);
            dLon = (dLon * 180.0) / (_r / sqrtMagic * Math.Cos(radLat) * Math.PI);
            var mgLat = mapPoint.Lat + dLat;
            var mgLon = mapPoint.Lng + dLon;
            return new MapPoint(mgLat, mgLon);
        }

        private double TransFormLat(double x, double y)
        {
            var ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                    + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * Math.PI) + 20.0 * Math.Sin(2.0 * x * Math.PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * Math.PI) + 40.0 * Math.Sin(y / 3.0 * Math.PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * Math.PI) + 320 * Math.Sin(y * Math.PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private double TransFormLng(double x, double y)
        {
            var ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                    * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * Math.PI) + 20.0 * Math.Sin(2.0 * x * Math.PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * Math.PI) + 40.0 * Math.Sin(x / 3.0 * Math.PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * Math.PI) + 300.0 * Math.Sin(x / 30.0
                    * Math.PI)) * 2.0 / 3.0;
            return ret;
        }

        #endregion

        #region 坐标格式转换
        /// <summary>
        /// 度(DDD.DDDDD)转换成度分秒 例如57.9323888888888->57°55'56.6"
        /// </summary>
        /// <param name="ddd">度</param>
        /// <returns></returns>
        public string ConvertDDDToDMS(double ddd)
        {
            double d = Math.Floor(ddd);
            double m = (ddd - d) * 60;
            double s = (m - Math.Floor(m)) * 60;
            return string.Format("{0}°{1}'{2}\"", d, Math.Floor(m), s);
        }
        /// <summary>
        /// 度分秒(DDD°MM'SS")转换成度 例如57°55'56.6"->57.9323888888888
        /// </summary>
        /// <param name="d">度</param>
        /// <param name="m">分</param>
        /// <param name="s">秒</param>
        /// <returns></returns>
        public double ConvertDMSToDDD(double d, double m, double s)
        {
            return d + m / 60d + s / 3600d;
        }
        /// <summary>
        /// 度分(DDMM.MMM)转换成度 例如7717.3644->77.28941
        /// </summary>
        /// <param name="dm">度分</param>
        /// <returns></returns>
        public double ConvertDMToDDD(string dmString)
        {

            double ddd = 0;
            double dm;
            if (double.TryParse(dmString, out dm))
            {
                dm = dm / 100;//->77.173644
                var d = Math.Floor(dm);//->77
                ddd = d + ((dm - d) * 100) / 60;//->77+17.3644/60
            }
            return ddd;
        }
        /// <summary>
        /// 度(DDD.DDDDD)转换成度分 例如77.28941->7717.3644
        /// </summary>
        /// <param name="dm">度分</param>
        /// <returns></returns>
        public string ConvertDDDToDM(double ddd)
        {
            double d = Math.Floor(ddd);
            double m = (ddd - d) * 60;
            return string.Format("{0}{1}", d, m);
        }
        #endregion 坐标格式转换

        #region 地理计算
        /// <summary>
        /// 获取地理位置
        /// </summary>
        /// <returns></returns>
        public MapLocation GetLocation(MapPoint mapPoint)
        {
            if (mapPoint.Coordinates == EnumCoordinates.Wgs84)
            {
                mapPoint = Wgs84ToGcj02(mapPoint);
            }

            MapLocation location = new MapLocation();
            try
            {
                var url = $"http://restapi.amap.com/v3/geocode/regeo?key={_aMapAk_Api}&location={mapPoint.Lng},{mapPoint.Lat}&homeorcorp=1&radius=1000&extensions=all&batch=false&roadlevel=1";
                var json = HttpLibSyncRequest.Get(url).RegexReplace(@"\[\]","''");
                location = json.ToObject<MapLocation>();
            }
            catch(Exception ex)
            {
                location = new MapLocation
                {
                    Regeocode = new Regeocode
                    {
                        FormattedAddress = "-",
                        Pois = new Pois[0],
                    }
                };
            }
            return location;
        }
        /// <summary>
        /// 获取两点间的距离
        /// </summary>
        /// <param name="mapPoint1"></param>
        /// <param name="mapPoint2"></param>
        /// <returns>米</returns>
        public double GetDistance(MapPoint mapPoint1, MapPoint mapPoint2)
        {
            double radLat1 = mapPoint1.Lat * Math.PI / 180.0;
            double radLat2 = mapPoint2.Lat * Math.PI / 180.0;
            double a = radLat1 - radLat2;
            double b = mapPoint1.Lng * Math.PI / 180.0 - mapPoint2.Lng * Math.PI / 180.0;

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * (_r / 1000);
            s = Math.Round(s * 10000) / 10;
            return s;
        }
        /// <summary>
        /// 判断点是否在圆内(在边上也认为在圆内)
        /// </summary>
        /// <param name="mapCenterPoint">圆心坐标</param>
        /// <param name="radius">圆半径 米</param>
        /// <param name="mapPoint">当前点</param>
        /// <returns></returns>
        public bool PointIsInCircle(MapPoint mapCenterPoint, double radius, MapPoint mapPoint)
        {
            return GetDistance(mapCenterPoint, mapPoint) <= radius;
        }
        /// <summary>
        /// 判断点是否在矩形 A B 两点即可构成一个矩形
        /// </summary>
        /// <param name="mapAPoint">A点</param>
        /// <param name="mapBPoint">B点</param>
        /// <param name="mapPoint">当前点</param>
        /// <returns></returns>
        public bool PointIsInRect(MapPoint mapAPoint, MapPoint mapBPoint, MapPoint mapPoint)
        {
            double maxx = 0;
            double minx = 0;
            double maxy = 0;
            double miny = 0;
            if (mapAPoint.Lng > mapBPoint.Lng)
            {
                maxx = mapAPoint.Lng;
                minx = mapBPoint.Lng;
            }
            else
            {
                maxx = mapBPoint.Lng;
                minx = mapAPoint.Lng;
            }

            if (mapAPoint.Lat > mapBPoint.Lat)
            {
                maxy = mapAPoint.Lat;
                miny = mapBPoint.Lat;
            }
            else
            {
                maxy = mapBPoint.Lat;
                miny = mapAPoint.Lat;
            }

            if (mapPoint.Lng < maxx && mapPoint.Lng > minx && mapPoint.Lat < maxy && mapPoint.Lat > miny)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断点是否在多边形内
        /// 射线法
        /// 原理是向由点mapPoint向经度正方向发射一个射线,穿过多边形线段上的个数为奇数则在多边形内,偶数则在多边形外
        /// </summary>
        /// <param name="mapPoints">点集合</param>
        /// <param name="mapPoint">当前点</param>
        /// <returns></returns>
        public bool PointIsInPolygon(MapPoint[] mapPoints, MapPoint mapPoint)
        {
            if (mapPoints.Length < 3)
            {
                throw new ArgumentException("多边形顶点必须大于3个!");
            }

            int j = 0, count = 0;
            for (int i = 0; i < mapPoints.Length; i++)
            {
                j = (i == mapPoints.Length - 1) ? 0 : j + 1;
                if ((mapPoints[i].Lat != mapPoints[j].Lat) &&
                    (((mapPoint.Lat >= mapPoints[i].Lat) &&
                    (mapPoint.Lat < mapPoints[j].Lat)) ||
                    ((mapPoint.Lat >= mapPoints[j].Lat) &&
                    (mapPoint.Lat < mapPoints[i].Lat))) &&
                    (mapPoint.Lng < (mapPoints[j].Lng - mapPoints[i].Lng) * (mapPoint.Lat - mapPoints[i].Lat) / (mapPoints[j].Lat - mapPoints[i].Lat) + mapPoints[i].Lng))
                {
                    count++;
                }
            }
            return (count % 2 > 0) ? true : false;
        }
        /// <summary>
        /// 判断点是否在线段上 A->B
        /// </summary>
        /// <param name="mapStartPoint">线段开始坐标</param>
        /// <param name="mapEndPoint">线段结束坐标</param>
        /// <param name="mapPoint">当前点</param>
        /// <param name="offset">偏移 0-10 数值越大表示允许的误差越大 0表示绝对精确 默认1</param>
        /// <returns></returns>
        public bool PointIsInLineSegment(MapPoint mapStartPoint, MapPoint mapEndPoint, MapPoint mapPoint, double offset = 1)
        {
            //result>0 在线的左边 result<0在线的右边 result=0 在线上
            var result = ((mapStartPoint.Lng - mapPoint.Lng) * (mapEndPoint.Lat - mapPoint.Lat) - (mapEndPoint.Lng - mapPoint.Lng) * (mapStartPoint.Lat - mapPoint.Lat)) * 1000000;
            var absResult = Math.Abs(result);
            if (absResult < offset)
            {
                //如果已经确定点在线上了(这里仅仅是确定是在这个线上,线是无线延长的 故还不是确定在线段里面) 这里还需要确定一下 这个点是否是在这两个点组成的矩形里面
                return PointIsInRect(mapStartPoint, mapEndPoint, mapPoint);
            }

            return false;
        }
        /// <summary>
        /// 判断点是否在线上 A->B->C->D
        /// </summary>
        /// <param name="mapPoints">线的点集合</param>
        /// <param name="mapPoint">当前点</param>
        /// <param name="offset">偏移 0-10 数值越大表示允许的误差越大 0表示绝对精确 默认1</param>
        /// <returns></returns>
        public bool PointIsInLine(MapPoint[] mapPoints, MapPoint mapPoint, double offset = 1)
        {
            if (mapPoints.Length < 2)
            {
                throw new ArgumentException("线上的点必须大于2个!");
            }

            //取出线上的每一段出来匹配 如果只要有点在其中一段上 那么说明点在线上
            for (int i = 0, j = 1; i < mapPoints.Length - 1; i++, j++)
            {
                if (PointIsInLineSegment(mapPoints[i], mapPoints[j], mapPoint, offset))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否超出中国范围
        /// </summary>
        /// <param name="mapPoint"></param>
        /// <returns></returns>
        public bool IsOutOfChina(MapPoint mapPoint)
        {
            if (mapPoint.Lng < 72.004 || mapPoint.Lng > 137.8347)
                return true;
            if (mapPoint.Lat < 0.8293 || mapPoint.Lat > 55.8271)
                return true;
            return false;
        }

        #endregion
    }
}
