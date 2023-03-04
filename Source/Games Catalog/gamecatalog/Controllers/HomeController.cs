using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gamecatalog.Models;
using System;
using System.Net;

namespace gamecatalog.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        var model = new Home();

        string hostname = Dns.GetHostName();

        model.hostip = Dns.GetHostByName(hostname).AddressList[0].ToString();

        return View(model);
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
}

