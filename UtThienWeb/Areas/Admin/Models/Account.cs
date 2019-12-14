namespace Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Orders = new HashSet<Order>();
        }

        public int AccountId { get; set; }

        [StringLength(50)]
        public string AccountName { get; set; }

        [StringLength(20)]
        public string AccountPassword { get; set; }

        public byte? AccountRole { get; set; }

        [StringLength(50)]
        public string AccountEmail { get; set; }

        [StringLength(100)]
        public string AccountAddress { get; set; }

        [StringLength(12)]
        public string AccountPhone { get; set; }

        [StringLength(256)]
        public string LinkFacebook { get; set; }

        public DateTime? TimeCreate { get; set; }

        [StringLength(255)]
        public string AccountImage { get; set; }

        [StringLength(255)]
        public string AccountUser { get; set; }

        public bool? AccountGender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AccountBOD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
