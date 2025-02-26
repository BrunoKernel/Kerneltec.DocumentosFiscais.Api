using ACBrLib.NFe;
using Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr.Configuration;
using Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr
{
    public class ACBrNFePool : IACBrNFePool
    {
        private readonly ConcurrentBag<ACBrNFe> _pool = new();
        private readonly ACBrIni _settings;        
        private int _instanceCount = 0;

        public ACBrNFePool(IOptions<ACBrIni> settings)
        {
            _settings = settings.Value;

            string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _settings.DllPath);
            if (!File.Exists(dllPath))
                throw new FileNotFoundException($"A DLL do ACBr não foi encontrada: {dllPath}");
        }   


        public ACBrNFe Rent()
        {
            if (_pool.TryTake(out ACBrNFe instance))
                return instance;

            if (_instanceCount < _settings.PoolSize)
            {
                _instanceCount++;
                var acbrNFe = new ACBrNFe("[Memory]", _settings.Senha);
                ConfigurarACBrNFe(acbrNFe);
                return acbrNFe;
            }

            throw new InvalidOperationException("Limite máximo de instâncias da ACBrNFe atingido.");
        }

        public void Return(ACBrNFe instance)
        {
            _pool.Add(instance);
        }


        private void ConfigurarACBrNFe(ACBrNFe acbrNFe)
        {
           acbrNFe.Config.
        }
    }
}
