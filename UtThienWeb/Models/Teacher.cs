namespace UtThienWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {
        public int TeacherId { get; set; }

        [StringLength(50)]
        public string TeacherName { get; set; }

        [Column(TypeName = "text")]
        public string TeacherDescription { get; set; }

        public int? Rating { get; set; }

        [StringLength(255)]
        public string TeacherImage { get; set; }
    }
}
