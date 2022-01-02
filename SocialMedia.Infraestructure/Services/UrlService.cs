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

        }
    }
}
