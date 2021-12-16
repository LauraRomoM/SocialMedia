using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        // en este caso indicamos que en ligar de recibir lista de publicaciones(Post), recibiremos un unico Post
        public async Task<Post> GetPost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.UserId == id);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            _context.Posts.Add post
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

    }
}

