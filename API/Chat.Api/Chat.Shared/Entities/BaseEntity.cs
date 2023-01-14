using System.ComponentModel.DataAnnotations.Schema;

namespace Chat.Shared.Entities
{
    public class BaseEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}

