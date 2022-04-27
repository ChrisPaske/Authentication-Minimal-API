using System;
using System.Net.Http;
using System.Text;
using NBomber.CSharp;
using NBomber.Plugins.Http;
using NBomber.Plugins.Http.CSharp;
using NUnit.Framework;

namespace Authentication_Minimal_API_Test.Authentication;

public class LoginLoadTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [Category("Load")]
    public void Login_Load_Test_Level_1()
    {
        var step = Step.Create("login",
            HttpClientFactory.Create(),
            context => Http.Send(LoginRequest(), context));

        var scenario = ScenarioBuilder
            .CreateScenario("login_load_test_level_1", step)
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(
                Simulation.InjectPerSec(100, TimeSpan.FromSeconds(30))
            );

        NBomberRunner
            .RegisterScenarios(scenario)
            .AddReportingPlugins()
            .Run();
    }

    [Test]
    public void Login_Load_Test_Level_2()
    {
        var step = Step.Create("login",
            HttpClientFactory.Create(),
            context => Http.Send(LoginRequest(), context));

        var scenario = ScenarioBuilder
            .CreateScenario("login_load_test_level_2", step)
            .WithWarmUpDuration(TimeSpan.FromSeconds(5))
            .WithLoadSimulations(
                Simulation.InjectPerSec(500, TimeSpan.FromSeconds(30))
            );

        NBomberRunner
            .RegisterScenarios(scenario)
            .AddReportingPlugins()
            .Run();
    }

    private static HttpRequest LoginRequest()
    {
        var content = new StringContent("{\"username\": \"loadtest\",\"password\": \"mypassword\"}",
            Encoding.UTF8, "application/json");

        return
            Http.CreateRequest("POST", "https://localhost:7103/login")
                .WithHeader("Accept", "*/*")
                .WithHeader("Cache-Control", "no-cache")
                .WithBody(content);
    }
}