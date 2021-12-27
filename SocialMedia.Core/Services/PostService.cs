using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{

    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;         //Repository generico para entidad de Post
        private readonly IRepository<User> _userRepository;         //Repository generico para la entidad User
        public PostService(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUser(post.UserId);
            if(user == null)
            {
                throw new Exception("User doesn't exist");      //hacemos esepcion donde verificamos la existencia del usuario 
            }

            if(post.Description.Contains("Sexo") || post.Description.Contains("sexo"))
            {
                throw new Exception("Contenido no permitido en la descripción de esta publicación");
            }

            await _postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }
    }
}
