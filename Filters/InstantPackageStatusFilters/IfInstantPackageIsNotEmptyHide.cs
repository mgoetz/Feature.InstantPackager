using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters.InstantPackageStatusFilters
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
