// INFOTABLE_V1.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace CE.Domain.DataObjects.Tables
{
    [Alias(CoreSchemaTableName.INFOTABLE_V1)]
    public class InfoTable : DomainTableBase, IHasId<int>
    {
        #region Constructor
        public InfoTable()
            : base(CoreSchemaTableName.INFOTABLE_V1)
        {
            InfoName = string.Empty;
            Value = string.Empty;
        }
		#endregion

		#region Public Properties
		[Ignore]
		public override int TableTypeID
		{
			get { return Domain.TableTypeId.InfoTable; }
		}
		[Alias(CoreSchemaFieldName.INFOID)]
        [PrimaryKey]
        [AutoIncrement]
        [Required]
        public override int Id
        {
            get;
            set;
        }
        [Alias(CoreSchemaFieldName.INFONAME)]
        [Required]
        [Unique]
        [CustomField("TEXT COLLATE NOCASE")]
        public string InfoName
        {
            get;
            set;
        }
        [Alias(CoreSchemaFieldName.INFOVALUE)]
        [Required]
        [CustomField("TEXT")]
        public string Value
        {
            get;
            set;
        }
        #endregion

        #region Protected Properties
        protected override string IndexName
        {
            get { return CoreIndexNames.Index_InfoTable_InfoName; }
        }
        protected override string IndexFieldName
        {
            get { return CoreSchemaFieldName.INFONAME; }
        }
        #endregion
    }
}
