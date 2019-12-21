namespace UtThienWeb.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Form")]
    public partial class Form
    {
        public int FormId { get; set; }

        [StringLength(50)]
        public string FormEmail { get; set; }

        [StringLength(60)]
        public string FormName { get; set; }

        [StringLength(12)]
        public string FormPhone { get; set; }

        public int NewsId { get; set; }

        public string NewsTitle { get; set; }

        [Column(TypeName = "text")]
        public string FormExperience { get; set; }
        public DateTime FormCreationDate { get; set; }
        [StringLength(20)]
        public string FormStatus { get; set; }

        public virtual News News { get; set; }
    }
}
