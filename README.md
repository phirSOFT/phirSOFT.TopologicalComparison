# phirSOFT.TopologicalComparison

A library for weaker orderings.

[![Build status](https://ci.appveyor.com/api/projects/status/k3w3e4rbo5ascn78/branch/master?svg=true)](https://ci.appveyor.com/project/phirSOFT/phirsoft-topologicalcomparison/branch/master)

## Introduction

Sometimes we cant compare all instances of a type with each other. In this case the usual sorting algoritms can't sort a collection of such instances. 
The Topological comparison aims at this problem by providing an algorithm that can sort such instances topologically. That means there might be more than one correct ordering. To give an example.
We can etablish a partial ordetring on the `ISet<T>` type by ordering differenct sets, if they are subsets of each other. This could be implemented as the following:

``` c#
public class TopologicalSetComparer<T> : ITopologicalComparer<ISet<T>>
{
  public int Compare(ISet<T> x, ISet<T> y)
  {
    var xLessy = x.IsSubsetOf(y);
    var yLessx = y.IsSubsetOf(x);
    
    if(xLessy && yLessx)
      return 0;
    return xLessy ? -1 : 1;
  }
  
  public bool CanCompare(ISet<T> x, ISet<T> y)
  {
    return x.IsSubsetOf(y) || y.IsSubsetOf(x);
  }

}
```
