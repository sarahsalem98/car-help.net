namespace OpenSourceProject.Models.ResourceModels
{
    public class ResultResponse <T> where T : class
    {
        public string Status {get;set;}
        public T Data { get; set; }
    }
}
