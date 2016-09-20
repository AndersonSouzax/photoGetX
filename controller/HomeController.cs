using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace getPhotos.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult photogetX(){

            return View();
        }

        [HttpPost]
        public ActionResult photogetX(String pesq) {
            String x = "https://www.google.com/search?hl=pt-BR&site=imghp&tbm=isch&source=hp&biw=1366&bih=667&q=" + pesq;
            WebRequest req = WebRequest.Create(x);
            using (WebResponse resp = req.GetResponse()) {
                using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8)) {

                    var source = reader.ReadToEnd().ToString();
                    //Regular expression para filtrar do html as tags de imagens//
                    String pattern = @"\<img\sheight[(_)(*)(!)(@)(#)($)(%)(&)(?)-//'':.=""0-9a-zA-Z\s]+";
                    
                    int i = 0;
                    //Encontrando as tags das imagens//
                    var mats = Regex.Matches(source, pattern);

                    String[] results = new String[mats.Count];//array de objetos

                    foreach (var img in mats){
                        String url;
                        //Retirando a url da imagem//
                        url = img.ToString().Substring(img.ToString().IndexOf("http"), img.ToString().IndexOf("width") - img.ToString().IndexOf("http") - 2);
                        results[i] = url;
                        i++;
                    }

                    ViewData["pics"] = results;
                }
            }
            return View("Resultado");
        }
    }
}
