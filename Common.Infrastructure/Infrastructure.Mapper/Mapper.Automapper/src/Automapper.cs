namespace Jopalesha.Common.Infrastructure.Mapper.AutoMapper
{
    public class AutoMapper : IMapper
    {
        private readonly global::AutoMapper.IMapper _mapper;

        public AutoMapper(global::AutoMapper.IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);
    }
}
