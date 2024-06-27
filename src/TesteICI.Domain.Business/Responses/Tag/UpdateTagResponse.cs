namespace TesteICI.Domain.Business.Responses.Tag
{
    public class UpdateTagResponse : BaseResponse
    {
        public UpdateTagResponse(long tagId, string descricao)
        {
            TagId = tagId;
            Descricao = descricao;
        }

        public long TagId { get; private set; }
        public string Descricao { get; private set; }
    }
}
