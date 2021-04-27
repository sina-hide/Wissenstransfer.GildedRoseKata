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
            var agingStrategy = AgingStrategySelector.SelectAgingStrategy(item);

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
