using BookStore.Model.Models;
using BookStore.Web.Models;
using System;

namespace BookStore.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdateContactDetail(this ContactDetail contactDetail, ContactDetailViewModel contactDetailVm)
        {
            contactDetail.ID = contactDetailVm.ID;
            contactDetail.CompanyName = contactDetailVm.CompanyName;
            contactDetail.CompanyEmail = contactDetailVm.CompanyEmail;
            contactDetail.CompanyAddress = contactDetailVm.CompanyAddress;
            contactDetail.CompanyPhone = contactDetailVm.CompanyPhone;
            contactDetail.Lat = contactDetailVm.Lat;
            contactDetail.Lng = contactDetailVm.Lng;         
            contactDetail.CompanyWebsite = contactDetailVm.CompanyWebsite;
            contactDetail.CompanyFax = contactDetailVm.CompanyFax;
        }

        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVm)
        {
            feedback.Name = feedbackVm.Name;
            feedback.Email = feedbackVm.Email;
            feedback.Message = feedbackVm.Message;
            feedback.Phone = feedbackVm.Phone;
            feedback.Status = feedbackVm.Status;
            feedback.CreatedDate = DateTime.Now;
        }

        public static void UpdatePage(this Page page, PageViewModel pageVm)
        {
            page.ID = pageVm.ID;
            page.Name = pageVm.Name;
            page.Alias = pageVm.Alias;
            page.DisplayOrder = pageVm.DisplayOrder;
            page.Content = pageVm.Content;
            page.CreatedDate = pageVm.CreatedDate;
            page.CreatedBy = pageVm.CreatedBy;
            page.UpdatedDate = pageVm.UpdatedDate;
            page.UpdatedBy = pageVm.UpdatedBy;
            page.MetaKeyword = pageVm.MetaKeyword;
            page.MetaDescription = pageVm.MetaDescription;
            page.Status = pageVm.Status;
        }
        public static void UpdateUser(this User user, UserViewModel userVm)
        {
            user.ID = userVm.ID;
            user.Name = userVm.Name;
            user.GroupID = userVm.GroupID;
            user.Address = userVm.Address;
            user.Email = userVm.Email;
            user.Phone = userVm.Phone;
        }
        public static void UpdateUserGroup(this UserGroup userGr, UserGroupViewModel userGrVm)
        {
            userGr.ID = userGrVm.ID;
            userGr.Name = userGrVm.Name;           
        }
        public static void UpdateSlide(this Slide slide, SlideViewModel slideVm)
        {
            slide.ID = slideVm.ID;
            slide.Name = slideVm.Name;
            slide.Image = slideVm.Image;
            slide.Url = slideVm.Url;
            slide.DisplayOrder = slideVm.DisplayOrder;
            slide.Status = slideVm.Status;
        }

        public static void UpdateBanner(this Banner banner, BannerViewModel bannerVm)
        {
            banner.ID = bannerVm.ID;
            banner.Name = bannerVm.Name;
            banner.Image = bannerVm.Image;
            banner.Url = bannerVm.Url;
            banner.Type = bannerVm.Type;           
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Alias = productCategoryVm.Alias;       
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;       
            productCategory.CreatedDate = productCategoryVm.CreatedDate;
            productCategory.CreatedBy = productCategoryVm.CreatedBy;
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.Status = productCategoryVm.Status;
        }

        public static void UpdateProvider(this Provider provider, ProviderViewModel providerVm)
        {
            provider.ID = providerVm.ID;
            provider.Name = providerVm.Name;          
            provider.CreatedDate = providerVm.CreatedDate;
            provider.CreatedBy = providerVm.CreatedBy;
            provider.UpdatedDate = providerVm.UpdatedDate;
            provider.UpdatedBy = providerVm.UpdatedBy;
            provider.MetaKeyword = providerVm.MetaKeyword;
            provider.MetaDescription = providerVm.MetaDescription;
            provider.Status = providerVm.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Alias = postVm.Alias;         
            post.Image = postVm.Image;
            post.Description = postVm.Description;
            post.Content = postVm.Content;          
            post.ViewCount = postVm.ViewCount;
            post.CreatedDate = postVm.CreatedDate;
            post.CreatedBy = postVm.CreatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }

        public static void UpdateProduct(this Product product, ProductViewModel productVm)
        {
            product.ID = productVm.ID;
            product.Name = productVm.Name;
            product.Alias = productVm.Alias;                  
            product.CategoryID = productVm.CategoryID;
            product.ProviderID = productVm.ProviderID;
            product.Quantity = productVm.Quantity;                        
            product.Image = productVm.Image;
            product.Description = productVm.Description;
            product.Content = productVm.Content;        
            product.MoreImages = productVm.MoreImages;
            product.PromotionPrice = productVm.PromotionPrice;
            product.Price = productVm.Price;          
            product.SaleFlag = productVm.SaleFlag;
            product.HomeFlag = productVm.HomeFlag;
            product.HotFlag = productVm.HotFlag;
            product.ViewCount = productVm.ViewCount;
            product.CreatedDate = productVm.CreatedDate;
            product.CreatedBy = productVm.CreatedBy;
            product.UpdatedDate = productVm.UpdatedDate;
            product.UpdatedBy = productVm.UpdatedBy;
            product.MetaKeyword = productVm.MetaKeyword;
            product.MetaDescription = productVm.MetaDescription;
            product.Status = productVm.Status;           
        }


        //public static void UpdatePost(this Post post, PostViewModel postVm)
        //{
        //    post.ID = postVm.ID;
        //    post.Name = postVm.Name;
        //    post.Description = postVm.Description;
        //    post.Alias = postVm.Alias;
        //    post.CategoryID = postVm.CategoryID;
        //    post.Content = postVm.Content;
        //    post.Image = postVm.Image;
        //    post.ViewCount = postVm.ViewCount;
        //    post.CreatedDate = postVm.CreatedDate;
        //    post.CreatedBy = postVm.CreatedBy;
        //    post.UpdatedDate = postVm.UpdatedDate;
        //    post.UpdatedBy = postVm.UpdatedBy;
        //    post.MetaKeyword = postVm.MetaKeyword;
        //    post.MetaDescription = postVm.MetaDescription;
        //    post.EventFlag = postVm.EventFlag;
        //    post.Status = postVm.Status;
        //    post.HotFlag = postVm.HotFlag;
        //    post.Tags = postVm.Tags;
        //    post.HotNewsFlag = postVm.HotNewsFlag;
        //}

        //public static void UpdateUserGroup(this UserGroup userGroup, UserGroupViewModel userGroupVm)
        //{
        //    userGroup.ID = userGroupVm.ID;
        //    userGroup.Name = userGroupVm.Name;
        //}

        //public static void UpdateUser(this User user, UserViewModel userVm)
        //{
        //    user.ID = userVm.ID;
        //    user.Name = userVm.Name;
        //    user.UserName = userVm.UserName;
        //    user.Password = userVm.Password;
        //    user.GroupID = userVm.GroupID;
        //    user.Address = userVm.Address;
        //    user.Email = userVm.Email;
        //    user.Phone = userVm.Phone;
        //}    

        //public static void UpdateBanner(this Banner banner, BannerViewModel bannerVm)
        //{
        //    banner.ID = bannerVm.ID;
        //    banner.Name = bannerVm.Name;
        //    banner.Description = bannerVm.Description;
        //    banner.Image = bannerVm.Image;
        //    banner.Url = bannerVm.Url;
        //    banner.Type = bannerVm.Type;
        //}
    }
}