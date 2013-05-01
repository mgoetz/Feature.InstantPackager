using Sitecore.Globalization;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys
{
	public interface ILanguageWrapper
	{
		Language GetLanguage();
		string ToString();
	}
}
