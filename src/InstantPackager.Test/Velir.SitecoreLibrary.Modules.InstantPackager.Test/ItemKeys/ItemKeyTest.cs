using System;
using NUnit.Framework;
using Rhino.Mocks;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Velir.SitecoreLibrary.Modules.InstantPackager.ItemKeys;
using Version = Sitecore.Data.Version;

namespace Velir.SitecoreLibrary.Modules.InstantPackager.Test.ItemKeys
{
	[Category("InstantPackageManager")]
	[TestFixture]
	class ItemKeyTest
	{
		#region constants
		private Sitecore.Data.Version _ver1 = new Sitecore.Data.Version(1);
		private Sitecore.Data.Version _ver1Duplicate = new Sitecore.Data.Version(1);
		private Sitecore.Data.Version _ver2 = new Sitecore.Data.Version(2);
		private static ID _guidNull = new ID("{00000000-0000-0000-0000-000000000000}");
		private static ID _guid1 = new ID("{00000000-0000-0000-0000-000000000001}");
		private static ID _guid1Duplicate = new ID("{00000000-0000-0000-0000-000000000001}");
		private static ID _guid2 = new ID("{00000000-0000-0000-0000-000000000002}");
		private string _langEnglish = "en";
		private string _langEnglishDuplicate = "en";
		private string _langSpanish = "es";
		private ILanguageWrapper _mockLanguageWrapperEnglish = null;
		private ILanguageWrapper _mockLanguageWrapperEnglishDuplicate = null;
		private ILanguageWrapper _mockLanguageWrapperSpanish = null;
		private string _masterDbName = "master";
		private string _masterDbNameDuplicate = "master";
		private string _webDbName = "web";
		private string _path1 = "/sitecore/home/item1";
		private string _path1Duplicate = "/sitecore/home/item1";
		private string _path2 = "/sitecore/home/item2";
		#endregion

		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			_mockLanguageWrapperEnglish = MockRepository.GenerateMock<ILanguageWrapper>();
			_mockLanguageWrapperEnglish.Stub(x => x.ToString()).Return(_langEnglish);

			_mockLanguageWrapperEnglishDuplicate = MockRepository.GenerateMock<ILanguageWrapper>();
			_mockLanguageWrapperEnglishDuplicate.Stub(x => x.ToString()).Return(_langEnglishDuplicate);

			_mockLanguageWrapperSpanish = MockRepository.GenerateMock<ILanguageWrapper>();
			_mockLanguageWrapperSpanish.Stub(x => x.ToString()).Return(_langSpanish);
		}

		#region void ItemKey(ID guid, string path, Version version, ILanguageWrapper language, string databaseName)
		[Test]
		public void Constructor2_ValidArgsSetCorrectly()
		{
			//Arrange

			//Act
			ItemKey testItemKey = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Assert
			Assert.AreEqual(testItemKey.Guid, _guid1);
			Assert.AreEqual(testItemKey.Path, _path1);
			Assert.AreEqual(testItemKey.Version, _ver1);
			Assert.AreEqual(testItemKey.Language, _mockLanguageWrapperEnglish);
			Assert.AreEqual(testItemKey.DatabaseName, _masterDbName);
		}

