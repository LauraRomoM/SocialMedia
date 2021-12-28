using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }
        
        [HttpGet]       //para consulta de todas las publicaciones o recursos
        public IActionResult GetPosts(int? userId, DateTime? date, string description)
        {
            var posts = _postService.GetPosts();
            var postsDtos = _mapper.Map<IEnumerable<PostDto>>(posts);    //convertimos a tipo de dato IEnumerable(es un listado de PostDto enumerable)
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDtos);
            return Ok(response);
        }

        [HttpGet("{id}")]      //para consultar publicacion o recurso especifico
        public async Task<IActionResult> GetPost(int id)        //llamado de un unico Post
        {
            var post = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]      //para publicacion de recursos
        public async Task<IActionResult> Post(PostDto postDto)        //llamado de un unico Post
        {
            var post = _mapper.Map<Post>(postDto);

            await _postService.InsertPost(post);

            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(post);
        }

        [HttpPut]      //para actualizacion de publicacion o recurso especifico
        public async Task<IActionResult> Put(int id, PostDto postDto)        //llamado de un unico Post
        {  
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;       //para garantizar que el esa entidad tenga el id que quiero actualizar
                        
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]      //para actualizacion de publicacion o recurso especifico
        public async Task<IActionResult> Delete(int id)        //llamado de un unico Post
        {
            var result = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);           //hacemos bool la respuesta result y lo guardamos en variable response
            return Ok(response);
        }

    }
}
