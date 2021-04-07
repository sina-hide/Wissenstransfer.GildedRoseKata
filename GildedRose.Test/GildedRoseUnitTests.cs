using GildedRose.Console;
using Xunit;

namespace GildedRose.Test
{
    public class GildedRoseUnitTests
    {
        [Fact]
        public void TestQuality()
        {
            var name = "Elixir of the Mongoose";
            var sellIn = 10;
            var quality = 20;

            var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
            var program = new Program(item);
            program.UpdateQuality();

            var actualQuality = item.Quality;

            var expectedQuality = 19;

            Assert.Equal(expectedQuality, actualQuality);
        }

        [Fact]
        public void TestSellIn()
        {
            var name = "Elixir of the Mongoose";
            var sellIn = 10;
            var quality = 20;

            var item = new Item { Name = name, SellIn = sellIn, Quality = quality };
            var program = new Program(item);
            program.UpdateQuality();

            var actualSellIn = item.SellIn;

            var expectedSellIn = 9;

            Assert.Equal(expectedSellIn, actualSellIn);
        }
    }
}
