using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LyricGeneratorLib
{
    public class LyricGeneratorException : Exception
    {
        public LyricGeneratorException() : base()
        {
        }

        public LyricGeneratorException(string? message) : base(message)
        {
        }

        public LyricGeneratorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LyricGeneratorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
