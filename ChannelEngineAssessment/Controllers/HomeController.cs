using ChannelEngineAssessment.Models;
using ChannelEngineAssessment.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChannelEngineAssessment.Controllers;

public class HomeController : Controller
{
    private readonly IChannelEngineClient channelEngineClient;

    public HomeController(IChannelEngineClient channelEngineClient) =>
        this.channelEngineClient = channelEngineClient;

    public IActionResult Index() => View(
        channelEngineClient.GetTopFiveBestSellingProductsAsync());
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? 
            HttpContext.TraceIdentifier });
    }
}