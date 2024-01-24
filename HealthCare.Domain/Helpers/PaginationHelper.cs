using HealthCare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Domain.Helpers
{
    public class PaginationHelper<T>
    {
        private List<T> pages { get; set; }
        private int pageNum { get; set; } = 0;
        public PaginationHelper(List<T> pages)
        {
            this.pages = pages;
        }

        public T ChangePage(int offest)
        {
            if (IndexOffsetAllowed(offest)){
                pageNum += offest;
            }
            return GetCurrentPage();
        }

        public T GetCurrentPage()
        {
            return pages[pageNum];
        }

        public bool IndexOffsetAllowed(int offset)
        {
            var newIndex = pageNum + offset;
            return (newIndex > -1 && newIndex < pages.Count);
        }



    }
}
