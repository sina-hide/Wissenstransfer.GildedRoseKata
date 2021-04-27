using System;
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
                    new Item { Name = ItemName.DexterityVest, SellIn = 10, Quality = 20 },
                    new Item { Name = ItemName.AgedBrie, SellIn = 2, Quality = 0 },
                    new Item { Name = ItemName.ElixirOfTheMongoose, SellIn = 5, Quality = 7 },
                    new Item { Name = ItemName.Sulfuras, SellIn = 0, Quality = 80 },
                    new Item { Name = ItemName.BackstagePasses, SellIn = 15, Quality = 20 },
                    new Item { Name = ItemName.ConjuredManaCake, SellIn = 3, Quality = 6 },
                },
            };

            app.UpdateItems();

            System.Console.ReadKey();
        }

        [Obsolete("use UpdateItems")]
        public void UpdateQuality()  // retain name for backward compatibility
        {
            UpdateItems();
        }

        public void UpdateItems()
        {
            Items.Update();
        }
    }

    public class Item // Don't change this class!
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }
}
