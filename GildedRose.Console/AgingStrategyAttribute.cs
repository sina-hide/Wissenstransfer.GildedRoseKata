using System;

namespace GildedRose.Console
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AgingStrategyAttribute : Attribute
    {
        public bool IsDefault { get; set; } = false;
    }
}
