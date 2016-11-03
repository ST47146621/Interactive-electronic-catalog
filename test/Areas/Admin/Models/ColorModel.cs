using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class ColorModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("顏色")]   
        public string Name { get; set; }

        [Required]
        [DisplayName("色票代碼")]
        [MaxLength(7, ErrorMessage = "最多輸入7字元")]     
        public string Sharp { get; set; }
    }

    public class ColorNoValidModel
    {

        public int Id { get; set; }

        [Required]
        [DisplayName("顏色")]    
        public string Name { get; set; }
        [Required]
        [DisplayName("色票代碼")]
        [MaxLength(7,ErrorMessage="最多輸入7字元")]
        public string Sharp { get; set; }

    }
}