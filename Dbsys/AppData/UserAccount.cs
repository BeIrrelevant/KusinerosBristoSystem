
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
    
public partial class UserAccount
{

    public UserAccount()
    {

        this.Purchase = new HashSet<Purchase>();

        this.OrderDetails = new HashSet<OrderDetails>();

    }


    public int userId { get; set; }

    public string userName { get; set; }

    public string userPassword { get; set; }

    public string userStatus { get; set; }

    public Nullable<int> roleId { get; set; }

    public Nullable<int> createdBy { get; set; }



    public virtual ICollection<Purchase> Purchase { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; }

}

}
