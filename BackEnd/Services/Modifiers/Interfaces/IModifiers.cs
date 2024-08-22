using BackEnd.Services.ErrorHandling;

namespace BackEnd.Services.Modifiers.Interfaces
{
    public interface IModifiers<T> where T : class
    {
        public Results<IEnumerable<T>> ModifyModels(IEnumerable<T> model);
    }
}
