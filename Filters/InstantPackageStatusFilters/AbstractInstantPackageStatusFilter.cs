using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters.InstantPackageStatusFilters
{
	abstract public class AbstractInstantPackageStatusFilter : AbstractInstantPackageFilter
	{
		public AbstractInstantPackageStatusFilter()
			: base()
		{
		}

		public AbstractInstantPackageStatusFilter(ICache cache)
			: base(cache)
		{
		}

		override public void Process(FilterArgs args)
		{
			args.HideCommand = Hide();
		}

		public abstract bool Hide();
	}
}
