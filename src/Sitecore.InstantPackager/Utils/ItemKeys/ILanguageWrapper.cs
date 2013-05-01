using Sitecore.Globalization;

namespace Sitecore.SharedSource.InstantPackager.Utils.ItemKeys
{
	public interface ILanguageWrapper
	{
		Language GetLanguage();
		string ToString();
	}
}
