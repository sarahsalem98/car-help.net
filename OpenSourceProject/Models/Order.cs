using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Models
{

    public enum orderType
    {
        Public,
        Private,
        Product
    }

    public enum orderStatus
    {
        New,
        InProcess,
        FinishedPrepration,
        Delivered,
        Complete,
        Cancelled

    }
    public class Order:TimeStamp
    {
        public int Id { get; set; }

        public string ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public string ? ProviderId { get; set; }
        [ForeignKey("ProviderId")]
        public Provider Provider { get; set; }



        public string? CarId { get; set; }
        [ForeignKey("CarId")]
        public Car Car{ get; set; }

        public int? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public UserAddress UserAddress { get; set; }

        [DefaultValue(orderStatus.New)]
        public orderStatus Status { get; set; } 

        public orderType Type { get; set; }

        public string? Description { get;set; }
        public string? Image { get; set; }

        public string? PaymentMethod { get; set; }    



    }
}
