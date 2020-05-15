using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo.API
{
    public class ItemsPaginadosViewModel<T> where T : class
    {
        public int IndicePagina { get; private set; }
        public int TamanoPagina { get; private set; }
        public long TotalItems { get; private set; }
        public IEnumerable<T> Data { get; private set; }

        public ItemsPaginadosViewModel(int indicePagina, int tamanoPagina, long totalItems, IEnumerable<T> data)
        {
            this.IndicePagina = indicePagina;
            this.TamanoPagina = tamanoPagina;
            this.TotalItems = totalItems;
            this.Data = data;
        }       

        
    }
}
