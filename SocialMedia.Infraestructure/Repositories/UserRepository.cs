using SocialMedia.Infraestructure.Data;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediaContext _context;
        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }
    }
}
