namespace GildedRose.Console
{
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
