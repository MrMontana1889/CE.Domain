// DomainTableInfoTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Tables;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using System;
using System.Reflection;

namespace CE.Domain.Test.DataObjects.TableInfo
{
	[TestFixture]
    public class DomainTableInfoTestFixture
    {
        #region Constructor
        public DomainTableInfoTestFixture()
        {

        }
        #endregion

        #region Tests
        [Test]
        public void TestDomainTableInfo()
        {
			OrmLiteConfig.DialectProvider = SqliteDialect.Provider;

			InfoTable info = new InfoTable();
            info.Should().NotBeNull();

            Assert.Throws<NullReferenceException>(new TestDelegate(
                delegate
                {
                    info.Create(null);
                }));

            Assert.Throws<NullReferenceException>(new TestDelegate(
                delegate
                {
                    info.Create_Index(null);
                }));

            PropertyInfo pi = info.GetType().GetProperty("IndexName", BindingFlags.NonPublic | BindingFlags.Instance);
            pi.Should().NotBeNull();

            Assert.IsNotNull(pi.GetValue(info));

            pi = info.GetType().GetProperty("IndexFieldName", BindingFlags.NonPublic | BindingFlags.Instance);
            pi.Should().NotBeNull();

            Assert.IsNotNull(pi.GetValue(info));
        }
        #endregion
    }
}
