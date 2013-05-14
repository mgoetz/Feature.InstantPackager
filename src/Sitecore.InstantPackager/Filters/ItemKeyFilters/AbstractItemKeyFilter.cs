using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;

namespace Sitecore.SharedSource.InstantPackager.Filters.ItemKeyFilters
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
