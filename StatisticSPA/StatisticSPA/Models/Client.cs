using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatisticSPA.Models
{
  public class Client
  {
    //public Client()
    //{
    //}

    //public Client()
    //{
    //  this.Groups = new HashSet<Group>();
    //}

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? BirthDate { get; set; }

    public string City { get; set; }

    public string Gender { get; set; }

    public virtual ICollection<Group> Groups { get; set; }
  }
}