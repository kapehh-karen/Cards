using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Core.API.Interfaces;
using System.Reflection;

namespace Core.API
{
    public class PluginManager
    {
        public static readonly PluginManager Instance = new PluginManager();

        private PluginManager() { }

        public List<IPlugin> Plugins { get; } = new List<IPlugin>();
        
        public IPlugin LoadPlugin(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                Type iface = type.GetInterface(typeof(IPlugin).FullName);
                if (iface != null)
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                    Plugins.Add(plugin); // Сначала добавляем
                    plugin.Load(); // Потом выполняем событие
                    return plugin;
                }
            }
            return null;
        }
        
        public void UnloadPlugin(IPlugin plugin)
        {
            if (Plugins.Contains(plugin))
            {
                plugin.Unload(); // Сначала выполняем пользовательский код
                Plugins.Remove(plugin); // Потом удаляем из списка
            }
        }

        public void UnloadAllPlugins()
        {
            while (Plugins.Count > 0)
                UnloadPlugin(Plugins[0]);
        }
    }
}
