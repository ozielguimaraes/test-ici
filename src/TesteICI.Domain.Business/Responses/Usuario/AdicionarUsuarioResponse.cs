namespace TesteICI.Domain.Business.Responses.Usuario
{
    public class AdicionarUsuarioResponse : BaseResponse
    {
        public AdicionarUsuarioResponse(long usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public long UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
    }
}
