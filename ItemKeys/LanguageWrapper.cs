using System;
using Sitecore.Globalization;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys
{
	public class LanguageWrapper : ILanguageWrapper
	{
		private Language _language = null;
		public LanguageWrapper(Language language)
		{
			if (language == null)
			{
				throw new ArgumentNullException("A LanguageWrapper cannot be constructed for a null language.");
			}
			_language = language;
		}

		public static implicit operator LanguageWrapper(Language language)
		{
			return new LanguageWrapper(language);
		}

		public Language GetLanguage()
		{
			return _language;
		}

		public override string ToString()
		{
			return _language.ToString();
		}
	}
}
