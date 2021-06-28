using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace hmfDoNet.Models
{
    public class Cards
    {
        [Required]
        [Display(Name = "投稿分类")]
        //卡片分类
        public String cardClassification { get; set; }
        //卡片标题
        [Required]
        [Display(Name = "文章标题")]

        public String cardTitle { get; set; }
        //卡片内容
        [Required]
        [Display(Name = "文章内容")]

        public String cardContent { get; set; }
        //卡片图片数据
        public String cardImageData { get; set; }
        //卡片图片类型

        public String cardImageType { get; set; }

        public String cardObjectId { get; set; }

    }
}