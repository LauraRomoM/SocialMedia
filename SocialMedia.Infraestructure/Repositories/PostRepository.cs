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
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var currentPost = await GetPost(post.PostId);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;
            //currentPost.UserId = post.UserId;     El usuario no es logico o comun que cambie, asi que no lo incluimos para actualizacion del mismo

            int rows = await _context.SaveChangesAsync();  //guardamos los cambios
            return rows > 0;    
        }

        public async Task<bool> DeletePost(int id)
        {
            var currentPost = await GetPost(id);
            _context.Posts.Remove(currentPost);
            //currentPost.UserId = post.UserId;     El usuario no es logico o comun que cambie, asi que no lo incluimos para actualizacion del mismo

            int rows = await _context.SaveChangesAsync();  //guardamos los cambios
            return rows > 0;
        }

    }
}

