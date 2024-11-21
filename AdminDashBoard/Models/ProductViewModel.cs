﻿using Store.G04.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }

        public IFormFile Image { get; set; } //
        public string? PictureUrl { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 100000)]

        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Brand Id is Required")]
        public int BrandId { get; set; }
        public ProductBrand? Brand { get; set; }

        [Required(ErrorMessage = "Prouct Type Id is Required")]

        public int TypeId { get; set; }
        public ProductType? Type { get; set; }
    }
}
