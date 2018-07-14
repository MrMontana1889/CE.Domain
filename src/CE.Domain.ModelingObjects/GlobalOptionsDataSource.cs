// GlobalOptionsDataSource.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.DataObjects.Tables;
using CE.Support.Support;

namespace CE.Domain.ModelingObjects
{
    public class GlobalOptionsDataSource : DataSource, IGlobalOptionsDataSource
    {
        #region Constructor
        public GlobalOptionsDataSource()
        {
        }
        #endregion

        #region Public Methods
        public void SetGlobalSetting(string key, bool value)
        {
            if (!IsOpen()) return;

            Setting setting = SettingRepository.Find(s => s.SettingName == key);

            if (setting == null)
                setting = new Setting();

            Set(setting, key, value.ToString().ToUpperInvariant());
        }
        public void SetGlobalSetting(string key, int value)
        {
            if (!IsOpen()) return;

            Setting setting = SettingRepository.Find(s => s.SettingName == key);

            if (setting == null)
                setting = new Setting();

            Set(setting, key, value.ToString());
        }
        public void SetGlobalSetting(string key, DrawingColor value)
        {
            if (!IsOpen()) return;

            Setting setting = SettingRepository.Find(s => s.SettingName == key);

            if (setting == null)
                setting = new Setting();

            Set(setting, key, value.ToARGBString());
        }
        public void SetGlobalSetting(string key, string value)
        {
            if (!IsOpen()) return;

            Setting setting = SettingRepository.Find(s => s.SettingName == key);

            if (setting == null)
                setting = new Setting();

            Set(setting, key, value);
        }
        public TValueType GetGlobalSetting<TValueType>(string key, TValueType defaultValue)
        {
            if (!IsOpen()) return defaultValue;
            if (defaultValue == null) return default(TValueType);

            Setting setting = SettingRepository.Find(s => s.SettingName == key);

            if (setting == null)
                return defaultValue;

            if (defaultValue.GetType() == typeof(bool))
                return (TValueType)((object)bool.Parse(setting.SettingValue));
            if (defaultValue.GetType() == typeof(int))
                return (TValueType)((object)int.Parse(setting.SettingValue));
            if (defaultValue.GetType() == typeof(DrawingColor))
                return (TValueType)((object)DrawingColor.Parse(setting.SettingValue));
            if (defaultValue.GetType() == typeof(string))
                return (TValueType)((object)setting.SettingValue);

            return defaultValue;
        }
        #endregion

        #region Protected Methods
        protected override ISqliteDataConnection NewDataConnection()
        {
            return new GlobalOptionsSqliteDataConnection();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _settingRepository = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Private Properties
        private ITableRepository<Setting> SettingRepository
        {
            get
            {
                if (IsOpen() && _settingRepository == null)
                {
                    _settingRepository = DataRepository.GetTableRepositoryFor<Setting>(SchemaTableName.SETTING_V1);
                }
                return _settingRepository;
            }
        }
        #endregion

        #region Private Methods
        private void Set(Setting setting, string key, string value)
        {
            setting.SettingName = key;
            setting.SettingValue = value;
            SettingRepository.Save(setting);
        }
        #endregion

        #region Private Fields
        private ITableRepository<Setting> _settingRepository;
        #endregion
    }
}
