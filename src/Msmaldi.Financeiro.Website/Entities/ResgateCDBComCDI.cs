using System;
using System.ComponentModel.DataAnnotations;

namespace Msmaldi.Financeiro.Website.Entities
{
    public class ResgateCDBComCDI
    {
        public int CDBComCDIId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
    }
}