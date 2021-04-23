using System.Collections.Generic;

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
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                }
            };

            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != "Sulfuras, Hand of Ragnaros")
                        {
                            item.DecrementItemQuality();
                        }
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.IncrementItemQuality();

                        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (item.SellIn < 11)
                            {
                                if (item.Quality < 50)
                                {
                                    item.IncrementItemQuality();
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (item.Quality < 50)
                                {
                                    item.IncrementItemQuality();
                                }
                            }
                        }
                    }
                }

                if (item.Name != "Sulfuras, Hand of Ragnaros")
                {
                    item.DecrementItemSellIn();
                }

                if (item.SellIn < 0)
                {
                    if (item.Name != "Aged Brie")
                    {
                        if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (item.Quality > 0)
                            {
                                if (item.Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    item.DecrementItemQuality();
                                }
                            }
                        }
                        else
                        {
                            item.DropItemQualityToZero();
                        }
                    }
                    else
                    {
                        if (item.Quality < 50)
                        {
                            item.IncrementItemQuality();
                        }
                    }
                }
            }
        }
    }

    public static class ItemExtensions
    {
        public static void IncrementItemQuality(this Item item)
        {
            item.Quality += 1;
        }

        public static void DecrementItemQuality(this Item item)
        {
            item.Quality -= 1;
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
