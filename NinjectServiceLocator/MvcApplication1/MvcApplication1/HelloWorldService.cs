using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1
{
    public class HelloWorldService : IHelloWorldService
    {
        public string Hello(string message)
        {
            return message;
        }
    }
}