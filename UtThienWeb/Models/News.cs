namespace UtThienWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            Images = new HashSet<Image>();
        }

        public int NewsId { get; set; }

        [StringLength(255)]
        public string NewsTitle { get; set; }

        [StringLength(255)]
        public string NewsShortContent { get; set; }

        [Column(TypeName = "text")]
        public string NewsContent { get; set; }

        [StringLength(50)]
        public string NewsAuthor { get; set; }

        public DateTime? NewsDate { get; set; }

        public int? NewsTypeId { get; set; }

        [StringLength(255)]
        public string NewsThumbails { get; set; }

        [StringLength(255)]
        public string NewsImagesList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        public virtual NewsCatalog NewsCatalog { get; set; }
    }
}
