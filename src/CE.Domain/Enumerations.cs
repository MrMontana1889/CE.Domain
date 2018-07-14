// Enumerations.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

namespace CE.Domain
{
	public struct TableTypeId
	{
		public const int Account = 1;
		public const int Asset = 2;
		public const int Attachment = 3;
		public const int BillDeposit = 4;
		public const int BudgetSplitTransaction = 5;
		public const int BudgetTable = 6;
		public const int BudgetYear = 7;
		public const int Category = 8;
		public const int Currency = 9;
		public const int CurrencyHistory = 10;
		public const int CustomFieldData = 11;
		public const int InfoTable = 12;
		public const int Payee = 14;
		public const int Report = 15;
		public const int Setting = 16;
		public const int SplitTransaction = 17;
		public const int Stock = 18;
		public const int StockHistory = 19;
		public const int StockShare = 20;
		public const int Subcategory = 21;
		public const int Transaction = 22;
		public const int Transfer = 23;
		public const int Usage = 24;
		public const int UserField = 25;

		public const int Room = 26;
		public const int Location = 27;
		public const int Box = 28;
		public const int InventoryItem = 29;
	}

	public enum InfoType
	{
		DATAVERSION,
		MMEXVERSION,
		CREATEDATE,
		DATEFORMAT,
		BASECURRENCYID,
		USERNAME,
		NAV_TREE_STATUS,
		HOME_PAGE_STATUS,
		FINANCIAL_YEAR_START_MONTH,
		FINANCIAL_YEAR_START_DAY,
		USER_COLOR1,
		USER_COLOR2,
		USER_COLOR3,
		USER_COLOR4,
		USER_COLOR5,
		USER_COLOR6,
		USER_COLOR7,
		ATTACHMENTSFOLDER_Win,
		ATTACHMENTSSUBFOLDER,
		ATTACHMENTSDELETE,
		ATTACHMENTSTRASH,
		WEBAPPURL,
		WEBAPPGUID,
		STOCKURL,
		SHARE_PRECISION,
		DELIMITER,
		CHECK_FILTER_ID_,
		TRANSACTIONS_FILTER_,
		BUDGET_FILTER,
		HIDDEN_CATEGS_ID,
	}

	public struct SettingName
	{
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
		public const string CHECK_ASC = "CHECK_ASC";
		public const string CHECK_SORT_COL = "CHECK_SORT_COL";
		public const string TRANSACTIONS_FILTER_VIEW_NO = "TRANSACTIONS_FILTER_VIEW_NO";
		public const string USEORIGDATEONCOPYPASTE = "USEORIGDATEONCOPYPASTE";
		public const string USETRANSSOUND = "USETRANSSOUND";
		public const string VIEWACCOUNTS = "VIEWACCOUNTS";
		public const string VIEWTRANSACTIONS = "VIEWTRANSACTIONS";
		public const string HTMLSCALE = "HTMLSCALE";
		public const string BUDGET_FINANCIAL_YEARS = "BUDGET_FINANCIAL_YEARS";
		public const string BUDGET_INCLUDE_TRANSFERS = "BUDGET_INCLUDE_TRANSFERS";
		public const string BUDGET_SETUP_WITHOUT_SUMMARY = "BUDGET_SETUP_WITHOUT_SUMMARY";
		public const string BUDGET_SUMMARY_WITHOUT_CATEGORIES = "BUDGET_SUMMARY_WITHOUT_CATEGORIES";
		public const string IGNORE_FUTURE_TRANSACTIONS = "IGNORE_FUTURE_TRANSACTIONS";
		public const string PROXYIP = "PROXYIP";
		public const string PROXYPORT = "PROXYPORT";
		public const string ENABLEWEBSERVER = "ENABLEWEBSERVER";
		public const string WEBSERVERPORT = "WEBSERVERPORT";
		public const string NETWORKTIMEOUT = "NETWORKTIMEOUT";
		public const string UPDATECHECK = "UPDATECHECK";
		public const string UPDATESOURCE = "UPDATESOURCE";
		public const string TRANSACTION_PAYEE_NONE = "TRANSACTION_PAYEE_NONE";
		public const string TRANSACTION_CATEGORY_NONE = "TRANSACTION_CATEGORY_NONE";
		public const string TRANSACTION_STATUS_RECONCILED = "TRANSACTION_STATUS_RECONCILED";
		public const string TRANSACTION_DATE_DEFAULT = "TRANSACTION_DATE_DEFAULT";
		public const string BACKUPDB = "BACKUPDB";
		public const string BACKUPDB_UPDATE = "BACKUPDB_UPDATE";
		public const string MAX_BACKUP_FILES = "MAX_BACKUP_FILES";
	}
}
