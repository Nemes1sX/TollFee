using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TollFee.Api.Models;

namespace TollFee.Api.Services
{
    public class TollFreeService
    {
        private readonly TollDBContext _dbContext;

        public TollFreeService(TollDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal IEnumerable<DateTime> RemoveFree(DateTime[] passages)
        {
            var OtherFreeDays = FreeDays();

            foreach (var p in passages)
            {
                if (p.DayOfWeek != DayOfWeek.Saturday && p.DayOfWeek != DayOfWeek.Sunday && !OtherFreeDays.Contains(p.Date) && p.Month != 7)
                    yield return p;
            }
        }

       private  DateTime[] FreeDays()
        {
                return _dbContext.TollFrees.Select(x => x.Date).ToArray();  

        }

    }
}
