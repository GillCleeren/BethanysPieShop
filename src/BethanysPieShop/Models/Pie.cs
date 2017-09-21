using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Models
{
    public class Pie
    {
        public int PieId { get; set; }

        [Remote("CheckIfPieNameAlreadyExists", "PieManagement", ErrorMessage = "That name already exists")]
        public string Name { get; set; }

        [MaxLength(100)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string AllergyInformation { get; set; }
        public decimal Price { get; set; }

        [ValidUrl(ErrorMessage = "That's not a valid URL")]
        public string ImageUrl { get; set; }

        [ValidUrl(ErrorMessage = "That's not a valid URL")]
        public string ImageThumbnailUrl { get; set; }
        public bool IsPieOfTheWeek { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<PieReview> PieReviews { get; set; }

        //Specific for Model Binding
        public SugarLevel SugarLevel { get; set; }
        public RecipeInformation RecipeInformation { get; set; }


    }
}
