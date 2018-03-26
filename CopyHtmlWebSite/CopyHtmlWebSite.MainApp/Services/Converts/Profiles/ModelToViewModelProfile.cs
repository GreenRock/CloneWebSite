namespace CopyHtmlWebSite.MainApp.Services.Converts.Profiles
{
    using AutoMapper;
    using Core.Models;
    using ViewModels;

    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<PageModel, PageViewModel>().ConvertUsing(model => new PageViewModel(null)
            {
                PageSource = model.Source,
                PageName = model.Name
            });
        }
    }
}