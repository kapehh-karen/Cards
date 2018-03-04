using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.API.Interfaces
{
    public interface IPlugin
    {
        /// <summary>
        /// Название плагина
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Версия плагина
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Автор плагина
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Происходит при загрузке плагина
        /// </summary>
        void Load();

        /// <summary>
        /// Происходит при выгрузке плагина
        /// </summary>
        void Unload();
    }
}
