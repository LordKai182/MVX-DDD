using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class ZIP
    {
        public static void Comprimir(string filePath, string zipName)
        {
            ZipFile zip;

            try
            {
                using (zip = new ZipFile())
                {
                    zip.AddFile(filePath, string.Empty);
                    zip.Save(zipName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                zip = null;
            }
        }

        public static void AddListCompress(List<string> filesPath, string zipName)
        {
            ZipFile zip;

            try
            {
                using (zip = new ZipFile())
                {
                    foreach (string item in filesPath)
                    {
                        zip.AddFile(item, string.Empty);
                    }
                    zip.Save(zipName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                zip = null;
            }
        }

        public static void Extrair(string zipFile, string path)
        {
            ZipFile zip;

            try
            {
                using (zip = new ZipFile(zipFile))
                {
                    zip.ExtractAll(path, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                zip = null;
            }
        }
    }
}
