using System.ComponentModel;

namespace CMD.Model.Enumeradores
{
    public partial class Enums
    {
        public enum TipoDeOcorrencia
        {
            Verbal = 1,
            Escrita = 2
        }

        public enum StatusMedida
        {
            [Description("Reprovada")]
            Reprovado = 1,
            [Description("Aprovada")]
            Aprovado = 2,
            [Description("Aguardando Aprovação (Coord / Gerente)")]
            AguardandoAprovacaoCoordenadorGerente = 3,
            [Description("Aguardando Aprovação (RH)")]
            AguardandoAprovacaoRH = 4,
            [Description("Disponível para Impressão")]
            DisponivelParaImpressao = 5,
            [Description("Impressa")]
            Impressa = 6,
            [Description("Bloqueado (Perda do Prazo de Impressão)")]
            BloqueadaPerdaPrazoImpressao = 7,
            [Description("Bloqueado (Perda do Prazo de Aprovação - Gerente)")]
            BloqueadaPerdaPrazoAprovacaoGerente = 8,
            [Description("Bloqueado (Perda do Prazo de Aprovação - RH)")]
            BloqueadaPerdaPrazoAprovacaoRH = 9
        }

        public enum Acao
        {
            Aprovar = 1,
            Reprovar = 2,
            Bloquear = 3
        }

        public enum Perfil
        {
            Administrador = 1,
            Coordenador = 2,
            Gerente = 3,
            RH = 4
        }
    }
}