using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    public class AutoMapperFactory
    {
        private readonly List<Profile> _profiles;

        public AutoMapperFactory(List<Profile> profiles)
        {
            _profiles = Check.NotEmpty(profiles);
        }

        public global::AutoMapper.IMapper Create()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddMaps(_profiles.Select(g => g.GetType())); });
            return config.CreateMapper();
        }
    }
}
