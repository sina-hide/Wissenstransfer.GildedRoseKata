namespace GildedRose.Console
{
    [AgingStrategy]
    public class ConjuredManaCakeAgingStrategy : AgingStrategy
    {
        public override bool CanHandle(string name) => name == ItemName.ConjuredManaCake;

        public override int GetQualityChange(int sellIn, int quality) =>
            2 * GetStandardQualityChange(sellIn);

        public override bool IsAging => true;
    }
}
