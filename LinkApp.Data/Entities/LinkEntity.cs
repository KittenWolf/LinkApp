using System.ComponentModel.DataAnnotations;

namespace LinkApp.Data.Entities
{
    public class LinkEntity
    {
        [Key]        
        public required string Token { get; set; }        
        public required string LongLink { get; set; }
    }
}
