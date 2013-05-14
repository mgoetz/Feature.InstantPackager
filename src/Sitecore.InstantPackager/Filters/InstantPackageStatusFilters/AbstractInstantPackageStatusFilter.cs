using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;

namespace Sitecore.SharedSource.InstantPackager.Filters.InstantPackageStatusFilters
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
