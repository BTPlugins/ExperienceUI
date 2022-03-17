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

namespace ExperienceUI
{
    public class Main : RocketPlugin<ExperienceUIConfiguration>
    {
        public static Main Instance;
        public static ExperienceUIConfiguration Config => Instance.Configuration.Instance;
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
        }

        protected override void Unload()
        {
            Logger.Log("ExperienceUI Unloaded");
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;
            UnturnedPlayerEvents.OnPlayerUpdateExperience -= OnPlayerUpdateExperience;
        }

        private void OnPlayerUpdateExperience(UnturnedPlayer player, uint experience)
        {
            EffectManager.sendUIEffectText(263, player.Player.channel.owner.transportConnection, true, "ExperienceUI_Balance_Var", experience.ToString());
        }
        private void OnPlayerConnected(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Config.UIKey, 263, player.Player.channel.owner.transportConnection, true);
            EffectManager.sendUIEffectText(263, player.Player.channel.owner.transportConnection, true, "ExperienceUI_Balance_Var", player.Experience.ToString());
        }
        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            EffectManager.askEffectClearByID(Config.UIKey, player.Player.channel.owner.transportConnection);
        }

    }
}
