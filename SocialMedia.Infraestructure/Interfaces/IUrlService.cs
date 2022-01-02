using SocialMedia.Core.QueryFilters;
using System;

namespace SocialMedia.Infraestructure.Interfaces
{
    public interface IUrlService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}