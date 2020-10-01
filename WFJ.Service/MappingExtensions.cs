using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository.EntityModel;
namespace WFJ.Service
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
        public static List<TDestination> MapList<TSource, TDestination>(List<TSource> source)
        {
            return AutoMapperConfiguration.Mapper.Map<List<TDestination>>(source);
        }
    }

    public class AutoMapperStartupTask
    {
        public void Execute()
        {
            AutoMapperConfiguration.InitializeAutoMapper();
        }
    }
}
