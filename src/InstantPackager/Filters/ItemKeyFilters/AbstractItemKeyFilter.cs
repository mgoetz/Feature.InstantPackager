using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters.ItemKeyFilters
{
	public abstract class AbstractItemKeyFilter : AbstractInstantPackageFilter
	{
		public AbstractItemKeyFilter()
			: base()
		{
		}

		public AbstractItemKeyFilter(ICache cache)
			: base(cache)
		{
		}

		override public void Process(FilterArgs args)
		{
			ItemKey contextItemKey = new ItemKey(args.ContentItem);
			if (Hide(contextItemKey))
			{
				args.HideCommand = true;
			}
		}

		public abstract bool Hide(IItemKey contextItemKey);
	}
}
