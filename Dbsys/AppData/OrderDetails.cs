
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Dbsys.AppData
{

using System;
    using System.Collections.Generic;
    
public partial class OrderDetails
{

    public int OrderNumber { get; set; }

    public string userName { get; set; }

    public string CustomerName { get; set; }

    public string ContactNumber { get; set; }

    public string Address { get; set; }

    public decimal TotalBill { get; set; }

    public decimal CashReceived { get; set; }

    public string Discount { get; set; }

    public decimal Change { get; set; }

    public string DiningOption { get; set; }

    public System.DateTime OrderDate { get; set; }

    public int userId { get; set; }



    public virtual UserAccount UserAccount { get; set; }

}

}