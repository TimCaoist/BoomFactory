using BoomFactory.Model;

namespace BoomFactory.Fetcher
{
    public class FetchFactory
    {
        public static BaseFetch Create(MetaData metaData)
        {
            switch (metaData.MetaMode)
            {
                case MetaMode.Mutil:
                    return DefaultFetch.Instance;
                case MetaMode.Single:
                    return CompentFetch.Instance;
                case MetaMode.SubType:
                    return SubFetch.Instance;
            }

            return null;
        }
    }
}
