﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Migration.Toolkit.KXP.Models
{
    [Table("DancingGoatCore_SocialLink")]
    public partial class DancingGoatCoreSocialLink
    {
        [Key]
        [Column("SocialLinkID")]
        public int SocialLinkId { get; set; }
        [StringLength(30)]
        public string SocialLinkName { get; set; } = null!;
        [StringLength(50)]
        public string SocialLinkTitle { get; set; } = null!;
        [StringLength(200)]
        public string SocialLinkUrl { get; set; } = null!;
        public string SocialLinkIcon { get; set; } = null!;
    }
}
