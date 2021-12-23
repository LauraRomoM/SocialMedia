using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

//Dentro de los DTO's no debe haber codificaciones lógicas 
// solo debe haber simplemente objetos planos para transmitir informacion 
namespace SocialMedia.Core.DTOs
{
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }

        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
