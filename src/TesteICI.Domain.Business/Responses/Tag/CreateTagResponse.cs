namespace TesteICI.Domain.Business.Responses.Tag
{
    public class CreateTagResponse : BaseResponse
    {
        public CreateTagResponse(long tagId)
        {
            TagId = tagId;
        }

        public long TagId { get; private set; }

    }
}
