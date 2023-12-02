using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTestDotNetCore.Shared
{
    public class ServiceResponse<T>
    {
        /// <summary>
        /// Nullable generic type
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Flag
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
