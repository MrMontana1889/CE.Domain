// StandardAttributeName.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

namespace CE.Domain
{
	public struct SchemaTableName
	{
		public const string INFOTABLE_V1 = "INFOTABLE_V1";
		public const string SETTING_V1 = "SETTING_V1";
		public const string USAGE_V1 = "USAGE_V1";
	}

	public struct SchemaFieldName
	{
		public const string Id = "Id";
		public const string NOTES = "NOTES";

		public const string SETTINGID = "SETTINGID";
		public const string SETTINGNAME = "SETTINGNAME";
		public const string SETTINGVALUE = "SETTINGVALUE";

		public const string USAGEID = "USAGEID";
		public const string USAGEDATE = "USAGEDATE";
		public const string JSONCONTENT = "JSONCONTENT";

		public const string INFOID = "INFOID";
		public const string INFONAME = "INFONAME";
		public const string INFOVALUE = "INFOVALUE";
	}

	public struct StandardIndexName
	{
		public const string Index_InfoTable_InfoName = "IDX_INFOTABLE_INFONAME";
		public const string Index_Setting_SettingName = "IDX_SETTING_SETTINGNAME";
		public const string Index_Usage_Date = "IDX_USAGE_DATE";
	}

	public struct StandardInfoNames
	{
		public const string DATAVERSION = "DATAVERSION";
		public const string MMEXVERSION = "MMEXVERSION";
		public const string CREATEDATE = "CREATEDATE";
		public const string DATEFORMAT = "DATEFORMAT";
		public const string BASECURRENCYID = "BASECURRENCYID";
		public const string USERNAME = "USERNAME";
		public const string NAV_TREE_STATUS = "NAV_TREE_STATUS";
		public const string HOME_PAGE_STATUS = "HOME_PAGE_STATUS";
		public const string FINANCIAL_YEAR_START_MONTH = "FINANCIAL_YEAR_START_MONTH";
		public const string FINANCIAL_YEAR_START_DAY = "FINANCIAL_YEAR_START_DAY";
		public const string USER_COLOR1 = "USER_COLOR1";
		public const string USER_COLOR2 = "USER_COLOR2";
		public const string USER_COLOR3 = "USER_COLOR3";
		public const string USER_COLOR4 = "USER_COLOR4";
		public const string USER_COLOR5 = "USER_COLOR5";
		public const string USER_COLOR6 = "USER_COLOR6";
		public const string USER_COLOR7 = "USER_COLOR7";
		public const string ATTACHMENTSFOLDER_Win = "ATTACHMENTSFOLDER:Win";
		public const string ATTACHMENTSSUBFOLDER = "ATTACHMENTSSUBFOLDER";
		public const string ATTACHMENTSDELETE = "ATTACHMENTSDELETE";
		public const string ATTACHMENTSTRASH = "ATTACHMENTSTRASH";
		public const string WEBAPPURL = "WEBAPPURL";
		public const string WEBAPPGUID = "WEBAPPGUID";
		public const string STOCKURL = "STOCKURL";
		public const string SHARE_PRECISION = "SHARE_PRECISION";
		public const string DELIMITER = "DELIMITER";
		public const string CHECK_FILTER_ID_ = "CHECK_FILTER_ID_";
		public const string TRANSACTIONS_FILTER_ = "TRANSACTIONS_FILTER_";
		public const string BUDGET_FILTER = "BUDGET_FILTER";
		public const string HIDDEN_CATEGS_ID = "HIDDEN_CATEGS_ID";
	}

