using AutoMapper;
using BookStore.Model.Models;
using BookStore.Web.Models;

namespace BookStore.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Banner, BannerViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<Provider, ProviderViewModel>();
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<UserGroup, UserGroupViewModel>();
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();
            Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();
            Mapper.CreateMap<Feedback, FeedbackViewModel>();
        }
    }
}