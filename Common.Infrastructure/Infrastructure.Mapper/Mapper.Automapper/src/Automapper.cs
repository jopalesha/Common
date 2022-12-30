namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    /// <summary>
    /// <see cref="global::AutoMapper.IMapper"/> implementation.
    /// </summary>
    internal sealed class AutoMapper : IMapper
    {
        private readonly global::AutoMapper.IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapper"/> class.
        /// </summary>
        /// <param name="mapper">Internal mapper.</param>
        public AutoMapper(global::AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);
    }
}
