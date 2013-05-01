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
	public class IfNoSubItemsInInstantPackageHide : AbstractRelatedItemKeyFilter
	{
		public IfNoSubItemsInInstantPackageHide()
			: base()
		{
		}

		public IfNoSubItemsInInstantPackageHide(ICache cache)
			: base(cache)
		{
		}

		//Testable
		public override bool Hide(IEnumerable<IItemKey> itemKeys)
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

			bool hasAnyChildren = false;
			foreach (IItemKey item in itemKeys)
			{
				if (_manager.Contains(item))
				{
					hasAnyChildren = true;
					break;
				}
			}
			return !hasAnyChildren;
		}
	}
}
