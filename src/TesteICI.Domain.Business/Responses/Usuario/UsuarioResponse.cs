namespace TesteICI.Domain.Business.Responses.Usuario
{
    public class UsuarioResponse : BaseResponse
    {
        public UsuarioResponse(long usuarioId, string nome, string email, string senha)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public long UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
    }
}
