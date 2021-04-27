namespace GildedRose.Console
{
    [AgingStrategy]
    internal class BackstagePassesAgingStrategy : AgingStrategy
    {
        public override bool CanHandle(string name) => name == ItemName.BackstagePasses;

        public override int GetQualityChange(int sellIn, int quality) =>
            sellIn <= 0 ? -quality :
            sellIn <= 5 ? 3 :
            sellIn <= 10 ? 2 :
            1;

        public override bool IsAging => true;
    }
}
