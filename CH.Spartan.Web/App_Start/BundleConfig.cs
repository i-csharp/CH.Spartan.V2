﻿using System.Threading;
using System.Web.Optimization;
using Abp.Dependency;
using Abp.Localization;

namespace CH.Spartan.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var currentLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            bundles.IgnoreList.Clear();

            #region 基础

            bundles.Add(
                new StyleBundle("~/styles")
                    .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/plugins/flags/famfamfam-flags.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/animate.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/style.min.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/base.css", new CssRewriteUrlTransform())
                    .Include("~/Content/css/main.css", new CssRewriteUrlTransform())
                );


            bundles.Add(
                new ScriptBundle("~/scripts/top")
                    .Include(
                        "~/Content/js/jquery.min.js",
                        "~/Content/js/bootstrap.min.js",
                        "~/Content/js/json2.min.js",
                        "~/Content/js/plugins/linq/jquery.linq.js",
                        "~/Content/js/plugins/layer/layer.min.js",
                        "~/Content/js/plugins/knockout/knockout.min.js",
                        "~/Content/js/plugins/knockout/knockout.mapping.js",
                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.spartan.js"
                    )
                );

            bundles.Add(
                new ScriptBundle("~/scripts/bottom")
                    .Include(
                        "~/Content/js/contabs.min.js",
                        "~/Content/js/plugins/pace/pace.min.js",
                        "~/Content/js/plugins/metisMenu/jquery.metisMenu.js",
                        "~/Content/js/plugins/slimscroll/jquery.slimscroll.min.js",
                        "~/Content/js/hplus.min.js",
                        "~/Content/js/content.min.js",
                        "~/Content/js/main.js"
                    )
                );
            #endregion

            #region 模块
            //list
            bundles.Add(
                new StyleBundle("~/styles/plugins/list")
                .Include("~/Content/js/plugins/table/bootstrap-table.css", new CssRewriteUrlTransform())
                .Include("~/Content/js/plugins/select2/css/select2.css", new CssRewriteUrlTransform())
                );

            bundles.Add(
                new ScriptBundle("~/scripts/plugins/list")
                    .Include(
                        "~/Content/js/plugins/table/bootstrap-table.js"
                    ).Include(
                        "~/Content/js/plugins/table/locale/bootstrap-table-" + currentLanguage + ".js"
                    ).Include(
                        "~/Content/js/plugins/table/extensions/export/bootstrap-table-export.js"
                    ).Include(
                        "~/Content/js/plugins/table/extensions/export/tableExport.js"
                    ).Include(
                       "~/Content/js/plugins/my97/wdatepicker.js"
                   ).Include(
                       "~/Content/js/plugins/select2/js/select2.full.js"
                   )
                );

            //edit
            bundles.Add(
                new StyleBundle("~/styles/plugins/edit")
                     .Include("~/Content/js/plugins/select2/css/select2.css", new CssRewriteUrlTransform())
                     .Include("~/Content/js/plugins/icheck/skins/all.css", new CssRewriteUrlTransform())
                );

            bundles.Add(
               new ScriptBundle("~/scripts/plugins/edit")
                   .Include(
                       "~/Content/js/plugins/validation/jquery.validate.js"
                   ).Include(
                       "~/Content/js/plugins/validation/localization/messages_" + currentLanguage + ".js"
                   ).Include(
                       "~/Content/js/plugins/validation/jquery.validate.default.js"
                   ).Include(
                       "~/Content/js/plugins/my97/wdatepicker.js"
                   ).Include(
                       "~/Content/js/plugins/select2/js/select2.full.js"
                   ).Include(
                       "~/Content/js/plugins/icheck/icheck.js"
                   ).Include(
                       "~/Content/js/plugins/icheck/icheck.default.js"
                   )
               );

            #endregion

            #region 插件
            //drag
            bundles.Add(
             new ScriptBundle("~/scripts/plugins/drag")
                 .Include(
                     "~/Content/js/plugins/drag/jquery.event.drag.js"
                 ).Include(
                     "~/Content/js/plugins/drag/jquery.event.drop.js"
                 ).Include(
                     "~/Content/js/plugins/drag/jquery.event.default.js"
                 )
             );

            //select
            bundles.Add(
              new StyleBundle("~/styles/plugins/select")
              .Include("~/Content/js/plugins/select2/css/select2.css", new CssRewriteUrlTransform())
              );

            bundles.Add(
                new ScriptBundle("~/scripts/plugins/select")
                    .Include(
                        "~/Content/js/plugins/select2/js/select2.full.js"
                    )
                );

            //date
            bundles.Add(
                new ScriptBundle("~/scripts/plugins/date")
                    .Include(
                       "~/Content/js/plugins/my97/wdatepicker.js"
                   )
                );

            //cropper
            bundles.Add(
                new StyleBundle("~/styles/plugins/cropper")
                    .Include("~/Content/js/plugins/cropper/cropper.css", new CssRewriteUrlTransform())
            );


            bundles.Add(
               new ScriptBundle("~/scripts/plugins/cropper")
                   .Include(
                      "~/Content/js/plugins/cropper/cropper.js"
                  )
               );

            //highcharts
            bundles.Add(
               new ScriptBundle("~/scripts/plugins/highcharts")
                   .Include(
                      "~/Content/js/plugins/highcharts/highcharts.js"
                  )
               );

            //highstock
            bundles.Add(
               new ScriptBundle("~/scripts/plugins/highstock")
                   .Include(
                      "~/Content/js/plugins/highstock/js/highstock.js"
                  )
               );


            //ionslider
            bundles.Add(
                new StyleBundle("~/styles/plugins/ionslider")
                    .Include("~/Content/js/plugins/ionslider/css/ion.rangeSlider.css", new CssRewriteUrlTransform())
                    .Include("~/Content/js/plugins/ionslider/css/ion.rangeSlider.skinNice.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
               new ScriptBundle("~/scripts/plugins/ionslider")
                   .Include(
                      "~/Content/js/plugins/ionslider/js/ion.rangeSlider.js"
                  )
               );


            bundles.Add(
            new StyleBundle("~/styles/plugins/icheck")
                 .Include("~/Content/js/plugins/icheck/skins/all.css", new CssRewriteUrlTransform())
            );

            bundles.Add(
               new ScriptBundle("~/scripts/plugins/icheck")
                   .Include(
                       "~/Content/js/plugins/icheck/icheck.js"
                   ).Include(
                       "~/Content/js/plugins/icheck/icheck.default.js"
                   )
               );

            #endregion


        }
    }
}