namespace GildedRose.Console
{
    public abstract class AgingStrategy
    {
        public abstract bool CanHandle(string name);

        public abstract int GetQualityChange(int sellIn, int quality);

        public abstract bool IsAging { get; }

        protected static int GetStandardQualityChange(int sellIn) =>
            sellIn <= 0 ? -2 : -1;
    }
}
