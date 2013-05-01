using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;

namespace Sitecore.SharedSource.InstantPackager.Utils.Filters.RelatedItemFilters
{
	public class IfAnySubItemsInInstantPackageHide : AbstractRelatedItemKeyFilter
	{
		public IfAnySubItemsInInstantPackageHide() : base()
		{
		}

		public IfAnySubItemsInInstantPackageHide(ICache cache) : base(cache)
		{
		}

		//Testable
		override public bool Hide(IEnumerable<IItemKey> itemKeys)
		{
			if (itemKeys == null)
			{
				throw new ArgumentNullException("itemKeys must not be null.");
			}
			int numKeys = itemKeys.Count();
			if (numKeys == 0)
			{
				return true;// no children so they're implicitly included
			}

			bool subItemFound = false;
			foreach (IItemKey item in itemKeys)
			{
				if (_manager.Contains(item))
				{
					subItemFound = true;
					break;
				}
			}
			return subItemFound;
		}
	}
}
