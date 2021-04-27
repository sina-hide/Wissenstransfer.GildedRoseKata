using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GildedRose.Console
{
    public static class AgingStrategySelector
    {
        public static AgingStrategy SelectAgingStrategy(Item item) =>
            CreateAgingStrategies()
                .First(strategy => strategy.CanHandle(item.Name));

        private static IEnumerable<AgingStrategy> CreateAgingStrategies() =>
            from type in Assembly.GetExecutingAssembly().GetTypes()
            let attribute = type.GetCustomAttribute<AgingStrategyAttribute>()
            where attribute != null
            orderby attribute.IsDefault
            select (AgingStrategy)Activator.CreateInstance(type);
    }
}
