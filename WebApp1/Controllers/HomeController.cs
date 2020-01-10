using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class HomeController : Controller
    {
        private string link { get; set;}
        private string vid1 { get; set; }
        private string vid2 { get; set; }
        private string vid3 { get; set; }

        public IActionResult GetVideoApp()
        {
            ViewBag.vidSrc = "";
            ViewBag.vidVisibility = "";

            return View();
        }

        //TESTE
        
        public ActionResult Index()
        {
            var data1 = new List<SelectListItem>
            {
                new SelectListItem{Text="1",Value="A" },
                new SelectListItem{Text="2",Value="B" },
                new SelectListItem{Text="3",Value="C" },
                new SelectListItem{Text="4",Value="D" }
            };
            var data2 = new List<SelectListItem>
            {
                new SelectListItem{Text="a",Value="Aa" },
                new SelectListItem{Text="b",Value="Bb" },
                new SelectListItem{Text="c",Value="Cc" },
                new SelectListItem{Text="d",Value="Dd" }
            };
            ViewBag.drop1 = data1;
            ViewBag.drop2 = data2;
            return View();
        }
        
        public ActionResult Refresh(string id_text_url)    /*Accept the data which is passed by ajax.*/
        {
            if (!string.IsNullOrEmpty(id_text_url))
            {
                link = id_text_url;
                GetVideo(link);
                ViewData["link1"] = link;
                ViewData["vidVisibility"] = "visible";
            }
            
            //Handler the data which is passed from view.

            return Json(ViewData); //Send the data back to view.
        }
        //TESTE FIM

        [HttpPost]
        public ActionResult GetVideoService(string text_url)
        {
            
            //return View();


            string strTest = "https://www.xvideos.com/video52422711/carolina_menina_inocente_do_interior_transou_com_o_namorado_da_sua_mamae";
            
            link = text_url;
            link = strTest;

            if (!string.IsNullOrEmpty(link))
            {
                GetVideo(link);

                var v = ViewBag;

                ViewData["myView"] = link;

                vid1 = text_url;
                vid2 = link;
                vid3 = "visible";

                //ViewBag.vidLink = text_url;
                //ViewBag.vidSource = link;
                //ViewBag.vidVisibility = "visible";

                return View("GetVideoApp");

                //ViewData["myView"]
                //return Redirect(link);
            }

            return Redirect(text_url);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void GetVideo(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(html))
            {
                string pattern = @"(html5player.setVideoUrlHigh([\(][\'])([\S]*)([\'][\)]))";
                Regex reg = new Regex(pattern, RegexOptions.None);
                var str = reg.Match(html);
                if (str.Success)
                {
                    link = str.Value;
                    link = link.Replace("html5player.setVideoUrlHigh", "");
                    link = link.Replace("'", "");
                    link = link.Replace("(", "");
                    link = link.Replace(")", "");
                }
            }

            ViewData["myView"] = link;
        }
    }
}
