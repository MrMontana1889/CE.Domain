// DomainTableManagerBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Support.Support;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CE.Domain.ModelingObjects.Support
{
    [ExcludeFromCodeCoverage]
    public abstract class DomainTableManagerBase<TElementType> : ObjectListManagerBase<TElementType>, IDomainTableManager<TElementType> where TElementType : class, IDomainTable
    {
        #region Constructor
        public DomainTableManagerBase(ITableRepository<TElementType> tableRepository)
        {
            TableRepository = tableRepository;
            Items = LoadItems();
            InitializeFields();
        }
        #endregion

        #region Public Methods
        public override IList<int> IDs()
        {
            IList<int> ids = new List<int>(Items.Count);
            foreach (var item in Items)
                ids.Add(item.Id);
            return ids;
        }
        public override IList<TElementType> Elements()
        {
            return Items;
        }
        public override int Add()
        {
            TElementType element = NewElement();
            TableRepository.Add(element);
            Items.Add(element);
            return element.Id;
        }
        public override TElementType Add(params object[] values)
        {
            TElementType element = NewElement(values);
            TableRepository.Add(element);
            Items.Add(element);
            return element;
        }
        public override void Delete(int id)
        {
            TElementType element = Items.Find(e => e.Id == id);
            if (element != null)
                Items.Remove(element);
            TableRepository.Remove(id);
        }
        public override int Commit()
        {
            try { return TableRepository.SaveAll(Elements()); }
            finally { Items = TableRepository.Items(); }
        }
        public override int Commit(TElementType element)
        {
            try { return TableRepository.Save(element).Id; }
            finally { Items = LoadItems(); }
        }
        public override IList GetList()
        {
            return Items;
        }
        public void ReQuery()
        {
            Items = LoadItems();
        }
        #endregion

        #region Public Properties
        public override Type ElementType
        {
            get { return typeof(TElementType); }
        }
        public override int Count
        {
            get { return Items.Count; }
        }
        public override TElementType this[int id]
        {
            get
            {
                int index = Items.FindIndex(e => e.Id == id);
                if (index > -1)
                    return Items[index];
                return null;
            }
        }
        public override bool ContainsListCollection
        {
            get { return false; }
        }
        #endregion

        #region Protected  Methods
        protected override List<TElementType> LoadItems()
        {
            return TableRepository.Items();
        }
        #endregion

        #region Protected Properties
        protected ITableRepository<TElementType> TableRepository
        {
            get;
            private set;
        }
        #endregion
    }
}
