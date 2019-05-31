using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Repositorio
{
    public class Formulario<T> where T : class
    {
        public T LerFormulario(System.Web.Mvc.FormCollection form, T entity)
        {
            var s = entity;
            foreach (var p in s.GetType().GetProperties().Where(p => p.GetGetMethod().GetParameters().Count() == 0))
            {
                foreach (var key in form.Keys)
                {
                    try
                    {

                        if (p.Name == key.ToString())
                        {
                            if (p.Name == "ContratoNrcId")
                            {
                                p.SetValue(s, Convert.ToInt32(form[key.ToString()]));
                            }
                            if (p.Name == "TipoClienteFaturamentoId")
                            {
                                p.SetValue(s, Convert.ToInt32(form[key.ToString()]));
                            }
                            if (p.Name == "ContratoId")
                            {
                                p.SetValue(s, Convert.ToInt32(form[key.ToString()]));
                            }

                            if (p.PropertyType.Name == "Int32")
                            {
                                p.SetValue(s, Convert.ToInt32(form[key.ToString()]));
                            }
                            if (p.PropertyType.Name == "String")
                            {
                                p.SetValue(s, form[key.ToString()]);
                            }
                            if (p.PropertyType.Name == "DateTime")
                            {
                                p.SetValue(s, Convert.ToDateTime(form[key.ToString()]));
                            }
                            if (p.PropertyType.Name == "Bool")
                            {
                                p.SetValue(s, Convert.ToBoolean(form[key.ToString()]));
                            }
                            if (p.PropertyType.Name == "Decimal")
                            {
                                p.SetValue(s, Convert.ToDecimal(form[key.ToString()]));
                            }
                            if (p.PropertyType.Name == "Double")
                            {
                                p.SetValue(s, Convert.ToDouble(form[key.ToString()]));
                            }
                        }
                    }
                    catch
                    {

                    }


                }
            }

            return s;
        }
    }
}
