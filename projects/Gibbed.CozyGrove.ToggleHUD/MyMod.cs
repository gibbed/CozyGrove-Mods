/* Copyright (c) 2021 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gibbed.CozyGrove.ToggleHUD
{
    public class MyMod : MelonMod
    {
        private const string PreferenceCategoryId = "Gibbed.CozyGrove.ToggleHUD";
        private const string PreferenceHotkeyId = "Hotkey";

        private KeyCode _HotKey = KeyCode.F11;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            
            TomlKeyCodeSupport.Install();
            MelonPreferences.CreateCategory(PreferenceCategoryId, $"Toggle HUD Settings");
            MelonPreferences.CreateEntry(PreferenceCategoryId, PreferenceHotkeyId, _HotKey, "Hotkey");
            this.ApplyPreferences();
        }

        private void ApplyPreferences()
        {
            _HotKey = MelonPreferences.GetEntryValue<KeyCode>(PreferenceCategoryId, PreferenceHotkeyId);
        }

        public override void OnPreferencesSaved() => this.ApplyPreferences();

        public override void OnUpdate()
        {
            if (this._HotKey != KeyCode.None &&
                SceneManager.GetActiveScene().name == "Game" &&
                Input.GetKeyDown(this._HotKey) == true)
            {
                // toggle UI visibility.
                var instance = GameUI.Instance;
                if (instance != null)
                {
                    instance.SetVisible(!instance.isVisible);
                }
            }
        }
    }
}
