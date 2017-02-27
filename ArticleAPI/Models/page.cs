namespace ArticleAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("page")]
    public partial class page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public page()
        {
            menus = new HashSet<menu>();
        }

        public short id { get; set; }

        [Required]
        [StringLength(100)]
        public string url { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Required]
        public string contents { get; set; }

        public short user_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime created_date { get; set; }

        public virtual ArtUser user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menu> menus { get; set; }
    }
}
