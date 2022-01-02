<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
﻿using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< 1144280c3556c900f60cfa5a94720c9f941ec396
=======
﻿using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
>>>>>>> Infraestructure.Services UrlService (hereda de interfaz)
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Services
{
    public class UrlService : IUrlService
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
