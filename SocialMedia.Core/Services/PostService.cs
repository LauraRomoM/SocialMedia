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
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository .GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new Exception("User doesn't exist");      //hacemos esepcion donde verificamos la existencia del usuario 
            }

            if(post.Description.Contains("Sexo") || post.Description.Contains("sexo"))
            {
                throw new Exception("Contenido no permitido en la descripción de esta publicación");
            }

            await _postRepository.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _postRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _postRepository.Delete(id);
            return true;
        }
    }
}
