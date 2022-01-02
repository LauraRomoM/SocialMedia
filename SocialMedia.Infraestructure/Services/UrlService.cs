using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Services
{
    public class UrlService
    {
        private readonly string _baseUri;        //inlleccion de dependencias
        public UrlService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";        //interpolacion de string (concatenacion de _baseUrl con actionUrl)
            return new Uri(baseUrl);
        }
    }
}
