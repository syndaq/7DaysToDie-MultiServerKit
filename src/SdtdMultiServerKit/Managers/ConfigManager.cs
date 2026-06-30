using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Data.IRepositories;
using System.Text;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Managers
{
    /// <summary>
    /// Configuration manager
    /// </summary>
    public static class ConfigManager
    {
        private static GlobalSettings? _globalSettings;

        /// <summary>
        /// Configuration changed event
        /// </summary>
        public static event Action<ISettings>? SettingsChanged;

        /// <summary>
        /// Global settings
        /// </summary>
        public static GlobalSettings GlobalSettings 
        {
            get 
            {
                if (_globalSettings == null)
                {
                    string language;
                    try
                    {
                        language = GamePrefs.GetString(EnumGamePrefs.Language);
                    }
                    catch
                    {
                        language = Language.English.ToString();
                        CustomLogger.Warn("Failed to get language from GamePrefs, use English as default.");
                    }
                    
                    _globalSettings = GetRequired<GlobalSettings>(Locales.Get(language));
                }

                return _globalSettings;
            }
        }

        /// <summary>
        /// Get configuration
        /// </summary>
        /// <typeparam name="TSettings">Configuration type</typeparam>
        /// <returns></returns>
        public static TSettings? Get<TSettings>() where TSettings : ISettings
        {
            TSettings? settings;

            string name = typeof(TSettings).Name;
            var settingsRepository = ModApi.ServiceContainer.Resolve<ISettingsRepository>();
            var entity = settingsRepository.GetById(name);
            if (entity == null)
            {
                return default;
            }
            else
            {
                settings = JsonConvert.DeserializeObject<TSettings>(entity.SerializedValue, ModApi.JsonSerializerSettings);
                if (settings == null)
                {
                    throw new InvalidOperationException($"The persistent settings: {name} is null!");
                }
            }

            return settings;
        }

        /// <summary>
        /// Get configuration, Load default configuration if it does not exist
        /// </summary>
        /// <typeparam name="TSettings">Configuration type</typeparam>
        /// <returns></returns>
        public static TSettings GetRequired<TSettings>(string locale) where TSettings : ISettings
        {
            return Get<TSettings>() ?? LoadDefault<TSettings>(locale);
        }

        /// <summary>
        /// Settings
        /// </summary>
        /// <typeparam name="TSettings">Configuration type</typeparam>
        /// <param name="settings"></param>
        public static void Update<TSettings>(TSettings settings) where TSettings : ISettings
        {
            string name = typeof(TSettings).Name;

            try
            {
                if (settings is GlobalSettings globalSettings)
                {
                    _globalSettings = globalSettings;
                }

                SettingsChanged?.Invoke(settings);

                string json = JsonConvert.SerializeObject(settings, ModApi.JsonSerializerSettings);
                var settingsRepository = ModApi.ServiceContainer.Resolve<ISettingsRepository>();
                settingsRepository.InsertOrReplace(new T_Settings()
                {
                    Name = name,
                    SerializedValue = json
                });
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex, "Error in ConfigManager.Update, SettingsName: {name}", name);
            }
        }

        /// <summary>
        /// LoaddefaultSettings
        /// </summary>
        /// <typeparam name="TSettings"></typeparam>
        /// <param name="locale"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public static TSettings LoadDefault<TSettings>(string locale) where TSettings : ISettings
        {
            try
            {
                string json = ModApi.GetDefaultConfigContent("functionsettings.json", locale);
                var jsonObject = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

                if (jsonObject == null)
                {
                    throw new InvalidOperationException("Load settings faild, the json object is null.");
                }

                var settings = jsonObject[typeof(TSettings).Name]!.ToObject<TSettings>();
                if (settings == null)
                {
                    throw new InvalidOperationException("Load settings faild, the json can not deserialize.");
                }

                Update(settings);
                return settings;
            }
            catch (Exception ex)
            {
                throw new Exception("Load default settings faild.", ex);
            }
        }
    }
}