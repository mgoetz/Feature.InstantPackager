using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;

namespace Sitecore.SharedSource.InstantPackager.Utils.Filters.InstantPackageStatusFilters
{
	public class IfInstantPackageIsNotEmptyHide : AbstractInstantPackageStatusFilter
	{
		public IfInstantPackageIsNotEmptyHide() : base()
		{
		}

		public IfInstantPackageIsNotEmptyHide(ICache cache) : base(cache)
		{
		}

		override public bool Hide()
		{
			bool hidden = _manager.Count > 0;
			return hidden;
		}
	}
}
