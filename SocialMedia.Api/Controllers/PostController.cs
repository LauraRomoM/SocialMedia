using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]       //para consulta de todas las publicaciones o recursos
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);    //convertimos a tipo de dato IEnumerable(es un listado de PostDto enumerable)
            
            return Ok(postsDto);
        }

        [HttpGet("{id}")]      //para consultar publicacion o recurso especifico
        public async Task<IActionResult> GetPost(int id)        //llamado de un unico Post
        {
            var post = await _postRepository.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        [HttpPost]      //para publicacion de recursos
        public async Task<IActionResult> Post(PostDto postDto)        //llamado de un unico Post
        {
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.InsertPost(post);
            return Ok(post);
        }

        [HttpPut]      //para actualizacion de publicacion o recurso especifico
        public async Task<IActionResult> Put(int id, PostDto postDto)        //llamado de un unico Post
        {  
            var post = _mapper.Map<Post>(postDto);
            post.PostId = id;       //para garantizar que el esa entidad tenga el id que quiero actualizar

            await _postRepository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("{id}")]      //para actualizacion de publicacion o recurso especifico
        public async Task<IActionResult> Put(int id)        //llamado de un unico Post
        {
            var result = await _postRepository.DeletePost(id);
            return Ok(result);
        }

    }
}
