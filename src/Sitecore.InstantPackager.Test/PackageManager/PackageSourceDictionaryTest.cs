using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Sitecore.Data;
using Sitecore.Globalization;
using Sitecore.SharedSource.Commons.Extensions;
using Sitecore.Data.Items;
using Sitecore.SharedSource.InstantPackager.Utils.Test.Utility;
using Sitecore.SharedSource.InstantPackager.Utils.Cache;
using Sitecore.SharedSource.InstantPackager.Utils.ItemKeys;
using Sitecore.SharedSource.InstantPackager.Utils.PackageManager;
using Version = Sitecore.Data.Version;

namespace Sitecore.SharedSource.InstantPackager.Utils.Test.PackageManager
{

	[TestFixture]
	class PackageSourceDictionaryTest
	{
		private PackageSourceDictionary _testSourceDictionary = null;
		private IItemKey _testItemKey1 = null;
		private IItemKey _testItemKey1Duplicate1 = null;
		private IItemKey _testItemKey1Duplicate2 = null;
		private IItemKey _testItemKey2 = null;
		private IItemKey _testItemKey3 = null;
		private IItemKey _testItemKey4 = null;

		#region constants
		private Version _ver1 = new Version(1);
		private Version _ver2 = new Version(2);
		private static ID _guid1 = new ID("{00000000-0000-0000-0000-000000000001}");
		private ID _guid2 = new ID("{00000000-0000-0000-0000-000000000002}");
		private ID _guid3 = new ID("{00000000-0000-0000-0000-000000000003}");
		private ID _guid4 = new ID("{00000000-0000-0000-0000-000000000004}");
		private string mockLanguageWrapperEnglish_langEnglish = "en";
		private string _langSpanish = "es";
		private string _webDbName = "web";
		private string _masterDbName = "master";
		private string _coreDbName = "core";
		private string _path1 = "sitecore/home/Item1";
		#endregion

		[TestFixtureSetUp]
		public void SetUpFixture()
		{
			ILanguageWrapper mockLanguageWrapperEnglish = MockRepository.GenerateMock<ILanguageWrapper>();
			ILanguageWrapper mockLanguageWrapperSpanish = MockRepository.GenerateMock<ILanguageWrapper>();
			mockLanguageWrapperEnglish.Stub(x => x.ToString()).Return("en");
			mockLanguageWrapperSpanish.Stub(x => x.ToString()).Return("es");

			Language foo = null;
			_testItemKey1 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey1Duplicate1 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey1Duplicate2 = new ItemKey(_guid1, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey2 = new ItemKey(_guid2, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey3 = new ItemKey(_guid3, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);
			_testItemKey4 = new ItemKey(_guid4, _path1, _ver1, mockLanguageWrapperEnglish, _webDbName);

			//_testSourceDictionary = new PackageSourceDictionary();// This is in the test setup instead of fixture setup so each test gets a clean start.
		}

		[SetUp]
		public void SetUp()
		{
			TestCache.Clear();
			_testSourceDictionary = new PackageSourceDictionary(new TestCache());
			//_testSourceDictionary.Clear();// This is part of the API we're testing, so it shouldn't be in the setup!
		}

		[TearDown]
		public void TearDown()
		{
		}

		#region void Add(ItemKey item);
		[Test]
		public void Add_AddNewToEmpty()
		{
			//Arrange

			//Action
			_testSourceDictionary.Add(_testItemKey1);

			//Assert
			Assert.AreEqual(1, _testSourceDictionary.Count);
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1));
		}

		[Test]
		public void Add_AddNewToPopulated()
		{
			//Arrange

			//Action
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);

