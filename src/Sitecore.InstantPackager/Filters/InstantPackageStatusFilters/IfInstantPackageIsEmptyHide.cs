using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;

namespace Sitecore.SharedSource.InstantPackager.Utils.Filters.InstantPackageStatusFilters
{
	public class IfInstantPackageIsEmptyHide : AbstractInstantPackageStatusFilter
	{
		public IfInstantPackageIsEmptyHide() : base()
		{
		}

		public IfInstantPackageIsEmptyHide(ICache cache) : base(cache)
		{
		}

		override public bool Hide()
		{
			bool hidden = _manager.Count == 0;
			return hidden;
		}
	}
}
