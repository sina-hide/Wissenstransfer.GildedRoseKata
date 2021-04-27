using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GildedRose.Console
{
    public class Program
    {
        public Program(params Item[] items)
        {
            Items = items;
        }

        IList<Item> Items; // Don't change this field!

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = ItemName.DexterityVest, SellIn = 10, Quality = 20 },
                    new Item { Name = ItemName.AgedBrie, SellIn = 2, Quality = 0 },
                    new Item { Name = ItemName.ElixirOfTheMongoose, SellIn = 5, Quality = 7 },
                    new Item { Name = ItemName.Sulfuras, SellIn = 0, Quality = 80 },
                    new Item { Name = ItemName.BackstagePasses, SellIn = 15, Quality = 20 },
                    new Item { Name = ItemName.ConjuredManaCake, SellIn = 3, Quality = 6 },
                },
            };

            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                item.Update();
            }
        }

        public static AgingStrategy SelectAgingStrategy(Item item) =>
            CreateAgingStrategies()
                .First(strategy => strategy.CanHandle(item.Name));

        private static IEnumerable<AgingStrategy> CreateAgingStrategies() =>
            from type in Assembly.GetExecutingAssembly().GetTypes()
            let attribute = type.GetCustomAttribute<AgingStrategyAttribute>()
            where attribute != null
            orderby attribute.IsDefault
            select (AgingStrategy)Activator.CreateInstance(type);

        [AttributeUsage(AttributeTargets.Class)]
        public class AgingStrategyAttribute : Attribute
        {
            public bool IsDefault { get; set; } = false;
        }

        public abstract class AgingStrategy
        {
            public abstract bool CanHandle(string name);

            public abstract int GetQualityChange(int sellIn, int quality);

            public abstract bool IsAging { get; }

            protected static int GetStandardQualityChange(int sellIn) =>
                sellIn <= 0 ? -2 : -1;
        }

        [AgingStrategy]
        private class AgedBrieAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => name == ItemName.AgedBrie;

            public override int GetQualityChange(int sellIn, int quality) =>
                -GetStandardQualityChange(sellIn);

            public override bool IsAging => true;
        }

        [AgingStrategy]
        private class SulfurasAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => name == ItemName.Sulfuras;

            public override int GetQualityChange(int sellIn, int quality) => 0;

            public override bool IsAging => false;
        }

        [AgingStrategy]
        private class BackstagePassesAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => name == ItemName.BackstagePasses;

            public override int GetQualityChange(int sellIn, int quality) =>
                sellIn <= 0 ? -quality :
                sellIn <= 5 ? 3 :
                sellIn <= 10 ? 2 :
                1;

            public override bool IsAging => true;
        }

        [AgingStrategy(IsDefault = true)]
        private class StandardAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => true;

            public override int GetQualityChange(int sellIn, int quality) =>
                GetStandardQualityChange(sellIn);

            public override bool IsAging => true;
        }
    }

    public class Item // Don't change this class!
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    public static class ItemExtensions
    {
        public static void Update(this Item item)
        {
            var agingStrategy = Program.SelectAgingStrategy(item);

            var qualityChange = agingStrategy.GetQualityChange(item.SellIn, item.Quality);
            item.ChangeQualityBy(qualityChange);

            if (agingStrategy.IsAging)
                item.DecrementSellIn();
        }

        private static void DecrementSellIn(this Item item)
        {
            item.SellIn--;
        }

        private static void ChangeQualityBy(this Item item, int qualityChange)
        {
            if (qualityChange == 0)
                return;

            if (qualityChange > 0)
                item.IncrementQuality(qualityChange);
            else
                item.DecrementQuality(-qualityChange);
        }

        private static void IncrementQuality(this Item item, int count)
        {
            var remaining = count;
            while (remaining > 0 && item.Quality < 50)
            {
                item.Quality++;
                remaining--;
            }
        }

        private static void DecrementQuality(this Item item, int count)
        {
            var remaining = count;
            while (remaining > 0 && item.Quality > 0)
            {
                item.Quality--;
                remaining--;
            }
        }
    }
}
