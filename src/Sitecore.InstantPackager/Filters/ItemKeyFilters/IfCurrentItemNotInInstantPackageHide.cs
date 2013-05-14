using System;
using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;

namespace Sitecore.SharedSource.InstantPackager.Filters.ItemKeyFilters
{
	public class IfCurrentItemNotInInstantPackageHide : AbstractItemKeyFilter
	{
		public IfCurrentItemNotInInstantPackageHide() : base()
		{
		}

		public IfCurrentItemNotInInstantPackageHide(ICache cache) : base(cache)
		{
		}

		//Testable, this is the new structure for the interface
		override public bool Hide(IItemKey contextItemKey)
		{
			if (contextItemKey == null)
			{
				throw new ArgumentNullException("contextItemKey must not be null.");
			}

			bool hide = !_manager.Contains(contextItemKey);
			return hide;
		}
	}
}
