namespace Encoder.Client.Interfaces
{
    internal interface IMessageDatabaseFacade
    {
        bool InsertMessage(string id, string encryptedValue);
    }
}
