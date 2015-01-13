using System.Collections.Generic;
using JuliusSweetland.ETTA.Enums;
using JuliusSweetland.ETTA.Properties;
using JuliusSweetland.ETTA.Services;
using log4net;
using Microsoft.Practices.Prism.Mvvm;

namespace JuliusSweetland.ETTA.UI.ViewModels.Management
{
    public class WordsViewModel : BindableBase
    {
        private readonly IDictionaryService dictionaryService;

        #region Private Member Vars

        private readonly static ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion
        
        #region Ctor

        public WordsViewModel(IDictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;

            LoadSettings();
        }

        #endregion
        
        #region Properties
        
        public List<KeyValuePair<string, Languages>> Languages
        {
            get
            {
                return new List<KeyValuePair<string, Languages>>
                {
                    new KeyValuePair<string, Languages>("American English", Enums.Languages.AmericanEnglish),
                    new KeyValuePair<string, Languages>("British English", Enums.Languages.BritishEnglish),
                    new KeyValuePair<string, Languages>("Canadian English", Enums.Languages.CanadianEnglish)
                };
            }
        }
        
        private Languages language;
        public Languages Language
        {
            get { return language; }
            set { SetProperty(ref this.language, value); }
        }
        
        private bool autoAddSpace;
        public bool AutoAddSpace
        {
            get { return autoAddSpace; }
            set { SetProperty(ref autoAddSpace, value); }
        }
        
        private bool autoCapitalise;
        public bool AutoCapitalise
        {
            get { return autoCapitalise; }
            set { SetProperty(ref autoCapitalise, value); }
        }
        
        private int multiKeySelectionMaxDictionaryMatches;
        public int MultiKeySelectionMaxDictionaryMatches
        {
            get { return multiKeySelectionMaxDictionaryMatches; }
            set { SetProperty(ref multiKeySelectionMaxDictionaryMatches, value); }
        }

        public bool ChangesRequireRestart
        {
            get { return false; }
        }
        
        #endregion
        
        #region Methods

        private void LoadSettings()
        {
            Language = Settings.Default.Language;
            AutoAddSpace = Settings.Default.AutoAddSpace;
            AutoCapitalise = Settings.Default.AutoCapitalise;
            MultiKeySelectionMaxDictionaryMatches = Settings.Default.MultiKeySelectionMaxDictionaryMatches;
        }

        public void ApplyChanges()
        {
            bool reloadDictionary = Settings.Default.Language != Language;

            Settings.Default.Language = Language;
            Settings.Default.AutoAddSpace = AutoAddSpace;
            Settings.Default.AutoCapitalise = AutoCapitalise;
            Settings.Default.MultiKeySelectionMaxDictionaryMatches = MultiKeySelectionMaxDictionaryMatches;
            Settings.Default.Save();

            if (reloadDictionary)
            {
                dictionaryService.LoadDictionary();
            }
        }

        #endregion
    }
}