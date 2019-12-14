namespace UtThienWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int ImageId { get; set; }

        [StringLength(255)]
        public string ImageLink { get; set; }

        [StringLength(50)]
        public string ImageTypeName { get; set; }

        public int ImageTypeId { get; set; }

        public virtual Course Course { get; set; }

        public virtual News News { get; set; }
    }
}
