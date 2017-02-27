namespace ArticleAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("menu")]
    public partial class menu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public menu()
        {
            submenu = new HashSet<menu>();
        }

        public short id { get; set; }

        [Required]
        [StringLength(30)]
        public string title { get; set; }

        public short? parent_id { get; set; }

        public short user_id { get; set; }

        public short page_id { get; set; }

        public virtual ArtUser user { get; set; }

        public virtual page page { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<menu> submenu { get; set; }

        public virtual menu parent { get; set; }
    }
}
