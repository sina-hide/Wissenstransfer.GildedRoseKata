# Gilded Rose Kata

This is the Gilded Rose Kata, invented by Terry Hughes
([@TerryHughes](https://twitter.com/TerryHughes)) and published by
Bobby Johnson ([@NotMyself](https://twitter.com/NotMyself)) on his
[Blog](http://iamnotmyself.com/2011/02/14/refactor-this-the-gilded-rose-kata/)
and [GitHub](https://github.com/NotMyself/GildedRose/).

This repository contains the
[non-refactored code](../main/GildedRose.Console/Program.cs) in all it's glory.
Take a look and enjoy it (you shouldn't be afraid of conditionals -- lots of
conditionals).  But before you start refactoring the mess, read the
[description of your task](kata-readme.md) (we always read the documentation
first ;-) ).

Additionally, Emily Bache collected
[lots of versions](https://github.com/emilybache/GildedRose-Refactoring-Kata/)
of this kata for different programming languages.

---

I stumbled across the Gilded Rose Kata in 2020.  I liked it, so that I wanted to
use it for a workshop about refactoring with my colleagues.  This GitHub
repository was used for the workshop, that took place on 2021-04-23.

### Branches

There are some branches in this repository.

The [`main` branch](../../tree/main) contains the non-refactored code
["Program.cs"](../main/GildedRose.Console/Program.cs), the
[task description](kata-readme.md), and a C# solution (.NET 5, C# 7.3), so that
it can be run.

The [`starting-point` branch](../../tree/starting-point) can serve as a starting
point for a workshop/kata, where the tests have to be developed by the
participants themselves.

The [`tests` branch](../../tree/tests) contains tests (xUnit) with a 100%
coverage of all lines in the function `UpdateQuality`.  It can serve as a
starting point for a workshop/kata.  This is what we did due to time
limitations.  Writing the tests could be a workshop of its own.  (Starting
without the tests for conjured items would be better.  You can merge or cherry
pick the conjured items tests later.)

The [`workshop` branch](../../tree/workshop) was the branch, where we
collectively developed our solution and published it.  I did the refactorings
my colleagues asked me to do.

Finally, the [`proposed-solution` branch](../../tree/proposed-solution) is a
proposition for solving the refactoring problem including the additions for
conjured items.

### General Refactoring Tips

Write tests.  Run tests often.  Use the possibilities of your IDE for
refactoring.  Learn the shortcut keys for the refactorings of your IDE.
