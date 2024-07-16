using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXMauiApp1.Dtos
{
    public class ResultDto
    {
        public class ResultDTO<T>
        {
            [JsonProperty("@odata.context")]
            public string odatacontext { get; set; }
            public T value { get; set; }
        }
    }
}
