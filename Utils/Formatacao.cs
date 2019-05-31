using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Formatacao
    {


        public string RetornaCpfCnpjSemPontuacao(string CpfCnpj)
        {
            string retorno = string.Empty;
            try
            {
                retorno = CpfCnpj.Replace(".", "").Replace("/", "").Replace("-", "");

                return retorno;
            }
            catch
            {
                retorno = "Houve Erro.";
                return retorno;
            }


        }



        public string FormataCnpjCpf(string CnpjCpf)
        {
            string retorno = string.Empty;
            if (CnpjCpf.Length == 11)
            {
                try
                {
                    return retorno = "CPF: " + @String.Format(@"{0:000\.000\.000\-00}", Convert.ToInt64(CnpjCpf));
                }
                catch
                {

                    return "Não Foi Possivel Formatar o Valor Passado para CPF. ";
                }

            }
            if (CnpjCpf.Length == 14)
            {
                try
                {
                    return retorno = "CNPJ: " + @String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(CnpjCpf));
                }
                catch
                {

                    return "Não Foi Possivel Formatar o Valor Passado para CNPJ. ";
                }

            }
            else
            {
                return "Não Foi Possivel Identificar o Valor Passado Para Formatação. ";
            }


            return retorno;
        }
        public string FormataTelefone(string Telefone, string Prefixo)
        {
            string retorno = string.Empty;
            try
            {

                return retorno = @String.Format(@"{0:\(00\) 0000-0000}", Convert.ToInt64(Prefixo + Telefone.Trim()));

            }
            catch
            {

                return retorno = "Telefone Invalido ";
            }

            return retorno;
        }
        public string FormataTelefone(string Telefone)
        {
            string retorno = string.Empty;
            try
            {
                if (Telefone.Trim().Length == 10)
                {

                    return retorno = @String.Format(@"{0:\(00\) 0000-0000}", Convert.ToInt64(Telefone.Trim()));
                }
                if (Telefone.Length == 11)
                {

                    return retorno = @String.Format(@"{0:\(00\) 00000-0000}", Convert.ToInt64(Telefone.Trim()));
                }
                if (Telefone.Trim().Length < 11)
                {
                    return retorno = "Telefone Invalido ";
                }
                if (Telefone.Trim().Length < 10)
                {
                    return retorno = "Telefone Invalido ";
                }
            }
            catch
            {

                return retorno = "Telefone Invalido ";
            }

            return retorno;
        }
        public string FormataCep(string Cep)
        {
            string retorno = string.Empty;
            if (Cep.Length == 8)
            {

                retorno = @String.Format(@"{0:00000\-00}", Convert.ToInt64(Cep));
            }
            else
            {
                retorno = "CEP invalido";
            }

            return retorno;
        }

        public string RetornoVazio(string Retorno)
        {
            if (Retorno == "''")
                return "Vazio";
            if (Retorno == string.Empty)
                return "Vazio";
            if (Retorno == null)
                return "Vazio";
            return "vazio";
        }
        public string FormataInscricaoEstadual(string UF, string InscricaoEstatudal)
        {
            string retorno = string.Empty;
            if (InscricaoEstatudal == null)
            {
                return "ISENTO";
            }
            if (InscricaoEstatudal.ToUpper() == "ISENTO")
            {
                return "ISENTO";
            }
            if (RetornoVazio(InscricaoEstatudal) == "Vazio")
            {
                return "Vazio";
            }
            switch (UF)
            {

                case "RS":

                    if (InscricaoEstatudal.Length != 10)
                    {
                        return "Inscricao Estadual Invalida Para a UF: RS";
                    }
                    return retorno = @String.Format(@"{0:000-0000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "SC":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: SC";
                    }
                    return retorno = @String.Format(@"{0:000.000.000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "PR":
                    if (InscricaoEstatudal.Length != 10)
                    {
                        return "Inscricao Estadual Invalida Para a UF: PR";
                    }
                    return retorno = @String.Format(@"{0:00000000-00}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "SP":
                    if (InscricaoEstatudal.Length != 12)
                    {
                        return "Inscricao Estadual Invalida Para a UF: SP";
                    }
                    return retorno = InscricaoEstatudal;
                    break;
                case "MG":
                    if (InscricaoEstatudal.Length != 13)
                    {
                        return "Inscricao Estadual Invalida Para a UF: MG";
                    }
                    return retorno = @String.Format(@"{0:00.000.00000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "RJ":
                    if (InscricaoEstatudal.Length != 8)
                    {
                        return "Inscricao Estadual Invalida Para a UF: RJ";
                    }
                    return retorno = @String.Format(@"{0:00\.000\.00\-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "ES":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: ES";
                    }
                    return retorno = @String.Format(@"{0:00.000.00-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "BA":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: BA";
                    }
                    return retorno = @String.Format(@"{0:00.000.00-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "SE":
                    if (InscricaoEstatudal.Length != 10)
                    {
                        return "Inscricao Estadual Invalida Para a UF: SE";
                    }
                    return retorno = @String.Format(@"{0:000000000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "AL":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: AL";
                    }
                    return retorno = @String.Format(@"{0:000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "PE":
                    if (InscricaoEstatudal.Length != 14)
                    {
                        return "Inscricao Estadual Invalida Para a UF: PE";
                    }
                    return retorno = @String.Format(@"{0:00.0.000.0000000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "PB":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: PB";
                    }
                    return retorno = @String.Format(@"{0:00000000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;

                case "RN":
                    if (InscricaoEstatudal.Length != 10)
                    {
                        return "Inscricao Estadual Invalida Para a UF: RN";
                    }
                    return retorno = @String.Format(@"{0:00.000.000-00}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "PI":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: PI";
                    }
                    return retorno = @String.Format(@"{0:000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "MA":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: MA";
                    }
                    return retorno = @String.Format(@"{0:000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "CE":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: CE";
                    }
                    return retorno = @String.Format(@"{0:00000000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;

                case "GO":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: GO";
                    }
                    return retorno = @String.Format(@"{0:00.000.000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "TO":
                    if (InscricaoEstatudal.Length != 11)
                    {
                        return "Inscricao Estadual Invalida Para a UF: TO";
                    }
                    return retorno = @String.Format(@"{0:00000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "MT":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: MT";
                    }
                    return retorno = @String.Format(@"{0:000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "MS":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: MS";
                    }
                    return retorno = @String.Format(@"{0:000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "DF":
                    if (InscricaoEstatudal.Length != 13)
                    {
                        return "Inscricao Estadual Invalida Para a UF: DF";
                    }
                    return retorno = @String.Format(@"{0:00000000000-00}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "AM":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: AM";
                    }
                    return retorno = @String.Format(@"{0:00.000.000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "AC":
                    if (InscricaoEstatudal.Length != 13)
                    {
                        return "Inscricao Estadual Invalida Para a UF: AC";
                    }
                    return retorno = @String.Format(@"{0:00.000.000\/000-00}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "PA":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: PA";
                    }
                    return retorno = @String.Format(@"{0:00.000000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "RO":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: PO";
                    }
                    return retorno = @String.Format(@"{0:000.00000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "RR":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: RR";
                    }
                    return retorno = @String.Format(@"{0:00000000-0}", Convert.ToInt64(InscricaoEstatudal));
                    break;
                case "AP":
                    if (InscricaoEstatudal.Length != 9)
                    {
                        return "Inscricao Estadual Invalida Para a UF: AP";
                    }
                    return retorno = @String.Format(@"{0:000000000}", Convert.ToInt64(InscricaoEstatudal));
                    break;
            }


            return retorno;
        }
    }
}
