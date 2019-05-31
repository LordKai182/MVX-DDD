using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Utils
{
    public static class MinhaNovaLabel
    {


        public static MvcHtmlString LabelPersonalizada(this HtmlHelper helper, string destino, string texto)
        {
            return MvcHtmlString.Create(string.Format("<label for='{0}'>{1}</label>", destino, texto));
        }

        public static MvcHtmlString Tab(this HtmlHelper helper)
        {
            string retorno = string.Empty;

            return MvcHtmlString.Create(retorno);
        }



        public static MvcHtmlString PanelHeader(this HtmlHelper helper, string Titulo)
        {
            string retorno = string.Empty;
            retorno = "<div class='bootstrap-table'>" +
                      "<hr/>" +
                       "<div class='panel panel-primary'>" +
                       "<div class='panel-heading'>" +
                       "<h3 class='panel-title'>" + Titulo + "</h3>" +
                       "</div>" +
                       "<div class='panel-body data-table'>" +
                       "<div class='row'>" +
                       "<div class='col-lg-12'>" +
                       "<div class='panel-body'>";

            return MvcHtmlString.Create(retorno);
        }
        public static MvcHtmlString PanelFooter(this HtmlHelper helper)
        {
            string retorno = string.Empty;
            retorno = "</div>" +
                       "</div>" +
                       "</div >" +
                       "</div>" +
                       "</div>" +
                       "</div >" +
                       "<div class='clearfix'></div>";

            return MvcHtmlString.Create(retorno);
        }
        public static MvcHtmlString FechaPanelColapsado(this HtmlHelper helper)
        {
            string retorno = string.Empty;
            retorno = "</div></div></div>";
            return MvcHtmlString.Create(retorno);
        }

        public static MvcHtmlString AbrePanelColapsado(this HtmlHelper helper, string RenderPage, string Id)
        {
            string retorno = string.Empty;

            retorno =
                     "<div id = " + Id + " class='collapse filter-panel'>" +
                     "<div class='panel panel-default'>" +
                     "<div class='panel-body'>" +
                     "<p></p>";


            return MvcHtmlString.Create(retorno);
        }

        public static MvcHtmlString BotaoColapse(this HtmlHelper helper, string NomeBotao, string IdColapse)
        {
            string retorno = string.Empty;
            retorno = "<button type = 'button' class='btn btn-primary' style='margin-left:15px' data-toggle='collapse' data-target='#" + IdColapse + "'>" +
            "<span class='glyphicon glyphicon-cog'></span>" + NomeBotao + "</button>";

            return MvcHtmlString.Create(retorno);
        }
        public static MvcHtmlString ChamaModal(this HtmlHelper helper)
        {
            string retorno = string.Empty;
            retorno = "<div id = 'myModal' class='modal'>" +
                     "<div class='modal-dialog'>" +
                     "<div class='modal-content'>" +
                     "<div id = 'myModalContent' ></ div ></ div ></ div ></ div >";

            return MvcHtmlString.Create(retorno);
        }
        public static MvcHtmlString Cabecalho(this HtmlHelper helper, string Texto)
        {
            string retorno = string.Empty;
            retorno = "<hr/><h4>" + Texto + "<h4/><hr/>";

            return MvcHtmlString.Create(retorno);
        }
        public static MvcHtmlString Botao(this HtmlHelper helper, string propriedades, string NomeBotao)
        {
            string retorno = string.Empty;
            retorno = "<button type = 'button' " + propriedades + ">" +
            "<span class='glyphicon glyphicon-search'></span>" + NomeBotao + "</button>";

            return MvcHtmlString.Create(retorno);
        }


    }
}
