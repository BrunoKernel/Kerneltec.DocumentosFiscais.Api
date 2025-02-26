namespace Kerneltec.DocumentosFiscais.Infra.Integrations.Acbr.Configuration
{
   public class ACBrIni
    {
        public bool UseMemory { get; set; } = true;
        public required string ConfigName { get; set; }
        public string? Senha { get; set; }
        public int PoolSize { get; set; }
    }
}
