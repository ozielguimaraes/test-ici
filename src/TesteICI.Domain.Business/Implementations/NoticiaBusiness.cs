using FluentValidation;
using TesteICI.Domain.Business.Interfaces;
using TesteICI.Domain.Business.Requests.Noticia;
using TesteICI.Domain.Business.Responses;
using TesteICI.Domain.Business.Responses.Noticia;
using TesteICI.Domain.Entities;
using TesteICI.Domain.Interfaces.Services;

namespace TesteICI.Domain.Business.Implementations
{
    public class NoticiaBusiness : INoticiaBusiness
    {
        private readonly INoticiaService _noticiaService;
        private readonly IValidator<AdicionarNoticiaRequest> _adicionarValidator;
        private readonly IValidator<EditarNoticiaRequest> _editarValidator;

        public NoticiaBusiness(INoticiaService noticiaService, IValidator<AdicionarNoticiaRequest> adicionarValidator, IValidator<EditarNoticiaRequest> editarValidator)
        {
            _noticiaService = noticiaService;
            _adicionarValidator = adicionarValidator;
            _editarValidator = editarValidator;
        }

        public async Task<AdicionarNoticiaResponse> Adicionar(AdicionarNoticiaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var resultadoValidacao = await _adicionarValidator.ValidateAsync(request);

            if (!resultadoValidacao.IsValid)
                return new AdicionarNoticiaResponse(resultadoValidacao);

            var noticia = new Noticia(request.Titulo, request.Texto, request.UsuarioId);

            var result = await _noticiaService.Add(noticia);

            return new AdicionarNoticiaResponse(result.NoticiaId);
        }

        public async Task<EditarNoticiaResponse> Editar(EditarNoticiaRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);


            var resultadoValidacao = await _editarValidator.ValidateAsync(request);

            if (!resultadoValidacao.IsValid)
                return new EditarNoticiaResponse(resultadoValidacao);
            var noticia = new Noticia(request.NoticiaId, request.Titulo, request.Texto, request.UsuarioId);

            var result = await _noticiaService.Update(noticia);

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

        public async Task<List<NoticiaResponse>> GetAll()
        {
            var result = _noticiaService.All();
            return await Task.FromResult(result.Select(x => MapearParaResposta(x)).ToList());
        }

        public async Task<BaseResponse> ObterPorId(long noticiaId)
        {
            var noticia = await _noticiaService.GetById(noticiaId);

            if (noticia is null)
                return new NullResponse();

            return MapearParaResposta(noticia);
        }

        private NoticiaResponse MapearParaResposta(Noticia noticia)
        {
            //TODO Usar automapper ou algo do tipo
            return new NoticiaResponse(noticia.NoticiaId, noticia.Titulo, noticia.Texto, noticia.UsuarioId);
        }
    }
}
