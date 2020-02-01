using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Jopalesha.Common.Infrastructure.Helpers;

namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    public class AutoMapperFactory
    {
        private readonly List<Profile> _profiles;

        public AutoMapperFactory(List<Profile> profiles)
        {
            _profiles = Check.NotNullOrEmpty(profiles);
        }

        public global::AutoMapper.IMapper Create()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddMaps(_profiles.Select(g => g.GetType())); });
            return config.CreateMapper();
        }
    }
}
