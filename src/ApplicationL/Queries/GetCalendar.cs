using ApplicationL.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApplicationL.Queries
{
    internal class GetCalendar
    {
        IReadCalendar _readCalendar;

        internal GetCalendar(IReadCalendar readCalendar)
        {
            _readCalendar = readCalendar;
        }

        internal IEnumerable<Calendar> CalendarDays()
        {
            IEnumerable<Calendar> calendarDays = null;

            var calendarData = _readCalendar.ReadCaledarData();
            if(calendarData != null) calendarDays = JsonSerializer.Deserialize<IEnumerable<Calendar>>(calendarData);

            return calendarDays;
        }
    }
}
