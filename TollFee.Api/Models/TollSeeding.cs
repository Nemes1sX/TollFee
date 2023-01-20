using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TollFee.Api.Models
{
    public class TollSeeding
    {
        private readonly TollDBContext _context;

        public TollSeeding(TollDBContext context)
        {
            _context = context;
        }

        public void SeedData(int year)
        {
            var seedingYearExists = _context.TollFrees.Where(x => x.Year == year).ToList();

            if (seedingYearExists.Any())
            {
                return;
            }

            var holidayYearList = NonRestConstHolidayDays(year);

            /*await*/ _context.TollFrees.AddRange(holidayYearList);
            /*await*/ _context.SaveChanges();
        }

        private DateTime EasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }

        private List<TollFree> NonRestConstHolidayDays(int year)
        {
            DateTime[] OtherFreeDays = new[]
           {
            new DateTime(year, 1, 1),
            new DateTime(year, 1, 5),
            new DateTime(year, 1, 6),
            new DateTime(year, 4, 30),
            new DateTime(year, 5, 1),
            new DateTime(year, 6, 6),
            new DateTime(year, 6, 24),
            new DateTime(year, 6, 25),
            new DateTime(year, 6, 26),
            new DateTime(year, 12, 23),
            new DateTime(year, 12, 24),
            new DateTime(year, 12, 25),
            new DateTime(year, 12, 26),
            new DateTime(year, 12, 31)
            };
            var NonRestFreeDays = new List<TollFree>();
            foreach (var OthreFreeDay in OtherFreeDays)
            {
                if (OthreFreeDay.DayOfWeek != DayOfWeek.Saturday || OthreFreeDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    NonRestFreeDays.Add(new TollFree
                    {
                        Year = OthreFreeDay.Year,
                        Date = OthreFreeDay.Date,
                    });
                }
            }

            var EasterTenureHolidays = new List<TollFree>
            {
               new TollFree
               {
                   Year= year,
                   Date =  EasterSunday(year).AddDays(-3)
               },
               new TollFree
               {
                   Year = year,
                   Date = EasterSunday(year).AddDays(-2)
               },
               new TollFree
               {
                   Year = year,
                   Date = EasterSunday(year).AddDays(1)
               },
               new TollFree
               {
                   Year = year,
                   Date = EasterSunday(year).AddDays(38)
               },
               new TollFree
               {
                   Year = year,
                   Date = EasterSunday(year).AddDays(39)
               },
            };

            NonRestFreeDays.AddRange(EasterTenureHolidays);

            return NonRestFreeDays;

        }
    }
}