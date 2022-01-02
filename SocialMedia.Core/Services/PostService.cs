using SocialMedia.Core.CustomEntities;
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

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? 1 : filters.PageNumber;      //si numero de paginas es 0, mostrar una pagina por defecto
            filters.PageSize = filters.PageSize == 0 ? 20 : filters.PageSize;           //si tamaño de pagina es 0, mostrar por defecto como minimo de tamaño de pagina 20 registros 

            var posts = _unitOfWork.PostRepository.GetAll();
            if (filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }
            if (filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if (filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            //La paginacion va despues de la serie de filtros hechos 
            var pagePosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);        //paginacion
            return pagePosts;
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
