using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IRepository<Post> _postRepository;         //Repository generico para entidad de Post
        private readonly IRepository<User> _userRepository;         //Repository generico para la entidad User
        private readonly IRepository<Coment> _comentRepository;         //Repository generico para entidad de Coment

        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IRepository<Post> PostRepository => _postRepository ?? new BaseRepository<Post>(_context);

        public IRepository<User> UserRepository => throw new NotImplementedException();

        public IRepository<Coment> ComentRepository => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
