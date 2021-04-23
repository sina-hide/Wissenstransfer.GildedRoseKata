using GildedRose.Console;
using Xunit;

namespace GildedRose.Test
{
    public class GildedRoseUnitTests
    {
        [Fact]
        public void QualityDecreasesForStandardItem() =>
            AssertQualityAfterUpdate(
                19,
                new Item { Name = "Elixir of the Mongoose", SellIn = 10, Quality = 20 });

        [Fact]
        public void SellInDecreasesForStandardItem() =>
            AssertSellInAfterUpdate(
                9,
                new Item { Name = "Elixir of the Mongoose", SellIn = 10, Quality = 20 });

        private static void AssertQualityAfterUpdate(int expectedQuality, Item item)
        {
            UpdateItemQuality(item);
            Assert.Equal(expectedQuality, item.Quality);
        }

        private static void AssertSellInAfterUpdate(int expectedSellIn, Item item)
        {
            UpdateItemQuality(item);
            Assert.Equal(expectedSellIn, item.SellIn);
        }

        private static void UpdateItemQuality(Item item)
        {
            var program = new Program(item);
            program.UpdateQuality();
        }
    }
}
