// See https://aka.ms/new-console-template for more information
using FakeGetTokenAsync;

IdentityTokenClaimService tokenFake = new();

Console.WriteLine("User:");
Console.WriteLine(await tokenFake.FakeGetTokenAsync("user", "no-registered"));

Console.WriteLine("");

Console.WriteLine("Admin:");
Console.WriteLine(await tokenFake.FakeGetTokenAsync("admin", "registered"));

Console.ReadKey();