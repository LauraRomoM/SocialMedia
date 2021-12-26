using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.Entities
{
    public abstract class BaseEntity        //abstracta y que no se generaran instancias de esta clase, solo se va a heredar de ella
    {
        public int Id { get; set; }         //llave primaria id, (como todas nuestras entidades tienen llave primaria de tipo int, colocamos que esa llave se llame id)
    }
}
