using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        {
            return _unitOfWork.PostRepository.GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if(user == null)
            {
                throw new BusinessException("El usuario no existe");      //hacemos esepcion donde verificamos la existencia del usuario 
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if(userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault(); 
                if((DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("Usted solo puede hacer publicaciones una vez por semana");
                }
            }
            if(post.Description.Contains("Sexo") || post.Description.Contains("sexo"))
            {
                throw new BusinessException("Contenido no permitido en la descripción de esta publicación");
            }

            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
