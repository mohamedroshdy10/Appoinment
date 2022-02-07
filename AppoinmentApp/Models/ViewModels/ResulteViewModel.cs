using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentApp.Models.ViewModels
{
    public class ResulteViewModel<T> where T :class
    {
        public int StatusCode { get; set; }
        public string message { get; set; }
        public ResulteEnum  ResulteEnum { get; set; }
        public T Data { get; set; }
    }
    public enum ResulteEnum
    {
        Success=1,
        Error=0
    }
}
