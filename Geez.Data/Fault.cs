//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Geez.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fault
    {
        public long Id { get; set; }
        public string TransactionId { get; set; }
        public System.DateTime TransactionTime { get; set; }
        public string FaultCode { get; set; }
        public string FaultString { get; set; }
    }
}