using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExa.Utilidades
{
    public class ModelResponse
    {
        public int Code { get; set; }
        public string Comments { get; set; }
        public object Response { get; set; }
    }
}
