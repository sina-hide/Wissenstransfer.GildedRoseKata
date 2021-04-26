using System.Collections.Generic;

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
                switch (item.Name)
                {
                    case AgedBrie:
                        UpdateAgedBrieItemQuality(item);
                        break;

                    case Sulfuras:
                        UpdateSulfurasItemQuality(item);
                        break;

                    case BackstagePasses:
                        UpdateBackstagePassesItemQuality(item);
                        break;

                    default:
                        UpdateStandardItemQuality(item);
                        break;
                }
            }
        }

        private static void UpdateAgedBrieItemQuality(Item item)
        {
            UpdateItemQuality(item);
        }

        private static void UpdateSulfurasItemQuality(Item item)
        {
            UpdateItemQuality(item);
        }

        private static void UpdateBackstagePassesItemQuality(Item item)
        {
            UpdateItemQuality(item);
        }

        private static void UpdateStandardItemQuality(Item item)
        {
            UpdateItemQuality(item);
        }

        private static void UpdateItemQuality(Item item)
        {
            var name = item.Name;

            if (name != AgedBrie &&
                name != BackstagePasses)
            {
                if (item.Quality > 0)
                {
                    if (name != Sulfuras)
                    {
                        item.Quality--;
                    }
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality++;

                    if (name == BackstagePasses)
                    {
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
                }
            }

            if (name != Sulfuras)
            {
                item.SellIn--;
            }

            if (item.SellIn < 0)
            {
                if (name != AgedBrie)
                {
                    if (name != BackstagePasses)
                    {
                        if (item.Quality > 0)
                        {
                            if (name != Sulfuras)
                            {
                                item.Quality--;
                            }
                        }
                    }
                    else
                    {
                        item.Quality = 0;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;
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
