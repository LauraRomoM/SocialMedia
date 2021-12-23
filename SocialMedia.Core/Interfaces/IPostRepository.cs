using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();

        Task<Post> GetPost(int id);         //Firmas de metodos anexadas

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);
    }
}
//Generar BD colocando el comando en El NuGet Pakage Manager Console, comando:
//Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=SocialMedia;Integrated Security = true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data
