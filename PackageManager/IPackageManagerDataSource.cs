using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager
{
	public interface IPackageManagerDataSource
	{
		void Add(IItemKey item);
		void Remove(IItemKey item);
		void Clear();
		int Count { get; }
		IEnumerator<IItemKey> GetEnumerator();
		bool Contains(IItemKey item);
	}
}

