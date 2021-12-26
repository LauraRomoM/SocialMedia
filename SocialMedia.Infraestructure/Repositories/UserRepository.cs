using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UserRepository
    {
        private readonly SocialMediaContext _context;
        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }
    }
}
