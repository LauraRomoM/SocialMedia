using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        private readonly DbSet<T> _entities;
        public BaseRepository(SocialMediaContext context)       //constructor
        {
            _context = context;
            _entities = context.Set<T>();       //Registramos matriculamos la entidad de tipo T (este puede ser Post, Update, Delete, etc)
        }

    }
}
