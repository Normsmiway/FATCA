namespace Fatca.Core
{
    public interface IFatcaProcessor : ISign, IEncrypt
    {
       void Apply();
    }

    public interface IValidate
    {
        ISign Validate();
    }
    public interface ISign
    {
        IEncrypt Sign(bool loadFromFolder = false,string path="");
    }
    public interface IEncrypt
    {
        FatcaProcessor Excypt();
    }
}
