using Proiect_Online_Shopping_Anania_Anamaria.Database;
using Proiect_Online_Shopping_Anania_Anamaria.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Proiect_Online_Shopping_Anania_Anamaria.Models.HOME
{
    public class HOMEIndexViewModels
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        AEOnlineShoppingEntities context = new AEOnlineShoppingEntities();
        public IPagedList<Product> ListOfProducts { get; set; }
        public HOMEIndexViewModels CreateModel(string search,int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<Product> data = context.Database.SqlQuery<Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HOMEIndexViewModels
            {
                ListOfProducts = data
            };
        }
    }
}