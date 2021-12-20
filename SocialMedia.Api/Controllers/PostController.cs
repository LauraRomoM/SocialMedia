using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]       //API de publicaciones
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);    //convertimos a tipo de dato IEnumerable(es un listado de PostDto enumerable)
            
            return Ok(postsDto);
        }

        [HttpGet("{id}")]      //API de una publicacion especifica
        public async Task<IActionResult> GetPost(int id)        //llamado de un unico Post
        {
            var post = await _postRepository.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }


        [HttpPost]      //API de una publicacion especifica
        public async Task<IActionResult> Post(Post post)        //llamado de un unico Post
        {
            var post = await _postRepository.GetPost(id);
            return Ok(post);
        }


    }
}
