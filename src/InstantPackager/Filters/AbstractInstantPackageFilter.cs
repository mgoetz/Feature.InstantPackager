using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Velir.SitecoreLibrary.Extensions;
using Sitecore.Data.Items;
using Velir.SitecoreLibrary.Modules.Contextualizer.Filters;
using Velir.SitecoreLibrary.Modules.InstantPackager.Cache;
using Velir.SitecoreLibrary.Modules.InstantPackager.PackageManager;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Filters 
{
	abstract public class AbstractInstantPackageFilter : Velir.SitecoreLibrary.Modules.Contextualizer.Filters.IFilter
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
