using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDExa.Utilidades
{
    public class JsonUtils
    {
        /// <summary>
        /// Método que serializa un objeto a una cadena JSON utilizando camelCase para los nombres de las propiedades.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObjectToLowerCamelCase(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = true,
                        ProcessDictionaryKeys = true
                    }
                },

                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
