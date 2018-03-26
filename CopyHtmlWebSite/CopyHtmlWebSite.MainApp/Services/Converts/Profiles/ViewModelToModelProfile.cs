namespace CopyHtmlWebSite.MainApp.Services.Converts.Profiles
{
    using AutoMapper;
    using Core.Models;
    using ViewModels;

    public class ViewModelToModelProfile : Profile
    {
        public ViewModelToModelProfile()
        {
            CreateMap<PageViewModel, PageModel>().ConvertUsing(viewModel => new PageModel
            {
                Name = viewModel.PageName.Trim(),
                Source = viewModel.PageSource.Trim()
            });
        }
    }
}