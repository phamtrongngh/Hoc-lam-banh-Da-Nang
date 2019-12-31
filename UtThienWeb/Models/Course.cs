namespace UtThienWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int CourseId { get; set; }

        [StringLength(100)]
        public string CourseName { get; set; }

        public long? CoursePrice { get; set; }

        [Column(TypeName = "text")]
        public string CourseDescription { get; set; }

        public int? CourseTeacherId { get; set; }

        [StringLength(50)]
        public string CourseDuration { get; set; }

        public DateTime? TimeCreate { get; set; }

        [StringLength(50)]
        public string DateStudy { get; set; }

        [StringLength(255)]
        public string CourseThumbails { get; set; }
        public int CourseCountOrder { get; set; }
        [StringLength(50)]
        public string CourseImagesList { get; set; }
        
        public int CourseCatalogId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseTeacherName { get; set; }

        public virtual CourseCatalog CourseCatalog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
