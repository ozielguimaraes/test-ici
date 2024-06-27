using System.Linq.Expressions;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces;
using TesteICI.Domain.Interfaces.Repositories;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUnitOfWork uow, IUsuarioRepository usuarioRepository) : base(uow)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Add(Usuario usuario)
        {
            BeginTransaction();
            usuario = _usuarioRepository.Add(usuario);
            await CommitAsync();
            return usuario;
        }

        public async Task<Usuario> Update(Usuario usuarioUpdated)
        {
            BeginTransaction();
            var usuario = await GetById(usuarioUpdated.UsuarioId);
            usuario.Update(usuarioUpdated);
            usuarioUpdated = _usuarioRepository.Update(usuario);
            await CommitAsync();
            return usuarioUpdated;
        }

        public async Task Remove(long usuarioId)
        {
            BeginTransaction();
            _usuarioRepository.Remove(await GetById(usuarioId));
            await CommitAsync();
        }

        public async Task<Usuario> GetById(long usuarioId)
        {
            var usuario = await _usuarioRepository.GetById(usuarioId);

            if (usuario is null)
                throw new Exception("Usuário não encontrado.");

            return usuario;
        }

        public async virtual Task<bool> HasAny(Expression<Func<Usuario, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.HasAnyAsync(predicate, cancellationToken);
        }

        public async Task<IQueryable<Usuario>> Filter(Expression<Func<Usuario, bool>> predicate)
        {
            return _usuarioRepository.Filter(predicate);
        }

        public IQueryable<Usuario> All()
        {
            return _usuarioRepository.All();
        }

        public void Dispose()
        {
            _usuarioRepository.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> EmailExiste(string email, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.HasAnyAsync(x => x.Email == email.ToLower(), cancellationToken);
        }
    }
}
