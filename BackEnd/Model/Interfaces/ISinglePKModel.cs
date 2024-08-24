namespace BackEnd.Model.Interfaces
{
    // Purpose is to help with generics to grab IDs
    // I don't want to use reflection in this case
    public interface ISinglePKModel
    {
        public int pKey { get; set; }
    }
}
