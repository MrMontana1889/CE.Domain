// Setting.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace CE.Domain.DataObjects.Tables
{
    [Alias(CoreSchemaTableName.SETTING_V1)]
    public class Setting : DomainTableBase, IHasId<int>
    {
        #region Constructor
        public Setting()
            : base(CoreSchemaTableName.SETTING_V1)
        {

        }
		#endregion

		#region Public Properties
		[Ignore]
		public override int TableTypeID
		{
			get { return Domain.TableTypeId.Setting; }
		}
		[Required]
        [PrimaryKey]
        [AutoIncrement]
        [Alias(CoreSchemaFieldName.SETTINGID)]
        public override int Id
        {
            get;
            set;
        }
        [Required]
        [CustomField("TEXT COLLATE NOCASE")]
        [Alias(CoreSchemaFieldName.SETTINGNAME)]
        [Unique]
        public string SettingName
        {
            get;
            set;
        }
        [CustomField("TEXT")]
        [Alias(CoreSchemaFieldName.SETTINGVALUE)]
        public string SettingValue
        {
            get;
            set;
        }
        #endregion

        #region Protected Properties
        protected override string IndexName
        {
            get { return CoreIndexNames.Index_Setting_SettingName; }
        }
        protected override string IndexFieldName
        {
            get { return CoreSchemaFieldName.SETTINGNAME; }
        }
        #endregion
    }
}
