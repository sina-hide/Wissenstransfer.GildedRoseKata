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
            var updater = SelectUpdater(item);
            updater.UpdateItemQuality(item);
        }

        private static Updater SelectUpdater(Item item) =>
            CreateUpdaters().First(updater => updater.CanHandle(item.Name));

        private static IEnumerable<Updater> CreateUpdaters() =>
            from type in Assembly.GetExecutingAssembly().GetTypes()
            let updaterAttribute = type.GetCustomAttribute<UpdaterAttribute>()
            where updaterAttribute != null
            orderby updaterAttribute.IsDefault
            select (Updater)Activator.CreateInstance(type);

        [AttributeUsage(AttributeTargets.Class)]
        public class UpdaterAttribute : Attribute
        {
            public bool IsDefault { get; set; } = false;
        }

        private abstract class Updater
        {
            public abstract bool CanHandle(string name);

            public abstract void UpdateItemQuality(Item item);
        }

        [Updater]
        private class AgedBrieUpdater : Updater
        {
            public override bool CanHandle(string name) => name == AgedBrie;

            public override void UpdateItemQuality(Item item)
            {
                if (item.Quality < 50)
                {
                    item.Quality++;
                }

                item.SellIn--;

                if (item.SellIn < 0)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;
                    }
                }
            }
        }

        [Updater]
        private class SulfurasUpdater : Updater
        {
            public override bool CanHandle(string name) => name == Sulfuras;

            public override void UpdateItemQuality(Item item)
            {
                // Sulfuras' quality and its sellIn never change.
            }
        }

        [Updater]
        private class BackstagePassesUpdater : Updater
        {
            public override bool CanHandle(string name) => name == BackstagePasses;

            public override void UpdateItemQuality(Item item)
            {
                if (item.Quality < 50)
                {
                    item.Quality++;

                    if (item.SellIn < 11)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality++;
                        }
                    }

                    if (item.SellIn < 6)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality++;
                        }
                    }
                }

                item.SellIn--;

                if (item.SellIn < 0)
                {
                    item.Quality = 0;
                }
            }
        }

        [Updater(IsDefault = true)]
        private class StandardUpdater : Updater
        {
            public override bool CanHandle(string name) => true;

            public override void UpdateItemQuality(Item item)
            {
                if (item.Quality > 0)
                {
                    item.Quality--;
                }

                item.SellIn--;

                if (item.SellIn < 0)
                {
                    if (item.Quality > 0)
                    {
                        item.Quality--;
                    }
                }
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
