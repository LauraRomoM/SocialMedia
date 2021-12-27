using SocialMedia.Infraestructure.Data;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UnitOfWork
    {
        private readonly SocialMediaContext _context;
        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }
    }
}
