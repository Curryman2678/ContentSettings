﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zorro.Settings;

namespace ContentSettings.API
{
    public static class SettingsLoader
    {
        internal static DefaultSettingsSaveLoad SaveLoader = new DefaultSettingsSaveLoad();

        internal static List<Setting> Settings = new List<Setting>();
        internal static void LoadSettingsMenu(SettingsMenu menu)
        {
            foreach (SettingsCell settingsCell in menu.m_cells)
            {
                GameObject.Destroy(settingsCell.gameObject);
            }
            menu.m_cells.Clear();
            SettingsHandler settingsHandler = GameHandler.Instance.SettingsHandler;
            List<Setting> settings = Settings;
            for (int i = 0; i < settings.Count; i++)
            {
                Setting setting = settings[i];
                SettingsCell component = GameObject.Instantiate<GameObject>(menu.m_settingsCell, menu.m_settingsContainer).GetComponent<SettingsCell>();
                component.Setup(setting, settingsHandler);
                menu.m_cells.Add(component);
            }
        }

        internal static void SaveSettings()
        {
            foreach (var setting in Settings)
            {
                setting.Save(SaveLoader);
            }
        }
        public static void RegisterSetting(Setting setting)
        {
            Settings.Add(setting);
            setting.Load(SaveLoader);
        }
    }
}
