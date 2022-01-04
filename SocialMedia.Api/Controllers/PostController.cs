using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        ///     Retry all posts
        /// </summary>
        /// <param name="filters"> Filters to apply </param>
        /// <returns></returns>
        [HttpGet (Name = nameof(GetPosts)) ]            //decoracion para obtener url generica
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)
        {
            var posts = _postService.GetPosts(filters);
            var postsDtos = _mapper.Map<IEnumerable<PostDto>>(posts);    //convertimos a tipo de dato IEnumerable(es un listado de PostDto enumerable)

            var metadata = new Metadata             //creando objeto tipo Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPages = posts.HasNextPage,
                HasPreviousPages = posts.HasPreviousPage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),      
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDtos)
            {
                Meta = metadata         //Meta (ubicado en ApiResponse), es igual al objeto metadata (declarado justo arriba en linea 39) 
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));        //JsonConvert.SerializeObject(), devuelve string del objeto metadata en formato Json

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
