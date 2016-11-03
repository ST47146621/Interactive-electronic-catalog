using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace test.Extend
{
    public class MyException : Exception, ISerializable
    {
        public MyException()
            : base("系統發生異常錯誤。") { }
        public MyException(string message)
            : base(message) { }
        public MyException(string message, Exception inner)
            : base(message, inner) { }
        protected MyException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        protected MyException(String message, Exception inner, UInt32 ErrorId)
            : base(message, inner) { }
    }
}