			//Assert
			Assert.AreEqual(3, _testSourceDictionary.Count);
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey2));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey3));
		}

		[Test]
		public void Add_AddDuplicateToPopulatedDoesNotDuplicateInSource()
		{
			//Arrange

			//Action
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey1Duplicate1);

			//Assert
			Assert.AreEqual(2, _testSourceDictionary.Count);
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey2));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1Duplicate1));
		}

		[Test]
		public void Add_AddSeveralDuplicatesDoesNotDuplicateInSource()
		{
			//Arrange

			//Action
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey1Duplicate1);
			_testSourceDictionary.Add(_testItemKey1Duplicate2);

			//Assert
			Assert.AreEqual(1, _testSourceDictionary.Count);
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1Duplicate1));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey1Duplicate2));
		}
		#endregion

		#region void Remove(ItemKey item);
		[Test]
		public void Remove_RemoveFromEmptyList()
		{
			//Arrange

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Remove(_testItemKey1);
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.IsFalse(_testSourceDictionary.Contains(_testItemKey1));
			Assert.That(afterCount, Is.EqualTo(beforeCount));
			Assert.That(afterCount, Is.EqualTo(0));
		}

		[Test]
		public void Remove_RemoveItemWhereNotIncludedMakesNoChange()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Remove(_testItemKey2);
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.IsFalse(_testSourceDictionary.Contains(_testItemKey2));
			Assert.That(afterCount, Is.EqualTo(beforeCount));
		}

		[Test]
		public void Remove_RemoveItemWhereIncludedRemoves()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Remove(_testItemKey1);
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.IsFalse(_testSourceDictionary.Contains(_testItemKey1));
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
		}

		[Test]
		public void Remove_MultipleRemovesWhereIncluddeRemovesAndDoesntThrowException()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Remove(_testItemKey1);
			_testSourceDictionary.Remove(_testItemKey1);
			_testSourceDictionary.Remove(_testItemKey1);
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.IsFalse(_testSourceDictionary.Contains(_testItemKey1));
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
		}

		[Test]
		public void Remove_RemoveEquivalentButNotSameKey()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Remove(_testItemKey1Duplicate1);
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
			Assert.IsFalse(_testSourceDictionary.Contains(_testItemKey1));
		}

		[Test]
		public void Remove_RemoveDoesNotAffectOtherElements()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);
			_testSourceDictionary.Add(_testItemKey4);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Remove(_testItemKey1);
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(beforeCount - 1));
			Assert.IsFalse(_testSourceDictionary.Contains(_testItemKey1));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey2));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey3));
			Assert.IsTrue(_testSourceDictionary.Contains(_testItemKey4));
		}
		#endregion

		#region void Clear();
		[Test]
		public void Clear_OnEmpty()
		{
			//Arrange

			//Act
			_testSourceDictionary.Clear();
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
		}

		[Test]
		public void Clear_OnPopulated()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Clear();
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
			Assert.That(afterCount, Is.LessThan(beforeCount));
		}

		[Test]
		public void Clear_MultipleCalls()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);

			//Act
			int beforeCount = _testSourceDictionary.Count;
			_testSourceDictionary.Clear();
			_testSourceDictionary.Clear();
			_testSourceDictionary.Clear();
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
			Assert.That(afterCount, Is.LessThan(beforeCount));
		}
		#endregion

		#region int Count { get; }
		[Test]
		public void Count_EmptyList()
		{
			//Arrange

			//Act
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(0));
		}

		[Test]
		public void Count_NonEmptyList()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);

			//Act
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(3));
		}

		[Test]
		public void Count_DuplicatesAddedCountOnce()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey1Duplicate1);
			_testSourceDictionary.Add(_testItemKey1Duplicate2);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);

			//Act
			int afterCount = _testSourceDictionary.Count;

			//Assert
			Assert.That(afterCount, Is.EqualTo(3));
		}
		
		#endregion

		#region IEnumerator<IItemKey> GetEnumerator();
		[Test]
		public void GetEnumerator_EmptyEnumerator()
		{
			//Arrange

			//Act
			var enumerator = _testSourceDictionary.GetEnumerator();

			//Assert
			int itemCount = 0;
			while (enumerator.MoveNext())
			{
				itemCount++;
			} ;
			Assert.That(itemCount, Is.EqualTo(_testSourceDictionary.Count));
			Assert.That(itemCount, Is.EqualTo(0));
		}

		[Test]
		public void GetEnumerator_PopulatedSourceEnumeratorContainsAllSourceItems()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);
			_testSourceDictionary.Add(_testItemKey3);
			_testSourceDictionary.Add(_testItemKey4);
			bool[] foundItems = new bool[4]{false, false, false, false};

			//Act
			IEnumerator<IItemKey> enumerator = _testSourceDictionary.GetEnumerator();

			//Assert
			int itemCount = 0;
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == _testItemKey1)
				{
					foundItems[0] = true;
				}
				else if (enumerator.Current == _testItemKey2)
				{
					foundItems[1] = true;
				}
				else if (enumerator.Current == _testItemKey3)
				{
					foundItems[2] = true;
				}
				else if (enumerator.Current == _testItemKey4)
				{
					foundItems[3] = true;
				}

				itemCount++;
			}
			Assert.That(itemCount, Is.EqualTo(_testSourceDictionary.Count));
			Assert.That(itemCount, Is.EqualTo(4));
			Assert.IsTrue(foundItems[0]);
			Assert.IsTrue(foundItems[1]);
			Assert.IsTrue(foundItems[2]);
			Assert.IsTrue(foundItems[3]);
		}

		#endregion

		#region bool Contains(ItemKey item);
		[Test]
		public void Contains_EmptyListContains()
		{
			//Arrange

			//Act
			bool itemIncluded = _testSourceDictionary.Contains(_testItemKey1);

			//Assert
			Assert.IsFalse(itemIncluded);
		}

		[Test]
		public void Contains_PopulatedListDoesNotContain()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);

			//Act
			bool itemIncluded = _testSourceDictionary.Contains(_testItemKey2);

			//Assert
			Assert.IsFalse(itemIncluded);
		}

		[Test]
		public void Contains_PopulatedListContainsAllItemsAdded()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);

			//Act
			bool item1Included = _testSourceDictionary.Contains(_testItemKey1);
			bool item2Included = _testSourceDictionary.Contains(_testItemKey2);

			//Assert
			Assert.IsTrue(item1Included);
			Assert.IsTrue(item2Included);
		}

		[Test]
		public void Contains_ContainsEquivalentButNotSameKey()
		{
			//Arrange
			_testSourceDictionary.Add(_testItemKey1);
			_testSourceDictionary.Add(_testItemKey2);

			//Act
			bool itemIncluded = _testSourceDictionary.Contains(_testItemKey1Duplicate1);

			//Assert
			Assert.IsTrue(itemIncluded);
		}
		#endregion
	}
}
