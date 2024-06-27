using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses.Noticia;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Implementations
{
    public class NoticiaBusiness : INoticiaBusiness
    {
        private readonly INoticiaService _noticiaService;

        public NoticiaBusiness(INoticiaService noticiaService)
        {
            _noticiaService = noticiaService;
        }

        public async Task<CreateNoticiaResponse> Create(CreateNoticiaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrWhiteSpace(request.Titulo))
                throw new ArgumentNullException(nameof(request.Titulo));
            if (string.IsNullOrWhiteSpace(request.Texto))
                throw new ArgumentNullException(nameof(request.Texto));

            if (request.UsuarioId == 0)
                throw new ArgumentNullException(nameof(request.UsuarioId));

            var noticia = new Noticia(request.Titulo, request.Texto, request.UsuarioId);

            var result = await _noticiaService.Add(noticia);

            return new CreateNoticiaResponse(result.NoticiaId);
        }

        public async Task<UpdateNoticiaResponse> Update(UpdateNoticiaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrWhiteSpace(request.Titulo))
                throw new ArgumentNullException(nameof(request.Titulo));
            if (string.IsNullOrWhiteSpace(request.Texto))
                throw new ArgumentNullException(nameof(request.Texto));

            if (request.UsuarioId == 0)
                throw new ArgumentNullException(nameof(request.UsuarioId));

            var noticia = new Noticia(request.NoticiaId, request.Titulo, request.Texto, request.UsuarioId);
            var result = await _noticiaService.Update(noticia);
            return new UpdateNoticiaResponse(result.NoticiaId);
        }

        public async Task<List<NoticiaResponse>> GetAll()
        {
            var result = _noticiaService.All();
            return await Task.FromResult(result.Select(x => MapearParaResposta(x)).ToList());
        }

        public async Task<NoticiaResponse> GetById(long NoticiaId)
        {
            var result = await _noticiaService.GetById(NoticiaId);

            return MapearParaResposta(result);
        }

        private NoticiaResponse MapearParaResposta(Noticia noticia)
        {
            //TODO Usar automapper ou algo do tipo
            return new NoticiaResponse(noticia.NoticiaId, noticia.Titulo, noticia.Texto, noticia.UsuarioId);
        }
    }
}
