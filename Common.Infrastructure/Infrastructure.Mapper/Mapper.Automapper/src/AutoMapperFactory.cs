using AutoMapper;
using Jopalesha.CheckWhenDoIt;

namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    /// <summary>
    /// <see cref="global::AutoMapper.IMapper"/> factory.
    /// </summary>
    internal sealed class AutoMapperFactory
    {
        private readonly IEnumerable<Profile> _profiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperFactory"/> class.
        /// </summary>
        /// <param name="profiles">List of <see cref="Profile"/>.</param>
        public AutoMapperFactory(IEnumerable<Profile> profiles)
        {
            _profiles = Check.NotEmpty(profiles);
        }

        /// <summary>
        /// Create <see cref="global::AutoMapper.IMapper"/> instance, with loaded profiles.
        /// </summary>
        /// <returns><see cref="global::AutoMapper.IMapper"/> instance.</returns>
        public global::AutoMapper.IMapper Create()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddMaps(_profiles.Select(g => g.GetType())); });
            return config.CreateMapper();
        }
    }
}
