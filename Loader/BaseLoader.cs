using BoomFactory.Model;
using BoomFactory.Model.FAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BoomFactory.Loader
{
    public abstract class BaseLoader
    {
        protected abstract string[] IngoreAssemblieNames { get; }

        internal volatile bool LoadComplete;

        internal Task Task { get; private set; }

        private readonly static Dictionary<Type, MetaData> metaDatas = new Dictionary<Type, MetaData>();

        public MetaData GetMetaData(Type type)
        {
            MetaData metaData;
            if (metaDatas.TryGetValue(type, out metaData))
            {
                return metaData;
            }

            return null;
        }

        public BaseLoader()
        {
            Start();
        }

        public void Start()
        {
            Task = Task.Factory.StartNew(() =>
            {
                var assemblies = LoadAssemblies();
                LoadConfig();
                Parallel.ForEach(assemblies, assembly =>
                {
                    var mDs = GetMetaDatas(assembly);
                    if (mDs.Count() == 0)
                    {
                        return;
                    }

                    lock (metaDatas)
                    {
                        foreach (var item in mDs)
                        {
                            metaDatas.Add(item.Key, item);
                        }
                    }
                });

                LoadComplete = true;
            });
        }

        protected virtual void LoadConfig()
        {
        }

        protected virtual IEnumerable<Assembly> LoadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return FilterAssemblies(assemblies);
        }

        protected virtual IEnumerable<Assembly> FilterAssemblies(IEnumerable<Assembly> assemblies)
        {
            return assemblies.Where(a => !IngoreAssemblieNames.Any(an => a.FullName.StartsWith(string.Concat(an, ','), StringComparison.OrdinalIgnoreCase))).ToArray();
        }
        protected abstract IEnumerable<MetaData> GetMetaDatas(Assembly assembly);
    }
}
