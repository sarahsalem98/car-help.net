namespace OpenSourceProject.Models
{
    public class Provider:ApplicationUser
    {
      public string? WorkShopPhotoUrl { get; set; }  
      public string ?WorkShopName { get; set; }
      public string? EngineerName { get; set; }
      public string ?RegisterationFile { get; set; }
      public string ?NextStep { get; set; }
      public int ?Rate { get; set; }

      public IList<ProviderService> ProviderServices { get; set; }
      public IList<ProviderBrand> ProviderBrands { get; set; }

     
     public IList<ProviderWorkHour> ProviderWorkHours { get; set; } 
     
     public IList<Product>ProviderProducts { get; set; }   




      

        
        
    }
}
