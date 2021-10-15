using System.Web;
using System.Web.Mvc;

namespace Proiect_Online_Shopping_Anania_Anamaria
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
