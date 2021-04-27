namespace GildedRose.Console
{
    [AgingStrategy(IsDefault = true)]
    internal class StandardAgingStrategy : AgingStrategy
    {
        public override bool CanHandle(string name) => true;

        public override int GetQualityChange(int sellIn, int quality) =>
            GetStandardQualityChange(sellIn);

        public override bool IsAging => true;
    }
}
