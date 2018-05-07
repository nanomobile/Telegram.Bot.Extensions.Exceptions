# .NET Client for Telegram Bot API

[![Telegram Chat](https://img.shields.io/badge/Chat-Telegram-blue.svg)](https://t.me/tgbots_dotnet)
[![license](https://img.shields.io/github/license/TelegramBots/Telegram.Bot.Extensions.Exceptions.svg?maxAge=2592000)](https://raw.githubusercontent.com/TelegramBots/Telegram.Bot.Extensions.Exceptions/master/LICENSE.txt)

|Package|Branch|Build|Test|
|:-----:|:----:|:---:|:--:|
| [NuGet ![NuGet Release](https://img.shields.io/nuget/vpre/Telegram.Bot.Extensions.Exceptions.svg?label=Telegram.Bot.Extensions.Exceptions&maxAge=3600)](https://www.nuget.org/packages/Telegram.Bot.Extensions.Exceptions/) | `master` | [![Build status](https://ci.appveyor.com/api/projects/status/x0vwuxdhe644sys0/branch/master?svg=true)](https://ci.appveyor.com/project/MrRoundRobin/telegram-bot/branch/master) | [![Test Status](https://img.shields.io/travis/TelegramBots/Telegram.Bot.Extensions.Exceptions/master.svg?maxAge=3600&label=Test)](https://travis-ci.org/TelegramBots/Telegram.Bot.Extensions.Exceptions) |
| [MyGet ![MyGet](https://img.shields.io/myget/telegram-bot/v/Telegram.Bot.Extensions.Exceptions.svg?label=Telegram.Bot.Extensions.Exceptions&maxAge=3600)](https://www.myget.org/feed/telegram-bot/package/nuget/Telegram.Bot.Extensions.Exceptions) | `develop` | [![Build status](https://ci.appveyor.com/api/projects/status/x0vwuxdhe644sys0/branch/develop?svg=true)](https://ci.appveyor.com/project/MrRoundRobin/telegram-bot/branch/develop) | [![Test Status](https://img.shields.io/travis/TelegramBots/Telegram.Bot.Extensions.Exceptions/develop.svg?maxAge=3600&label=Test)](https://travis-ci.org/TelegramBots/Telegram.Bot.Extensions.Exceptions) |

Exceptions extension class for [.NET Client for Telegram Bot API](https://github.com/TelegramBots/Telegram.Bot).

Join our **super group on Telegram**: [`@tgbots_dotnet`](https://t.me/tgbots_dotnet)

> If you need the latest features(tested but unstable), use [MyGet feed](https://www.myget.org/feed/telegram-bot/package/nuget/Telegram.Bot.Extensions.Exceptions) (auto deployed from `develop` branch) until we update the NuGet package with stable changes.

## Getting Started

First, talk to [BotFather](https://t.me/botfather) on Telegram to get an API token. Place your token in method below and call the method.

```c#
static async Task TestApiAsync()
{
    var botClient = new Telegram.Bot.TelegramBotClient("your API access Token");
    var me = await botClient.GetMeAsync();
    System.Console.WriteLine($"Hello! My name is {me.FirstName}");
}
```

## Learning More

If you don't know how to use this project or what is available for a Telegram bot, check the self-documented [Systems Integration tests](./test/Exceptions.Tests.Integ/) which are runnable examples of API methods.

Before submitting issues please consult following resources:

* [Changelog](https://github.com/TelegramBots/Telegram.Bot.Extensions.Exceptions/blob/master/CHANGELOG.md)
* [API docs](https://core.telegram.org/bots/api)
* [Tests cases](./test/Exceptions.Tests.Integ/)

## Installation

Install as [NuGet package](https://www.nuget.org/packages/Telegram.Bot.Extensions.Exceptions/):

Package manager:

```powershell
Install-Package Telegram.Bot.Extensions.Exceptions
```

.NET CLI:

```bash
dotnet add package Telegram.Bot.Extensions.Exceptions
```

For testing you can use the [MyGet feed](https://www.myget.org/gallery/telegram-bot) with automated builds.
