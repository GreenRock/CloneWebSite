namespace CopyHtmlWebSite.Core.Infrastructure
{
    public interface IConverter
    {
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        TDestination Map<TSource, TDestination>(TSource source);
    }
}