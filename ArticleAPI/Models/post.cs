namespace ArticleAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("post")]
    public partial class post
    {
        public short id { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Required]
        public string texts { get; set; }

        public string image { get; set; }

        [Column(TypeName = "date")]
        public DateTime post_date { get; set; }

        [Required]
        [StringLength(30)]
        public string author { get; set; }

        public short category_id { get; set; }

        public short? user_id { get; set; }

        public virtual ArtUser user { get; set; }

        public virtual category category { get; set; }
    }
}
