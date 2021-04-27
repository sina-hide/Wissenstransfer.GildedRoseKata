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

        [Theory]
        [InlineData("Elixir of the Mongoose")]
        [InlineData("Aged Brie")]
        [InlineData("Backstage passes to a TAFKAL80ETC concert")]
        [InlineData("Conjured Mana Cake")]
        public void SellInDecreases(string name) =>
            AssertSellInAfterUpdate(
                9,
                new Item { Name = name, SellIn = 10, Quality = 20 });

        [Fact]
        public void QualityDecreasesTwiceAsFastForStandardItemOnceSellDateHasPassed() =>
            AssertQualityAfterUpdate(
                18,
                new Item { Name = "Elixir of the Mongoose", SellIn = -1, Quality = 20 });

        [Theory]
        [InlineData("Elixir of the Mongoose", 10, 0)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 0, 0)]
        [InlineData("Conjured Mana Cake", 10, 0)]
        public void QualityIsNeverNegative(string name, int sellIn, int quality) =>
            AssertQualityAfterUpdateIsNotNegative(
                new Item { Name = name, SellIn = sellIn, Quality = quality });

        [Theory]
        [InlineData("Aged Brie", 10, 50)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 12, 50)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 7, 49)]
        [InlineData("Backstage passes to a TAFKAL80ETC concert", 2, 48)]
        public void QualityIsNeverGreaterThan50(string name, int sellIn, int quality) =>
            AssertQualityAfterUpdateIsNotMoreThan50(
                new Item { Name = name, SellIn = sellIn, Quality = quality });

        [Fact]
        public void AgedBrieIncreasesInQuality() =>
            AssertQualityAfterUpdate(
                11,
                new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 });

        [Fact]
        public void AgedBrieIncreasesInQualityTwiceAsFastOnceSellDateHasPassed() =>
            AssertQualityAfterUpdate(
                12,
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 });

        [Fact]
        public void SulfurasNeverHasToBeSold() =>
            AssertSellInAfterUpdate(
                10,
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 });

        [Fact]
        public void SulfurasNeverDecreasesInQuality() =>
            AssertQualityAfterUpdate(
                80,
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 });

        [Theory]
        [InlineData(11, +1)]
        [InlineData(10, +2)]
        [InlineData(6, +2)]
        [InlineData(5, +3)]
        [InlineData(1, +3)]
        public void BackstageQualityIncreasesAsSellByApproaches(int sellIn, int qualityChange)
        {
            var quality = 10;
            AssertQualityAfterUpdate(
                quality + qualityChange,
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality });
        }

        [Fact]
        public void BackstageQualityDropsTo0AfterConcert()
        {
            AssertQualityAfterUpdate(
                0,
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 });
        }

        private static void AssertQualityAfterUpdate(int expectedQuality, Item item)
        {
            UpdateItemQuality(item);
            Assert.Equal(expectedQuality, item.Quality);
        }

        private static void AssertQualityAfterUpdateIsNotNegative(Item item)
        {
            UpdateItemQuality(item);
            Assert.False(item.Quality < 0);
        }

        private static void AssertQualityAfterUpdateIsNotMoreThan50(Item item)
        {
            UpdateItemQuality(item);
            Assert.False(item.Quality > 50);
        }

        private static void AssertSellInAfterUpdate(int expectedSellIn, Item item)
        {
            UpdateItemQuality(item);
            Assert.Equal(expectedSellIn, item.SellIn);
        }

        private static void UpdateItemQuality(Item item)
        {
            var program = new Program(item);
            program.UpdateItems();
        }
    }
}
