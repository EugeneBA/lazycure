﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LifeIdea.LazyCure.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("TimeLogs")]
        public string TimeLogsFolder {
            get {
                return ((string)(this["TimeLogsFolder"]));
            }
            set {
                this["TimeLogsFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SaveAfterDone {
            get {
                return ((bool)(this["SaveAfterDone"]));
            }
            set {
                this["SaveAfterDone"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("30")]
        public int MaxActivitiesInHistory {
            get {
                return ((int)(this["MaxActivitiesInHistory"]));
            }
            set {
                this["MaxActivitiesInHistory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("01:00:00")]
        public global::System.TimeSpan ReminderTime {
            get {
                return ((global::System.TimeSpan)(this["ReminderTime"]));
            }
            set {
                this["ReminderTime"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1280, 1024")]
        public global::System.Drawing.Point MainWindowLocation {
            get {
                return ((global::System.Drawing.Point)(this["MainWindowLocation"]));
            }
            set {
                this["MainWindowLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int ActivitiesNumberInTray {
            get {
                return ((int)(this["ActivitiesNumberInTray"]));
            }
            set {
                this["ActivitiesNumberInTray"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TwitterEnabled {
            get {
                return ((bool)(this["TwitterEnabled"]));
            }
            set {
                this["TwitterEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitterAccessToken {
            get {
                return ((string)(this["TwitterAccessToken"]));
            }
            set {
                this["TwitterAccessToken"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SwitchOnLogOff {
            get {
                return ((bool)(this["SwitchOnLogOff"]));
            }
            set {
                this["SwitchOnLogOff"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LeftClickOnTray {
            get {
                return ((bool)(this["LeftClickOnTray"]));
            }
            set {
                this["LeftClickOnTray"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Ctrl+F12")]
        public string HotKeyToActivate {
            get {
                return ((string)(this["HotKeyToActivate"]));
            }
            set {
                this["HotKeyToActivate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SwitchTimeLogAtMidnight {
            get {
                return ((bool)(this["SwitchTimeLogAtMidnight"]));
            }
            set {
                this["SwitchTimeLogAtMidnight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool SplitByComma {
            get {
                return ((bool)(this["SplitByComma"]));
            }
            set {
                this["SplitByComma"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Ctrl+Alt+Shift+F12")]
        public string HotKeyToSwitch {
            get {
                return ((string)(this["HotKeyToSwitch"]));
            }
            set {
                this["HotKeyToSwitch"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TweetingActivity {
            get {
                return ((string)(this["TweetingActivity"]));
            }
            set {
                this["TweetingActivity"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseTweetingActivity {
            get {
                return ((bool)(this["UseTweetingActivity"]));
            }
            set {
                this["UseTweetingActivity"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Language {
            get {
                return ((string)(this["Language"]));
            }
            set {
                this["Language"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string TwitterAccessTokenSecret {
            get {
                return ((string)(this["TwitterAccessTokenSecret"]));
            }
            set {
                this["TwitterAccessTokenSecret"] = value;
            }
        }
    }
}
