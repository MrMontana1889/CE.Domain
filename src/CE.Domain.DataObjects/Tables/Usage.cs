// Usage.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace CE.Domain.DataObjects.Tables
{
    [Alias(CoreSchemaTableName.USAGE_V1)]
    public class Usage : DomainTableBase, IHasId<int>
    {
        #region Constructor
        public Usage()
            : base(CoreSchemaTableName.USAGE_V1)
        {

        }
		#endregion

		#region Public Properties
		[Ignore]
		public override int TableTypeID
		{
			get { return Domain.TableTypeId.Usage; }
		}
		[Required]
        [PrimaryKey]
        [AutoIncrement]
        [Alias(CoreSchemaFieldName.USAGEID)]
        public override int Id
        {
            get;
            set;
        }
        [Required]
        [CustomField("TEXT")]
        [Alias(CoreSchemaFieldName.USAGEDATE)]
        public string UsageDate
        {
            get;
            set;
        }
        [Required]
        [CustomField("TEXT")]
        [Alias(CoreSchemaFieldName.JSONCONTENT)]
        public string JSONContent
        {
            get;
            set;
        }
        #endregion

        #region Protected Properties
        protected override string IndexName
        {
            get { return CoreIndexNames.Index_Usage_Date; }
        }
        protected override string IndexFieldName
        {
            get { return CoreSchemaFieldName.USAGEDATE; }
        }
        #endregion
    }
}
