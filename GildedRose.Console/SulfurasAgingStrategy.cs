namespace GildedRose.Console
{
    [AgingStrategy]
    internal class SulfurasAgingStrategy : AgingStrategy
    {
        public override bool CanHandle(string name) => name == ItemName.Sulfuras;

        public override int GetQualityChange(int sellIn, int quality) => 0;

        public override bool IsAging => false;
    }
}
