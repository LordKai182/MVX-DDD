using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class retornos
    {
        #region RETORNO DE IMPRESSORAS
        public List<string> RetornaImpressoras()
        {
            List<string> Impressoraslst = new List<string>();
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Impressoraslst.Add(PrinterSettings.InstalledPrinters[i]);
            }

            return Impressoraslst;
        }

        #endregion


        public void RetornaPrestacao(int MesParcela, ref DateTime Prestacao, ref DateTime Vencimento)
        {

            if(MesParcela  == 1)
            {
                Prestacao = DateTime.Now.AddMonths(1);
                Vencimento = DateTime.Now.AddMonths(1);
            }
            if (MesParcela == 2)
            {
                Prestacao = DateTime.Now;
                Vencimento = DateTime.Now.AddMonths(1);
            }
            if (MesParcela == 3)
            {
                Prestacao = DateTime.Now;
                Vencimento = DateTime.Now;
            }
            if (MesParcela == 4)
            {
                Prestacao = DateTime.Now.AddMonths(-1);
                Vencimento = DateTime.Now;
            }
            if (MesParcela == 5)
            {
                Prestacao = DateTime.Now.AddMonths(-1);
                Vencimento = DateTime.Now.AddMonths(-1);
            }
            if (MesParcela == 6)
            {
                Prestacao = DateTime.Now.AddMonths(1);
                Vencimento = DateTime.Now.AddMonths(2);
            }
        }

        public DateTime RetornaVencimento(int MesParcela)
        {

            if (MesParcela == 1)
            {
               
               return  DateTime.Now.AddMonths(1);
            }
            if (MesParcela == 2)
            {
                
                return DateTime.Now.AddMonths(1);
            }
            if (MesParcela == 3)
            {

                return DateTime.Now;
            }
            if (MesParcela == 4)
            {

                return DateTime.Now;
            }
            if (MesParcela == 5)
            {

                return DateTime.Now.AddMonths(-1);
            }
            if (MesParcela == 6)
            {

                return DateTime.Now.AddMonths(2);
            }
            return DateTime.Now;
        }

        public DateTime RetornaPrestacao(int MesParcela)
        {

            if (MesParcela == 1)
            {
                return  DateTime.Now.AddMonths(1);
               
            }
            if (MesParcela == 2)
            {
                return DateTime.Now;
               
            }
            if (MesParcela == 3)
            {
                return DateTime.Now;
               
            }
            if (MesParcela == 4)
            {
                return DateTime.Now.AddMonths(-1);
               
            }
            if (MesParcela == 5)
            {
                return DateTime.Now.AddMonths(-1);
               
            }
            if (MesParcela == 6)
            {
                return DateTime.Now.AddMonths(1);
               
            }

            return DateTime.Now;
        }
    }
}
