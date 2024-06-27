namespace TesteICI.Domain.Entities
{
    public class NoticiaTag
    {
        public long NoticiaTagId { get; set; }
        public string Descricao { get; private set; }
		
        public void Update(NoticiaTag noticiaTagUpdated)
        {
            //TODO Update properties
        }
    }
}
