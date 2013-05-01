using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters.RelatedItemFilters
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
