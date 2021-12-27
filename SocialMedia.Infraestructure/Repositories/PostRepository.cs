using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext context) : base(context) { }  //enviamos el context a la clase padre a traves del metodo base()
        
        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
