using FluentValidation;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;
using TesteICI.Domain.Business.Responses.Tag;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Services;
using TesteICI.Infra.CrossCutting.Security.Shared;

namespace TesteICI.Domain.Business.Implementations
{
    public class NoticiaBusiness : INoticiaBusiness
    {
        private readonly INoticiaService _noticiaService;
        private readonly IValidator<AdicionarNoticiaRequest> _adicionarValidator;
        private readonly IValidator<EditarNoticiaRequest> _editarValidator;
        private readonly IUser _user;

        public NoticiaBusiness(INoticiaService noticiaService, IValidator<AdicionarNoticiaRequest> adicionarValidator, IValidator<EditarNoticiaRequest> editarValidator, IUser user)
        {
            _noticiaService = noticiaService;
            _adicionarValidator = adicionarValidator;
            _editarValidator = editarValidator;
            _user = user;
        }

        public async Task<AdicionarNoticiaResponse> Adicionar(AdicionarNoticiaRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            request.UsuarioId = _user.GetUserId();

            var resultadoValidacao = await _adicionarValidator.ValidateAsync(request);

            if (!resultadoValidacao.IsValid)
                return new AdicionarNoticiaResponse(resultadoValidacao);

            var noticia = new Noticia(request.Titulo, request.Texto, request.UsuarioId);
            noticia.InformarTags(request.TagIds);

            var result = await _noticiaService.Adicionar(noticia, cancellationToken);

            return new AdicionarNoticiaResponse(result.NoticiaId);
        }

        public async Task<EditarNoticiaResponse> Editar(EditarNoticiaRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            request.UsuarioId = _user.GetUserId();

            var resultadoValidacao = await _editarValidator.ValidateAsync(request, cancellationToken);

            if (!resultadoValidacao.IsValid)
                return new EditarNoticiaResponse(resultadoValidacao);
            var noticia = new Noticia(request.NoticiaId, request.Titulo, request.Texto, _user.GetUserId());

            var result = await _noticiaService.Editar(noticia, cancellationToken);

            if (result is null)
                return new EditarNoticiaResponse(new FluentValidation.Results.ValidationResult
                {
                    Errors = new List<FluentValidation.Results.ValidationFailure>
                {
                    new FluentValidation.Results.ValidationFailure("NoticiaId", "Noticia não encontrada")
                }
                });

            return new EditarNoticiaResponse(result.NoticiaId);
        }

        public async Task<BaseResponse> Deletar(long usuarioId, CancellationToken cancellationToken)
        {
            var foiDeletado = await _noticiaService.Deletar(usuarioId, cancellationToken);

            if (foiDeletado)
                return new EmptyResponse();

            return new NullResponse();
        }

        public async Task<IList<NoticiaResponse>> ObterTodas(CancellationToken cancellationToken)
        {
            var noticias = await _noticiaService.ObterTodas(cancellationToken);

            return noticias.Select(x => MapearParaResposta(x)).ToList();
        }

        public async Task<BaseResponse> ObterPorId(long noticiaId, CancellationToken cancellationToken)
        {
            var noticia = await _noticiaService.ObterPorId(noticiaId, cancellationToken);

            if (noticia is null)
                return new NullResponse();

            return MapearParaResposta(noticia);
        }

        private NoticiaResponse MapearParaResposta(Noticia noticia)
        {
            //TODO poderia Usar automapper ou algo do tipo
            var tags = new List<NoticiaTagResponse>();

            foreach (var tag in noticia.Tags)
            {
                if (tag.Tag is not null)
                    tags.Add(new NoticiaTagResponse(tag.NoticiaTagId, tag.TagId, new TagResponse(tag.Tag)));
                else
                    tags.Add(new NoticiaTagResponse(tag.NoticiaTagId, tag.TagId));
            }

            return new NoticiaResponse(noticia.NoticiaId, noticia.Titulo, noticia.Texto, noticia.UsuarioId, tags: tags);
        }
    }
}
