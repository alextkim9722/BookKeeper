namespace BackEnd.Model.Interfaces
{
    public interface IDoublePKModel
    {
        public int firstKey { get; set; }
        public int secondKey { get; set; }
    }
}
