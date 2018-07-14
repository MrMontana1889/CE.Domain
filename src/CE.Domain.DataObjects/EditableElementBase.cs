// EditableElementBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Support.Support;
using CE.Support.Units;
using System;
using System.Collections.Generic;

namespace CE.Domain.DataObjects
{
    public abstract class EditableElementBase : IEditableElement
    {
        #region Constructor
        public EditableElementBase()
        {
            FieldCollection = new List<IField>();
            Fields = new Dictionary<string, IField>();
            InitializeFields();
        }
        #endregion

        #region Public Methods
        public IList<IField> SupportedFields()
        {
            return FieldCollection;
        }
        public IField Field(string name)
        {
            return Fields[name];
        }
        #endregion

        #region Protected Methods
        protected virtual void InitializeFields()
        {

        }
        protected void AddField<TValueType, IElementType>(string fieldName, Type valueType, string labelKey, IElementType element)
        {
            IFieldType fieldType = new FieldType(fieldName, valueType, labelKey, string.Empty, string.Empty, string.Empty, Unit.None, default(TValueType));
            IField field = new ObjectField<IElementType>(fieldType, element);

            Fields.Add(field.Name, field);
            FieldCollection.Add(field);
        }
        protected void AddField<IElementType>(string fieldName, Type valueType, string labelKey, object defaultValue, IElementType element)
        {
            IFieldType fieldType = new FieldType(fieldName, valueType, labelKey, string.Empty, string.Empty, string.Empty, Unit.None, defaultValue);
            IField field = new ObjectEditField<IElementType>(fieldType, element);

            Fields.Add(field.Name, field);
            FieldCollection.Add(field);
        }
        protected void AddField<TValueType, IElementType>(string fieldName, Type valueType, string labelKey, string formatterName,
            Unit storageUnit, object defaultValue, IElementType element)
        {
            IFieldType fieldType = new FieldType(fieldName, valueType, labelKey, string.Empty, string.Empty, formatterName, storageUnit, defaultValue);
            IField field = new UnitizedObjectField<IElementType, TValueType>(fieldType, element);

            Fields.Add(field.Name, field);
            FieldCollection.Add(field);
        }
        #endregion

        #region Protected Properties
        protected IList<IField> FieldCollection
        {
            get;
            private set;
        }
        protected IDictionary<string, IField> Fields
        {
            get;
            private set;
        }
        #endregion
    }
}
