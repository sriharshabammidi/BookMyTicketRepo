using System;
using System.Collections.Generic;
using System.Text;

namespace BookMyTicket.Entities
{
    public class DBTransactionResult<T>
    {
        public T Obj { get; set; }
        public bool IsTransactionSuccess { get; set; }
    }
}
