using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]       //API de publicaciones
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]      //API de una publicacion especifica
        public async Task<IActionResult> GetPost(int id)        //llamado de un unico Post
        {
            var post = await _postRepository.GetPost(id);
            return Ok(post);
        }


        [HttpPost]      //API de una publicacion especifica
        public async Task<IActionResult> Post(Post post)        //llamado de un unico Post
        {
            var post = await _postRepository.GetPost(id);
            return Ok(post);
        }


    }
}
