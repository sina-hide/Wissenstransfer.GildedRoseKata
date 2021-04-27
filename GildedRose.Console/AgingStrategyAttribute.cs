using System;
using JetBrains.Annotations;

namespace GildedRose.Console
{
    [AttributeUsage(AttributeTargets.Class)]
    [MeansImplicitUse]
    public class AgingStrategyAttribute : Attribute
    {
        public bool IsDefault { get; set; } = false;
    }
}
