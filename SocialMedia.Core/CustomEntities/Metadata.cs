namespace SocialMedia.Core.CustomEntities
{
    public class Metadata
    {
        public int TotalCount { get; set; }
        public int PageSize { get; set; }       //tamaño de pagina (cantidad de registros a mostrar x pag)
        public int CurrentPage { get; set; }    //Pagina actual 
        public int TotalPages { get; set; }     //Total de paginas
        public bool HasNextPages { get; set; }
        public bool HasPreviousPages { get; set; }

        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }

    }
}
