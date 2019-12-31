namespace UtThienWeb.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int OrderId { get; set; }

        public int CourseId { get; set; }

        [Key]
        public int OrderDetailsId { get; set; }
        public int? OrderQuantity { get; set; }
        public virtual Course Course { get; set; }

        public virtual Order Order { get; set; }
    }
}
