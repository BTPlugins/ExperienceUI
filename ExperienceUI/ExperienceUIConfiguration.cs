using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperienceUI
{
    public class ExperienceUIConfiguration : IRocketPluginConfiguration
    {
        public ushort UIKey { get; set; } 
        public bool useEXP { get; set; }
        public bool useUconomy { get; set; }
        public void LoadDefaults()
        {
            UIKey = 26301;
            useEXP = true;
            useUconomy = false;
        }
    }
}
