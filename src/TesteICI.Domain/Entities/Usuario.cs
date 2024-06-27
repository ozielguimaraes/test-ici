namespace TesteICI.Domain.Entities
{
    public class Usuario
    {
        public Usuario() { }
        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public Usuario(long usuarioId, string nome, string email, string senha)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public long UsuarioId { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public void Update(Usuario usuarioUpdated)
        {
            //TODO Update properties
        }
    }
}
