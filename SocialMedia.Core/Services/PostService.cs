using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Services
{
    public class PostService
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
    }
}