		[Test]
		public void Constructor2_NullGuidThorwsNullArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => new ItemKey(null, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName));
		}

		[Test]
		public void Constructor2_NullPathThrowsNullArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => new ItemKey(_guid1, null, _ver1, _mockLanguageWrapperEnglish, _masterDbName));
		}

		[Test]
		public void Constructor2_EmptyPathThrowsArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentException>(() => new ItemKey(_guid1, string.Empty, _ver1, _mockLanguageWrapperEnglish, _masterDbName));
		}

		[Test]
		public void Constructor2_NullVersionThrowsNullArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => new ItemKey(_guid1, _path1, null, _mockLanguageWrapperEnglish, _masterDbName));
		}

		[Test]
		public void Constructor2_NullLanguageThrowsNullArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => new ItemKey(_guid1, _path1, _ver1, null, _masterDbName));
		}

		[Test]
		public void Constructor2_NullDatabaseNameThrowsNullArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentNullException>(() => new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, null));
		}

		[Test]
		public void Constructor2_EmptyDatabaseThrowsNullArgumentException()
		{
			//Arrange

			// Act & Assert
			Assert.Throws<ArgumentException>(() => new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, string.Empty));
		}
		#endregion

		#region ID Guid
		[Test]
		public void Guid_MatchesConstructedValue()
		{
			//Arrange
			ItemKey testItemKey = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			ID testId = testItemKey.Guid;

			//Assert
			Assert.AreEqual(_guid1, testId);
		}
		#endregion

		#region string Path;
		[Test]
		public void Path_MatchesConstructedValue()
		{
			//Arrange
			ItemKey testItemKey = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testPath = testItemKey.Path;

			//Assert
			Assert.AreEqual(_path1, testPath);
		}
		#endregion

		#region Version Version
		[Test]
		public void Version_MatchesConstructedValue()
		{
			//Arrange
			ItemKey testItemKey = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			Version testVersion = testItemKey.Version;

			//Assert
			Assert.AreEqual(_ver1, testVersion);
		}
		#endregion

		#region ILanguageWrapper Language
		[Test]
		public void Language_MatchesConstructedValue()
		{
			//Arrange
			ItemKey testItemKey = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			ILanguageWrapper testLanguageWrapper = testItemKey.Language;

			//Assert
			Assert.AreEqual(_mockLanguageWrapperEnglish.ToString(), testLanguageWrapper.ToString());
		}
		#endregion

		#region string DatabaseName
		[Test]
		public void DatabaseName_MatchesConstructedValue()
		{
			//Arrange
			ItemKey testItemKey = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testDbName = testItemKey.DatabaseName;

			//Assert
			Assert.AreEqual(_masterDbName, testDbName);
		}
		#endregion

		#region string GetKeyString()
		[Test]
		public void GetKeyString_DifferentGuidsGenerateDifferntKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.GetKeyString();
			string testItemKey2KeyString = testItemKey2.GetKeyString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void GetKeyString_DifferentPathsgenerateDifferentKeyString()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.GetKeyString();
			string testItemKey2KeyString = testItemKey2.GetKeyString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void GetKeyString_DifferntVersionsGerneateDifferentKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.GetKeyString();
			string testItemKey2KeyString = testItemKey2.GetKeyString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void GetKeyString_DifferentLanguagesGenerateDifferntKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperSpanish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.GetKeyString();
			string testItemKey2KeyString = testItemKey2.GetKeyString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void GetKeyString_DiffenretDbNamesGenerateDifferntKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _webDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.GetKeyString();
			string testItemKey2KeyString = testItemKey2.GetKeyString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void GetKeyString_EquivalentKeyItemsGenerateSameKeyString()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1Duplicate, _path1Duplicate, _ver1Duplicate, _mockLanguageWrapperEnglishDuplicate, _masterDbNameDuplicate);

			//Act
			string testItemKey1KeyString = testItemKey1.GetKeyString();
			string testItemKey2KeyString = testItemKey2.GetKeyString();

			//Assert
			Assert.AreEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void GetKeyString_SeveralDifferntItemKeysNeverGenerateSameKeyString()
		{
			//Arrange
			ItemKey[] itemKeyCollection = new ItemKey[32];
			itemKeyCollection[0] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[1] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[2] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[3] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[4] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[5] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[6] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[7] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[8] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[9] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[10] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[11] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[12] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[13] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[14] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[15] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[16] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[17] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[18] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[19] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[20] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[21] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[22] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[23] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[24] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[25] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[26] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[27] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[28] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[29] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[30] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[31] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperSpanish, _webDbName);

			//Act
			string[] itemKeyStringCollection = new string[32];
			for (int i = 0; i < itemKeyCollection.Length; i++)
			{
				itemKeyStringCollection[i] = itemKeyCollection[i].GetKeyString();
			}

			//Assert
			for (int j = 0; j < itemKeyStringCollection.Length; j++)
			{
				for (int k = j + 1; k < itemKeyStringCollection.Length; k++)
				{
					Assert.AreNotEqual(itemKeyStringCollection[j], itemKeyStringCollection[k]);
				}
			}
		}
		#endregion

		#region string ToString()
		[Test]
		public void ToString_DifferentGuidsGenerateDifferntKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.ToString();
			string testItemKey2KeyString = testItemKey2.ToString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void ToString_DifferentPathsgenerateDifferentKeyString()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.ToString();
			string testItemKey2KeyString = testItemKey2.ToString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void ToString_DifferntVersionsGerneateDifferentKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperEnglish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.ToString();
			string testItemKey2KeyString = testItemKey2.ToString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void ToString_DifferentLanguagesGenerateDifferntKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperSpanish, _masterDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.ToString();
			string testItemKey2KeyString = testItemKey2.ToString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void ToString_DiffenretDbNamesGenerateDifferntKeyStrings()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _webDbName);

			//Act
			string testItemKey1KeyString = testItemKey1.ToString();
			string testItemKey2KeyString = testItemKey2.ToString();

			//Assert
			Assert.AreNotEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void ToString_EquivalentKeyItemsGenerateSameKeyString()
		{
			//Arrange
			ItemKey testItemKey1 = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			ItemKey testItemKey2 = new ItemKey(_guid1Duplicate, _path1Duplicate, _ver1Duplicate, _mockLanguageWrapperEnglishDuplicate, _masterDbNameDuplicate);

			//Act
			string testItemKey1KeyString = testItemKey1.ToString();
			string testItemKey2KeyString = testItemKey2.ToString();

			//Assert
			Assert.AreEqual(testItemKey1KeyString, testItemKey2KeyString);
		}

		[Test]
		public void ToString_SeveralDifferntItemKeysNeverGenerateSameKeyString()
		{
			//Arrange
			ItemKey[] itemKeyCollection = new ItemKey[32];
			itemKeyCollection[0] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[1] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[2] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[3] = new ItemKey(_guid1, _path1, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[4] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[5] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[6] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[7] = new ItemKey(_guid1, _path1, _ver2, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[8] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[9] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[10] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[11] = new ItemKey(_guid1, _path2, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[12] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[13] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[14] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[15] = new ItemKey(_guid1, _path2, _ver2, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[16] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[17] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[18] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[19] = new ItemKey(_guid2, _path1, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[20] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[21] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[22] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[23] = new ItemKey(_guid2, _path1, _ver2, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[24] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[25] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[26] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[27] = new ItemKey(_guid2, _path2, _ver1, _mockLanguageWrapperSpanish, _webDbName);
			itemKeyCollection[28] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperEnglish, _masterDbName);
			itemKeyCollection[29] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperEnglish, _webDbName);
			itemKeyCollection[30] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperSpanish, _masterDbName);
			itemKeyCollection[31] = new ItemKey(_guid2, _path2, _ver2, _mockLanguageWrapperSpanish, _webDbName);

			//Act
			string[] itemKeyStringCollection = new string[32];
			for (int i = 0; i < itemKeyCollection.Length; i++)
			{
				itemKeyStringCollection[i] = itemKeyCollection[i].ToString();
			}

			//Assert
			for (int j = 0; j < itemKeyStringCollection.Length; j++)
			{
				for (int k = j + 1; k < itemKeyStringCollection.Length; k++)
				{
					Assert.AreNotEqual(itemKeyStringCollection[j], itemKeyStringCollection[k]);
				}
			}
		}

		#endregion

	}
}
