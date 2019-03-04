using stackexchangeTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace stackexchangeTask.Controllers
{
    public class questionsController : Controller
    {
        // GET: questions
        public ActionResult Index()
        {

            RootObject questions = null;

            HttpClientHandler handler = new HttpClientHandler();
            handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using (var Client = new HttpClient(handler))
            {
                Client.BaseAddress = new Uri("https://api.stackexchange.com/");
                //HTTP GET
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/jason"));
                var response = Client.GetAsync("2.2/questions?page=1&pagesize=50&order=desc&sort=creation&site=stackoverflow");
                response.Wait();

                var readTask = (response.Result).Content.ReadAsAsync<RootObject>();
                readTask.Wait();
                questions = readTask.Result;
                ViewBag.result = questions;



            }

            return View(questions);


        
        }
    }
}