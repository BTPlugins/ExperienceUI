using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fr34kyn01535.Uconomy;

namespace ExperienceUI
{
    public class Main : RocketPlugin<ExperienceUIConfiguration>
    {
        public static Main Instance;
        protected override void Load()
        {
            Instance = this;
            Logger.Log("#############################################");
            Logger.Log("###             ExperienceUI              ###");
            Logger.Log("###   Plugin Created By blazethrower320   ###");
            Logger.Log("###            Join my Discord:           ###");
            Logger.Log("###     https://discord.gg/YsaXwBSTSm     ###");
            Logger.Log("#############################################");
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateExperience += OnPlayerUpdateExperience;
            Uconomy.Instance.OnBalanceUpdate += BalanceUpdated;
            if (Main.Instance.Configuration.Instance.useEXP == true && Main.Instance.Configuration.Instance.useUconomy == true || Main.Instance.Configuration.Instance.useEXP == false && Main.Instance.Configuration.Instance.useUconomy == false)
            {
                Logger.Log("ERROR: useEXP && useUconomy set to the Same Value!");
                Logger.Log("ERROR: useEXP && useUconomy set to the Same Value!");
                Logger.Log("ERROR: useEXP && useUconomy set to the Same Value!");
            }
        }

        protected override void Unload()
        {
            Logger.Log("ExperienceUI Unloaded");
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateExperience -= OnPlayerUpdateExperience;
            Uconomy.Instance.OnBalanceUpdate -= BalanceUpdated;
        }

        private void OnPlayerUpdateExperience(UnturnedPlayer player, uint experience)
        {
            if(Main.Instance.Configuration.Instance.useEXP == true && Main.Instance.Configuration.Instance.useUconomy == false)
            {
                EffectManager.sendUIEffectText(263, player.Player.channel.owner.transportConnection, true, "ExperienceUI_Balance_Var", experience.ToString());
            }
        }
        private void BalanceUpdated(UnturnedPlayer player, decimal amt)
        {
            if (Main.Instance.Configuration.Instance.useEXP == false && Main.Instance.Configuration.Instance.useUconomy == true)
            {
                var newBalanced = Uconomy.Instance.Database.GetBalance(player.CSteamID.ToString());
                EffectManager.sendUIEffectText(263, player.Player.channel.owner.transportConnection, true, "ExperienceUI_Balance_Var", newBalanced.ToString());
            }
        }
        private void OnPlayerConnected(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Main.Instance.Configuration.Instance.UIKey, 263, player.Player.channel.owner.transportConnection, true);
            if (Main.Instance.Configuration.Instance.useEXP == true && Main.Instance.Configuration.Instance.useUconomy == false)
            {
                EffectManager.sendUIEffectText(263, player.Player.channel.owner.transportConnection, true, "ExperienceUI_Balance_Var", player.Experience.ToString());
            }
            else
            {
                EffectManager.sendUIEffectText(263, player.Player.channel.owner.transportConnection, true, "ExperienceUI_Balance_Var", Uconomy.Instance.Database.GetBalance(player.CSteamID.ToString()).ToString());
            }
        }
        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            EffectManager.askEffectClearByID(Main.Instance.Configuration.Instance.UIKey, player.Player.channel.owner.transportConnection);
        }

    }
}
