using OpenSourceProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSourceProject.Areas.Provider.Models.ResourceModels
{
    public class AddProductRequest
    {
        public string CategoryId { get; set; }
      

      //  public string ProviderId { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }

        public string PriceBeforeDiscount { get; set; }
        public string PriceAfterDiscount { get; set; }
        public string Qty { get; set; }

        public List<IFormFile>? Image { get; set; }
    }
}
