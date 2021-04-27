namespace GildedRose.Console
{
    [AgingStrategy]
    internal class AgedBrieAgingStrategy : AgingStrategy
    {
        public override bool CanHandle(string name) => name == ItemName.AgedBrie;

        public override int GetQualityChange(int sellIn, int quality) =>
            -GetStandardQualityChange(sellIn);

        public override bool IsAging => true;
    }
}