	public struct StandardSettingNames
	{
		public const string SETTINGNAME = "SETTINGNAME";
		public const string LANGUAGE = "LANGUAGE";
		public const string UUID = "UUID";
		public const string SENDUSAGESTATS = "SENDUSAGESTATS";
		public const string SHOWBEGINAPP = "SHOWBEGINAPP";
		public const string LASTFILENAME = "LASTFILENAME";
		public const string AUIPERSPECTIVE = "AUIPERSPECTIVE";
		public const string ORIGINX = "ORIGINX";
		public const string ORIGINY = "ORIGINY";
		public const string SIZEW = "SIZEW";
		public const string SIZEH = "SIZEH";
		public const string ISMAXIMIZED = "ISMAXIMIZED";
		public const string CHECK_COL0_WIDTH = "CHECK_COL0_WIDTH";
		public const string CHECK_COL1_WIDTH = "CHECK_COL1_WIDTH";
		public const string CHECK_COL2_WIDTH = "CHECK_COL2_WIDTH";
		public const string CHECK_COL3_WIDTH = "CHECK_COL3_WIDTH";
		public const string CHECK_COL4_WIDTH = "CHECK_COL4_WIDTH";
		public const string CHECK_COL5_WIDTH = "CHECK_COL5_WIDTH";
		public const string CHECK_COL6_WIDTH = "CHECK_COL6_WIDTH";
		public const string CHECK_COL7_WIDTH = "CHECK_COL7_WIDTH";
		public const string CHECK_COL8_WIDTH = "CHECK_COL8_WIDTH";
		public const string CHECK_COL9_WIDTH = "CHECK_COL9_WIDTH";
		public const string CHECK_COL10_WIDTH = "CHECK_COL10_WIDTH";
		public const string BUDGET_COL1_WIDTH = "BUDGET_COL1_WIDTH";
		public const string BUDGET_COL0_WIDTH = "BUDGET_COL0_WIDTH";
		public const string BUDGET_COL2_WIDTH = "BUDGET_COL2_WIDTH";
		public const string BUDGET_COL3_WIDTH = "BUDGET_COL3_WIDTH";
		public const string BUDGET_COL4_WIDTH = "BUDGET_COL4_WIDTH";
		public const string BUDGET_COL5_WIDTH = "BUDGET_COL5_WIDTH";
		public const string BUDGET_COL6_WIDTH = "BUDGET_COL6_WIDTH";
		public const string STOCKS_COL0_WIDTH = "STOCKS_COL0_WIDTH";
		public const string STOCKS_COL1_WIDTH = "STOCKS_COL1_WIDTH";
		public const string STOCKS_COL2_WIDTH = "STOCKS_COL2_WIDTH";
		public const string STOCKS_COL3_WIDTH = "STOCKS_COL3_WIDTH";
		public const string STOCKS_COL4_WIDTH = "STOCKS_COL4_WIDTH";
		public const string STOCKS_COL5_WIDTH = "STOCKS_COL5_WIDTH";
		public const string STOCKS_COL6_WIDTH = "STOCKS_COL6_WIDTH";
		public const string STOCKS_COL7_WIDTH = "STOCKS_COL7_WIDTH";
		public const string STOCKS_COL8_WIDTH = "STOCKS_COL8_WIDTH";
		public const string STOCKS_COL9_WIDTH = "STOCKS_COL9_WIDTH";
		public const string STOCKS_COL10_WIDTH = "STOCKS_COL10_WIDTH";
		public const string STOCKS_COL11_WIDTH = "STOCKS_COL11_WIDTH";
		public const string STOCKS_COL12_WIDTH = "STOCKS_COL12_WIDTH";
		public const string STOCKS_COL13_WIDTH = "STOCKS_COL13_WIDTH";
		public const string ASSETS_COL0_WIDTH = "ASSETS_COL0_WIDTH";
		public const string ASSETS_COL1_WIDTH = "ASSETS_COL1_WIDTH";
		public const string ASSETS_COL2_WIDTH = "ASSETS_COL2_WIDTH";
		public const string ASSETS_COL3_WIDTH = "ASSETS_COL3_WIDTH";
		public const string ASSETS_COL4_WIDTH = "ASSETS_COL4_WIDTH";
		public const string ASSETS_COL5_WIDTH = "ASSETS_COL5_WIDTH";
		public const string ASSETS_COL6_WIDTH = "ASSETS_COL6_WIDTH";
		public const string ASSETS_COL7_WIDTH = "ASSETS_COL7_WIDTH";
		public const string BD_COL1_WIDTH = "BD_COL1_WIDTH";
		public const string BD_COL2_WIDTH = "BD_COL2_WIDTH";
		public const string BD_COL4_WIDTH = "BD_COL4_WIDTH";
		public const string BD_COL5_WIDTH = "BD_COL5_WIDTH";
		public const string BD_COL7_WIDTH = "BD_COL7_WIDTH";
		public const string BD_COL12_WIDTH = "BD_COL12_WIDTH";
		public const string BD_COL13_WIDTH = "BD_COL13_WIDTH";
		public const string BD_COL6_WIDTH = "BD_COL6_WIDTH";
		public const string BD_COL3_WIDTH = "BD_COL3_WIDTH";
		public const string BD_COL0_WIDTH = "BD_COL0_WIDTH";
		public const string BD_COL8_WIDTH = "BD_COL8_WIDTH";
		public const string BD_COL9_WIDTH = "BD_COL9_WIDTH";
		public const string BD_COL10_WIDTH = "BD_COL10_WIDTH";
		public const string BD_COL11_WIDTH = "BD_COL11_WIDTH";
		public const string BD_COL14_WIDTH = "BD_COL14_WIDTH";
		public const string BD_COL15_WIDTH = "BD_COL15_WIDTH";
		public const string RECENT_DB_1 = "RECENT_DB_1";
		public const string RECENT_DB_2 = "RECENT_DB_2";
		public const string RECENT_DB_3 = "RECENT_DB_3";
		public const string RECENT_DB_4 = "RECENT_DB_4";
		public const string RECENT_DB_5 = "RECENT_DB_5";
		public const string RECENT_DB_6 = "RECENT_DB_6";
		public const string RECENT_DB_7 = "RECENT_DB_7";
		public const string RECENT_DB_8 = "RECENT_DB_8";
		public const string EXPAND_CATEGS_TREE = "EXPAND_CATEGS_TREE";
		public const string SHOW_HIDDEN_CATEGS = "SHOW_HIDDEN_CATEGS";
	}
}
