
using ACBrLib.NFe;

namespace Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr.Interfaces
{
  public  interface IACBrNFePool
    {
        ACBrNFe Rent();
        void Return(ACBrNFe instance);
    }
}