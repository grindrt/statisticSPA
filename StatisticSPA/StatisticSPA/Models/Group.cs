using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatisticSPA.Models
{
  public class Group
  {
    //public Group()
    //{
    //}

    //public Group()
    //{
    //  this.Clients = new HashSet<Client>();
    //}

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }

    public string Color { get; set; }

    public virtual ICollection<Client> Clients { get; set; }
  }
}