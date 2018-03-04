using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.API
{
    public class AssemblyManager
    {
        public static readonly AssemblyManager Instance = new AssemblyManager();

        private AssemblyManager()
        {
            // Вызывается событие когда приложение не может найти сборку
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Тычем загруженной ранее сборкой в наглую рожу модулей
            return GetAssembly(args.Name);
        }

        public Dictionary<string, Assembly> AssemblyList { get; } = new Dictionary<string, Assembly>();

        public Assembly LoadFromStream(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                var assembly = Assembly.Load(ms.ToArray());
                AssemblyList.Add(assembly.FullName, assembly);
                return assembly;
            }
        }

        public Assembly GetAssembly(string name) => AssemblyList.ContainsKey(name) ? AssemblyList[name] : null;
    }
}
