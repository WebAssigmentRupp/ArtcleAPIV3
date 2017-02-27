namespace ArticleAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ArtUser")]
    public partial class ArtUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ArtUser()
        {
            menus = new HashSet<menu>();
            pages = new HashSet<page>();
            posts = new HashSet<post>();
        }

        public short id { get; set; }

        [Required]
        [StringLength(30)]
        public string name { get; set; }

        [Required]
        [StringLength(30)]
        public string email { get; set; }

        [Required]
        [StringLength(30)]
        public string firstname { get; set; }

        [Required]
        [StringLength(30)]
        public string lastname { get; set; }

        [Required]
        [StringLength(1)]
        public string gender { get; set; }

        public string passwd { get; set; }

        public short role_id { get; set; }
        public virtual UserRole role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menu> menus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<page> pages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<post> posts { get; set; }
    }
}
