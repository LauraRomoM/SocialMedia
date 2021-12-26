using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Responses
{
    //esta clase siempre se va a retornar en los CRUD a traves de nuestra APi,
    public class ApiResponse<T>     //a esta clase para usarla, se especifica sobre que tipo de objeto (tipo de dato) se debe comportar (bool, Post, etc.) 
    {
        public ApiResponse(T data)      //constructor para recibir la informacion de tipo T
        {
            Data = data;                //establecemos valor de data de tipo T a nuestra propiedad Data
        }

        public T Data { get; set; }       //propiedad Data de tipo T, en Data enviamos los resultados de post especificos, si hacemos insert, en Data se guarda la info de esta insercion, para el Delete hacemos solo un bool
    }
}
