//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SStest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseOrders
    {
        public PurchaseOrders()
        {
            this.PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
        }
    
        public int PurchaseOrder_ID { get; set; }
        public int Vendor_ID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string InvoiceNumber { get; set; }
        public int SubTotal { get; set; }
        public int ValueAddTax { get; set; }
        public int ShippingFee { get; set; }
        public int Amount { get; set; }
        public string ShipAddress { get; set; }
        public int PaymentMethod_ID { get; set; }
        public int Status_ID { get; set; }
        public string Comment { get; set; }
    
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual Status Status { get; set; }
        public virtual Vendors Vendors { get; set; }
    }
}
