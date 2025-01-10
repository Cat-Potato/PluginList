using System.ComponentModel;
using Exiled.API.Interfaces;

namespace BroadCast
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Hide Exiled Plugins from the list")]
        public bool HideExiledPlugins { get; set; } = false;
        [Description("Available Languages: pl, en")]
        public string CurrentLanguage { get; set; } = "pl";
    }
}
