﻿using System.Web;
using System.Web.Mvc;

namespace Bai1_63135741
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
