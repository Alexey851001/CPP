using System;
using System.Collections.Generic;

namespace app.Generate
{
    public class DateGenerate : IGenerate
    {
        public object Generate()
        {
            Random random = new Random();
            DateTime start = new DateTime(2000, 1, 1, 1, 1, 1);
            int rangeSeconds = (DateTime.Today - start).Seconds;
            int rangeMinutes = (DateTime.Today - start).Minutes;
            int rangeHours = (DateTime.Today - start).Hours;
            int rangeDays = (DateTime.Today - start).Days;
            return start.AddSeconds(random.Next(rangeSeconds))
                .AddMinutes(random.Next(rangeMinutes))
                .AddHours(random.Next(rangeHours))
                .AddDays(random.Next(rangeDays));
        }

        public object GenericGenerate(Type type, Dictionary<Type, IGenerate> dictionary)
        {
            return null;
        }
    }
}