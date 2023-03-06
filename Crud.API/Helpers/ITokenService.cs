using System.CodeDom.Compiler;

namespace Crud.API.Helpers
{
    public interface ITokenService
    {
        string Generate();
    }
}
