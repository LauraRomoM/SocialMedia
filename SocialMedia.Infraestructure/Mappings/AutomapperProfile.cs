using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Mappings
{
    public class AutomapperProfile : Profile     //Definimos clase publica que hereda de la clase Profile
    {
        public AutomapperProfile()      //definimos constructor
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    };
}
