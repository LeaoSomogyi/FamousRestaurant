namespace FamousRestaurant.Domain.Contracts
{
    public interface IIsValid
    {
        /// <summary>
        /// Method to validate information received on API
        /// </summary>
        /// <returns>True if is valid, else false</returns>
        bool IsValid();
    }
}
