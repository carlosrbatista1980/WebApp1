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
        private VideoViewModel vddViewModel { get; set; }
        
        //TESTE

        public enum TD : string
        {
            r = 1,
            e = 2,
            d = 3,
        }

        public ActionResult Index(string id_text_url)
        {
            if (!string.IsNullOrEmpty(id_text_url))
            {
                link = id_text_url;
                GetVideo(link);
                var v = ViewBag;
                ViewData["myView"] = link;
                var videoViewModel = new VideoViewModel()
                {
                    Link = link,
                    VideoVisibilityy = "visible",
                    VideoSource = "srcccc",
                };

                return View(videoViewModel);
            }

            return View();
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
