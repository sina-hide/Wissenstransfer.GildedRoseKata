using System.Collections.Generic;

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
            switch (item.Name)
            {
                case AgedBrie:
                    UpdateAgedBrieItem(item);
                    break;

                case Sulfuras:
                    UpdateSulfurasItem(item);
                    break;

                case Backstage:
                    UpdateBackstageItem(item);
                    break;

                default:
                    UpdateDefaultItem(item);
                    break;
            }
        }

        private static void UpdateAgedBrieItem(Item item)
        {
            item.IncrementItemQuality();

            item.DecrementItemSellIn();

            if (item.SellIn < 0)
            {
                item.IncrementItemQuality();
            }
        }

        private static void UpdateSulfurasItem(Item item)
        {
            // Sulfuras doesn't lose or gain quality; and its sellIn does not
            // change.
        }

        private static void UpdateBackstageItem(Item item)
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

        private static void UpdateDefaultItem(Item item)
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
