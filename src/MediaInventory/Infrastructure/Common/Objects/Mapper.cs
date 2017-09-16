using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Internal;

namespace MediaInventory.Infrastructure.Common.Objects
{
    public class Mapper : IMapper
    {
        private readonly IMapper _mapper;

        public Mapper(IEnumerable<Profile> profiles)
        {
            _mapper = new MapperConfiguration(config =>
            {
                profiles.Each(config.AddProfile);
            }).CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider => _mapper.ConfigurationProvider;

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
        {
            return _mapper.Map<TDestination>(source, opts);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return _mapper.Map(source, opts);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            return _mapper.Map(source, destination, opts);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            return _mapper.Map(source, destination, sourceType, destinationType, opts);
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            return _mapper.Map(source, sourceType, destinationType, opts);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, destination, sourceType, destinationType);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, sourceType, destinationType);
        }
    }
}