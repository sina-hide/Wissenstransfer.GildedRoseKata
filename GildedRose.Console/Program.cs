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
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != AgedBrie &&
                    Items[i].Name != BackstagePasses)
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != Sulfuras)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == BackstagePasses)
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != Sulfuras)
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != AgedBrie)
                    {
                        if (Items[i].Name != BackstagePasses)
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != Sulfuras)
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
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
