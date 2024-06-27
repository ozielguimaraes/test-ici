using System;

namespace TesteICI.Domain.Business.Responses.Tag
{
    public class UpdateTagResponse : BaseResponse
    {
        public UpdateTagResponse(long tagId)
        {
            TagId = tagId;
        }

        public long TagId { get; private set; }
        public string Descricao { get; private set; }
		
    }
}
