// DomainTestBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using NUnit.Framework;
using ServiceStack;

namespace CE.Domain.Test
{
    [TestFixture]
    public abstract class DomainTestFixtureBase
    {
        #region Constructor
        public DomainTestFixtureBase()
        {

        }
        #endregion

        #region Initialize/Cleanup
        [SetUp]
        public void Initialize()
        {
            InitializeLicense();
            InitializeImpl();
        }
        [TearDown]
        public void Cleanup()
        {
            CleanupImpl();
        }
        #endregion

        #region Protected Methods
        protected virtual void InitializeImpl()
        {

        }
        protected virtual void CleanupImpl()
        {

        }
        protected void InitializeLicense()
        {
            
        }
        #endregion

        #region Protected Properties
        protected IRepositoryDataSource DataSource
        {
            get;
            set;
        }
        protected string Filename
        {
            get;
            set;
        }
        #endregion
    }
}
