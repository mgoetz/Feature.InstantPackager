using System;
using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters.ItemKeyFilters
{
	public class IfCurrentItemInInstantPackageHide : AbstractItemKeyFilter
	{
		public IfCurrentItemInInstantPackageHide() : base()
		{
		}

		public IfCurrentItemInInstantPackageHide(ICache cache) : base(cache)
		{
		}

		//Testable, this is the new structure for the interface
		override public bool Hide(IItemKey contextItemKey)
		{
			if (contextItemKey == null)
			{
				throw new ArgumentNullException("contextItemKey must not be null.");
			}

			bool hide = _manager.Contains(contextItemKey);
			return hide;
		}
	}
}
