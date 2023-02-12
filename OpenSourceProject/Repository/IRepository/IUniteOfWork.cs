namespace OpenSourceProject.Repository.IRepository
{
    public interface IUniteOfWork
    {
        public ICarRepository car { get; }

        public  void  Save();

    }
}
