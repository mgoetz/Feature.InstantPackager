using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;

namespace Sitecore.SharedSource.InstantPackager.Filters.RelatedItemFilters
{
	abstract public class AbstractRelatedItemKeyFilter : AbstractInstantPackageFilter
	{
		public AbstractRelatedItemKeyFilter() : base()
		{
		}

		public AbstractRelatedItemKeyFilter(ICache cache) : base(cache)
		{
		}

		override public void Process(FilterArgs args)
		{
			bool hideVal = Hide(args.ContentItem);
			args.HideCommand = hideVal;
		}

		public bool Hide(Item item)
		{
			IEnumerable<IItemKey> allSubItems = item.Axes.GetDescendants().Select(x => new ItemKey(x) as IItemKey);
			bool hideVal = Hide(allSubItems);
			return hideVal;
		}

		abstract public bool Hide(IEnumerable<IItemKey> itemKeys);
	}
}
