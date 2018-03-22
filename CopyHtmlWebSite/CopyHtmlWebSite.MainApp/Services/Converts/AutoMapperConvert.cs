namespace CopyHtmlWebSite.MainApp.Services.Converts
{
    using AutoMapper;
    using Core.Infrastructure;
    using Profiles;

    public sealed class AutoMapperConvert : IConverter
    {
        public AutoMapperConvert()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.AddProfile<ModelToViewModelProfile>();
                config.AddProfile<ViewModelToModelProfile>();
            });
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TDestination>(source);
        }
    }
}