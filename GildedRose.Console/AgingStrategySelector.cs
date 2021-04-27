using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GildedRose.Console
{
    public static class AgingStrategySelector
    {
        public static Program.AgingStrategy SelectAgingStrategy(Item item) =>
            CreateAgingStrategies()
                .First(strategy => strategy.CanHandle(item.Name));

        private static IEnumerable<Program.AgingStrategy> CreateAgingStrategies() =>
            from type in Assembly.GetExecutingAssembly().GetTypes()
            let attribute = type.GetCustomAttribute<AgingStrategyAttribute>()
            where attribute != null
            orderby attribute.IsDefault
            select (Program.AgingStrategy)Activator.CreateInstance(type);
    }
}
