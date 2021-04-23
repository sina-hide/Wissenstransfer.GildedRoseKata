using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class Program
    {
        private const string DexterityVest = "+5 Dexterity Vest";
        private const string AgedBrie = "Aged Brie";
        private const string ElixirOfTheMongoose = "Elixir of the Mongoose";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string Backstage = "Backstage passes to a TAFKAL80ETC concert";
        private const string Conjured = "Conjured Mana Cake";

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
                    new Item { Name = Backstage, SellIn = 15, Quality = 20 },
                    new Item { Name = Conjured, SellIn = 3, Quality = 6 }
                }
            };

            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItem(item);
            }
        }

        private static void UpdateItem(Item item)
        {
            var updater = SelectItemUpdater(item);
            updater.UpdateItem(item);
        }

        private static readonly IDictionary<string, ItemUpdater> ItemUpdaterMap =
            CreateItemUpdaterMap();

        private static readonly ItemUpdater DefaultUpdater =
            new DefaultItemUpdater();

        private static IDictionary<string,ItemUpdater> CreateItemUpdaterMap()
        {
            return new Dictionary<string, ItemUpdater>
            {
                { AgedBrie, new AgedBrieUpdater() },
                { Sulfuras, new SulfurasUpdater() },
                { Backstage, new BackstageUpdater() },
                { Conjured, new ConjuredUpdater() },
            };
        }

        private static ItemUpdater SelectItemUpdater(Item item)
        {
            var found = ItemUpdaterMap.TryGetValue(item.Name, out var updater);
            return found ? updater : DefaultUpdater;
        }
    }

    public abstract class ItemUpdater
    {
        public abstract void UpdateItem(Item item);
    }

    public class AgedBrieUpdater : ItemUpdater
    {
        public override void UpdateItem(Item item)
        {
            item.IncrementItemQuality();

            item.DecrementItemSellIn();

            if (item.SellIn < 0)
            {
                item.IncrementItemQuality();
            }
        }
    }

    public class SulfurasUpdater : ItemUpdater
    {
        public override void UpdateItem(Item item)
        {
            // Sulfuras doesn't lose or gain quality; and its sellIn does not
            // change.
        }
    }

    public class BackstageUpdater : ItemUpdater
    {
        public override void UpdateItem(Item item)
        {
            item.IncrementItemQuality();

            if (item.SellIn < 11)
            {
                item.IncrementItemQuality();
            }

            if (item.SellIn < 6)
            {
                item.IncrementItemQuality();
            }

            item.DecrementItemSellIn();

            if (item.SellIn < 0)
            {
                item.DropItemQualityToZero();
            }
        }
    }

    public class ConjuredUpdater : ItemUpdater
    {
        public override void UpdateItem(Item item)
        {
            item.DecrementItemQuality();
            item.DecrementItemQuality();

            item.DecrementItemSellIn();

            if (item.SellIn < 0)
            {
                item.DecrementItemQuality();
                item.DecrementItemQuality();
            }
        }
    }

    public class DefaultItemUpdater : ItemUpdater
    {
        public override void UpdateItem(Item item)
        {
            item.DecrementItemQuality();

            item.DecrementItemSellIn();

            if (item.SellIn < 0)
            {
                item.DecrementItemQuality();
            }
        }
    }

    public static class ItemExtensions
    {
        public static void IncrementItemQuality(this Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }

        public static void DecrementItemQuality(this Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality -= 1;
            }
        }

        public static void DropItemQualityToZero(this Item item)
        {
            item.Quality = 0;
        }

        public static void DecrementItemSellIn(this Item item)
        {
            item.SellIn -= 1;
        }
    }

    public class Item // Don't change this class!
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
