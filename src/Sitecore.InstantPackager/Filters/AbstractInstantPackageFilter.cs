using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.SharedSource.Commons.Extensions;
using Sitecore.Data.Items;
using Sitecore.SharedSource.Contextualizer.Filters;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;

namespace Sitecore.SharedSource.InstantPackager.Utils.Filters 
{
	abstract public class AbstractInstantPackageFilter : Sitecore.SharedSource.Contextualizer.Filters.IFilter
	{
		protected InstantPackageManager _manager = null;
		public AbstractInstantPackageFilter()
		{
			_manager = new InstantPackageManager(new PackageSourceDictionary());
		}

		public AbstractInstantPackageFilter(ICache cache)
		{
			_manager = new InstantPackageManager(new PackageSourceDictionary(cache));
		}

		abstract public void Process(FilterArgs args);
	}
}
