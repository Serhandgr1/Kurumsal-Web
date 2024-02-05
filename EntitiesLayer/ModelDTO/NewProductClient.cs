﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer.ModelDTO
{
    public class NewProductClient
    {
        public int? Id { get; set; }
        public string ProductDetail { get; set; }
        public string? ProductSpacial { get; set; }
        public string ProductTitle { get; set; }
        public string CategoryName { get; set; }
        public IFormFile ProductImage { get; set; }
        public IFormFile? ProductImage2 { get; set; }
        public IFormFile? ProductImage3 { get; set; }
    }
}
