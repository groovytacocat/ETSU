using System;
using System.Collections.Generic;

namespace SortClasses;

public interface ISort
{
	void Sort<T>(List<T> values) where T: IComparable<T>;
}
