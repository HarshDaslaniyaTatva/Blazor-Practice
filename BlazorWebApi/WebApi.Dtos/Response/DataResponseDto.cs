using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Dtos.Response
{
    public class DataResponseDto
    {
        public string Type { get; set; } = string.Empty;
        public string Msg { get; set; } = string.Empty;
        public object? obj {get; set;} = null;
    }
}
