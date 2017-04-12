using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMusicBand
{
    public class PagingInfo
    {

        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get{return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);}
        }

        public PagingInfo(int totalItem, int itemPerPage, int currentPage)
        {
            TotalItems = totalItem;
            CurrentPage = currentPage;
            if (totalItem < itemPerPage)
            {
                ItemsPerPage = totalItem;
            }
            else
            {
                ItemsPerPage = itemPerPage;
            }
        }
    }
}


