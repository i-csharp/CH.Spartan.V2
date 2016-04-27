using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CH.Spartan.Maps;

namespace CH.Spartan.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly MapManager _mapManager;
        public MapController(MapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public JsonResult GetLocation(MapPoint point)
        {
            var result = _mapManager.GetLocation(point);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}