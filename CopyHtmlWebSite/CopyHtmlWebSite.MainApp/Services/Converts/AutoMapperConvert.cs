using CopyHtmlWebSite.Core.Models;
using CopyHtmlWebSite.MainApp.ViewModels;
using Omu.ValueInjecter;
using CopyHtmlWebSite.Core.Infrastructure;

namespace CopyHtmlWebSite.MainApp.Services.Converts
{
    public sealed class AutoMapperConvert : IConverter
    {
        public AutoMapperConvert()
        {
            Mapper.Reset();

            Mapper.AddMap<PageModel, PageViewModel>(source => new PageViewModel(null)
            {
                PageSource = source.Source,
                PageName = source.Name
            });

            Mapper.AddMap<PageViewModel, PageModel>(source => new PageModel
            {
                Name = source.PageName.Trim(),
                Source = source.PageSource.Trim()
            });
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map<TSource, TDestination>(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}