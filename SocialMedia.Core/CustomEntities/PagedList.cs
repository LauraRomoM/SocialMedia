using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialMedia.Core.CustomEntities
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }    //Pagina actual 
        public int TotalPages { get; set; }     //Total de paginas
        public int PageSize { get; set; }       //tamaño de pagina (cantidad de registros a mostrar x pag)
        public int TotalCount { get; set; }     //total de registros que hay

        public bool HasPreviousPage => CurrentPage > 1;      //validar si hay pagina anterior 
        public bool HasNextPage => CurrentPage < TotalPages;        //validar si hay pagina siguiente

        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1: (int?) null;      //validar si hay pagina anterior 
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1: (int?) null;        //validar si hay pagina siguiente

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = count;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageNumber);   //redondeo hacia arriba pe: 8.3 = 9 ya redondeado (para evitar gerdida de paginas con pocos registros)

            AddRange(items);
        }

        //creamos clase para la paginacion
        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();     //obtener cant de registros
            var items = source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();   //implementa Paginacion, (es basicamente el omitir parte de los registros)
 //Ej: hay 2pags de 10regs cada uno, Skip((2-1)*10).Take(10) = omite(primeros 10 registros) y toma(10 registros)

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
