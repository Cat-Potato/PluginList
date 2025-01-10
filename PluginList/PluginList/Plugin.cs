using System;
using System.Linq;
using Exiled.API.Features;
using Exiled.Loader;

namespace BroadCast
{
    public class Updater : Plugin<Config>
    {
        public override string Name => "PluginList";
        public override string Prefix => "PluginList";
        public override string Author => "Potato Cat";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 2, 2);

        public override void OnEnabled()
        {
            base.OnEnabled();

            #if DEBUG
            PluginAPI.Core.Log.Warning(Localize("Używasz wersji testowej.", "You are using a test version."), Name);
            #endif
            List();
        }

        public override void OnDisabled()
        {
            Bye();
            base.OnDisabled();
        }

        public void List()
        {
            var plugins = Loader.Plugins
                .Where(p => !Config.HideExiledPlugins || !p.Name.StartsWith("Exiled"))
                .OrderBy(p => p.Name)
                .ToList();

            int nameColumnWidth = Math.Max(Localize("Nazwa Pluginów", "Plugin Name").Length, plugins.Max(p => p.Name.Length));
            int versionColumnWidth = Math.Max(Localize("Wersja", "Version").Length, plugins.Max(p => $"V.{p.Version}".Length));

            string horizontalBorder = $"╔{new string('═', nameColumnWidth + 2)}╦{new string('═', versionColumnWidth + 2)}╗";
            string middleBorder = $"╠{new string('═', nameColumnWidth + 2)}╬{new string('═', versionColumnWidth + 2)}╣";
            string bottomBorder = $"╚{new string('═', nameColumnWidth + 2)}╩{new string('═', versionColumnWidth + 2)}╝";

            string header = $"║ {Localize("Nazwa Pluginów", "Plugin Name").PadRight(nameColumnWidth)} ║ {Localize("Wersja", "Version").PadRight(versionColumnWidth)} ║";

            var rows = plugins.Select(plugin =>
                $"║ {plugin.Name.PadRight(nameColumnWidth)} ║ V.{plugin.Version.ToString().PadRight(versionColumnWidth - 2)} ║"
            );

            PluginAPI.Core.Log.Raw(horizontalBorder, Name);
            PluginAPI.Core.Log.Raw(header, Name);
            PluginAPI.Core.Log.Raw(middleBorder, Name);
            foreach (var row in rows)
            {
                PluginAPI.Core.Log.Raw(row, Name);
            }
            PluginAPI.Core.Log.Raw(bottomBorder, Name);
        }

        private string Localize(string plText, string enText)
        {
            return Config.CurrentLanguage == "pl" ? plText : enText;
        }
        
        public void Bye()
        {
            string message = (Localize("Nie wyłączaj mnie...", "Don't turn me off..."));

            // Tworzenie ramki wokół tekstu "Bye"
            int width = message.Length + 4; // Szerokość ramki: tekst + odstępy
            string topBorder = $"╔{new string('═', width)}╗";
            string middle = $"║  {message}  ║";
            string bottomBorder = $"╚{new string('═', width)}╝";

            // Wyświetlenie ramki
            PluginAPI.Core.Log.Raw(topBorder, Name);
            PluginAPI.Core.Log.Raw(middle, Name);
            PluginAPI.Core.Log.Raw(bottomBorder, Name);
        }
    }
}