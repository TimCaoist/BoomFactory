using BoomFactory.Model;
using BoomFactory.Model.FAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BoomFactory.Loader
{
    public class StandLoader : BaseLoader
    {
        protected override string[] IngoreAssemblieNames => new string[] { "mscorlib", "mscorlib.resources", "BoomFactory" };

        protected override IEnumerable<MetaData> GetMetaDatas(Assembly assembly)
        {
            var types = assembly.GetTypes();
            ICollection<MetaData> metaDatas = new List<MetaData>();
            foreach (var type in types)
            {
                if ((type.IsAbstract || type.IsInterface))
                {
                    var attributes = AttributeLoader.Instance.GetFactoryAttributes(type);
                    if (attributes.Any() == false)
                    {
                        continue;
                    }

                    MetaData metaData = new MetaData(type, attributes);
                    metaDatas.Add(metaData);
                    LoadReleatedTypes(metaData, types);
                }
            }

            return metaDatas;
        }

        protected void LoadReleatedTypes(MetaData metaData, Type[] types)
        {
            foreach (var type in types)
            {
                if (type.IsAbstract || type.IsInterface)
                {
                }

                if (!type.IsSubclassOf(metaData.Key) && type.GetInterface(metaData.Key.FullName) == null)
                {
                    continue;
                }

                metaData.SubTypes.Add(type);
            }

            metaData.Analysis();
        }
    }
}
