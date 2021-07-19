using UnityEngine;
using UnityEngine.SceneManagement;

namespace CanerSungur.SavingSystem
{
    /// <summary>
    /// Fill in these classes according to what do you want to save-load
    /// </summary>
    public class SaveData
    {
        #region Examples

        //public int Gold { get; set; }
        //public float MovementSpeedMultiplier { get; set; }
        //public int MovementSpeedMultiplierPerk { get; set; }

        #endregion
    }

    public static class SavePersistence
    {
        /// <summary>
        /// You have to assign default values for Android.
        /// In editor it works perfectly but if a value should have a default value other than 0, 
        /// you have to assign it in LoadData.
        /// Make conditions and detect if that variable has changed and saved or not.
        /// If not, give it the default value.
        /// If it should be 0, than ignore all this.
        /// </summary>
        /// <returns></returns>
        public static SaveData LoadData()
        {
            // Fetch Data
            //int Gold = PlayerPrefs.GetInt("Gold");

            //#region Movement Speed Multiplier

            //float MovementSpeedMultiplier;
            //if (PlayerPrefs.GetFloat("MovementSpeedMultiplier") < 1f && PlayerPrefs.GetInt("MovementSpeedMultiplierPerks") == 0)
            //    MovementSpeedMultiplier = 1f;
            //else
            //    MovementSpeedMultiplier = PlayerPrefs.GetFloat("MovementSpeedMultiplier");
            //int MovementSpeedMultiplierPerk = PlayerPrefs.GetInt("MovementSpeedMultiplierPerks");

            //#endregion

            SaveData saveData = new SaveData()
            {
                // Create new variables from data we've fetched
                //Gold = Gold,
                //MovementSpeedMultiplier = MovementSpeedMultiplier,
                //MovementSpeedMultiplierPerk = MovementSpeedMultiplierPerk,
            };

            return saveData;
        }

        // This section requires parameter.
        // This parameter is which script you are saving these data;

        //public static void SaveData(PlayerUpgrades playerUpgrades)
        //{
        //    // Save data where we've fethced
        //    PlayerPrefs.SetInt("Gold", PlayerStats.Gold);
        //    PlayerPrefs.SetFloat("MovementSpeedMultiplier", PlayerStats.MovementSpeedMultiplier);
        //    PlayerPrefs.SetInt("MovementSpeedMultiplierPerks", playerUpgrades.MovementSpeedButton.Upgrade.Perk);

        //    PlayerPrefs.Save();
        //}

        //public static void DeleteAll(PlayerUpgrades playerUpgrades)
        //{
        //    PlayerPrefs.DeleteAll();

        //    // Default static variable values
        //    PlayerStats.Gold = 0;
        //    PlayerStats.MovementSpeedMultiplier = 1f;
        //    playerUpgrades.MovementSpeedButton.Upgrade.Perk = 0;

        //    //if you want to reload the scene after reseting all game data.
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
    }
}