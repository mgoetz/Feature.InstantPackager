using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters.RelatedItemFilters
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
