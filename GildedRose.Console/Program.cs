using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GildedRose.Console
{
    public class Program
    {
        private const string DexterityVest = "+5 Dexterity Vest";
        private const string AgedBrie = "Aged Brie";
        private const string ElixirOfTheMongoose = "Elixir of the Mongoose";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string ConjuredManaCake = "Conjured Mana Cake";

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
                    new Item { Name = DexterityVest, SellIn = 10, Quality = 20 },
                    new Item { Name = AgedBrie, SellIn = 2, Quality = 0 },
                    new Item { Name = ElixirOfTheMongoose, SellIn = 5, Quality = 7 },
                    new Item { Name = Sulfuras, SellIn = 0, Quality = 80 },
                    new Item { Name = BackstagePasses, SellIn = 15, Quality = 20 },
                    new Item { Name = ConjuredManaCake, SellIn = 3, Quality = 6 },
                },
            };

            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItemQuality(item);
            }
        }

        private static void UpdateItemQuality(Item item)
        {
            var agingStrategy = SelectAgingStrategy(item);
            agingStrategy.UpdateItemQuality(item);
        }

        private static AgingStrategy SelectAgingStrategy(Item item) =>
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

        private abstract class AgingStrategy
        {
            public abstract bool CanHandle(string name);

            public abstract void UpdateItemQuality(Item item);

            protected static void DecrementSellIn(Item item)
            {
                item.SellIn--;
            }

            protected static void IncrementQuality(Item item)
            {
                if (item.Quality < 50)
                {
                    item.Quality++;
                }
            }

            protected static void DecrementQuality(Item item)
            {
                if (item.Quality > 0)
                {
                    item.Quality--;
                }
            }
        }

        [AgingStrategy]
        private class AgedBrieAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => name == AgedBrie;

            public override void UpdateItemQuality(Item item)
            {
                IncrementQuality(item);

                if (item.SellIn <= 0)
                {
                    IncrementQuality(item);
                }

                DecrementSellIn(item);
            }
        }

        [AgingStrategy]
        private class SulfurasAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => name == Sulfuras;

            public override void UpdateItemQuality(Item item)
            {
                // Sulfuras' quality and its sellIn never change.
            }
        }

        [AgingStrategy]
        private class BackstagePassesAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => name == BackstagePasses;

            public override void UpdateItemQuality(Item item)
            {
                IncrementQuality(item);

                if (item.SellIn < 11)
                {
                    IncrementQuality(item);
                }

                if (item.SellIn < 6)
                {
                    IncrementQuality(item);
                }

                if (item.SellIn <= 0)
                {
                    item.Quality = 0;
                }

                DecrementSellIn(item);
            }
        }

        [AgingStrategy(IsDefault = true)]
        private class StandardAgingStrategy : AgingStrategy
        {
            public override bool CanHandle(string name) => true;

            public override void UpdateItemQuality(Item item)
            {
                DecrementQuality(item);

                if (item.SellIn <= 0)
                {
                    DecrementQuality(item);
                }

                DecrementSellIn(item);
            }
        }
    }

    public class Item // Don't change this class!
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
