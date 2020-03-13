using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.Models.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Text { get; set; } //текст запроса
        public bool StatusAfter { get; set; } //выполнено - true || отклонено - false
        public bool StatusBefore { get; set; } //на рассмотрении - false || рассмотрено - true
    }
}
