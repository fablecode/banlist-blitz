<div align="center">
  <img src="assets/logo/logo_128x128.png" alt="Banlist Blitz Logo">
</div>

[![Build Status](https://dev.azure.com/fablecode/Yugioh/_apis/build/status%2Ffablecode.banlist-blitz?branchName=main)](https://dev.azure.com/fablecode/Yugioh/_build/latest?definitionId=22&branchName=main)
[![NuGet](https://img.shields.io/nuget/v/BanlistBlitz.svg)](https://www.nuget.org/packages/BanlistBlitz/)
# Welcome to Banlist Blitz! 🚀
Banlist Blitz is your ultimate companion for staying on top of the ever-changing Yu-Gi-Oh! banlists. Whether you're a duelist, a developer, or just a fan of the game, this tool is designed to make your Yu-Gi-Oh! experience more exciting and accessible.

## ✨ Key Features

- 🔄 **Real-Time Updates:** Get lightning-fast, real-time updates on banlist changes the moment Konami makes them.

- 🧩 **API Magic:** Access banlist information through a magical library that's as easy to use as a Spell Card.

- 🕰️ **Time Travel:** Dive into the past with historical banlist data and see how the metagame has evolved over time.

- 📚 **Docs for Wizards:** Our documentation is so user-friendly, even Dark Magician can understand it.

- 👥 **Community Love:** Join our community of passionate duelists and developers to help make Banlist Blitz even better.

- 🔍 **Data Integrity:** We solemnly swear to uphold data accuracy, ensuring you have reliable banlist info at your fingertips.

## 🚀 Getting Started

To unleash the power of Banlist Blitz in your .NET projects, simply conjure it from NuGet:

## NuGet

    PM> Install-Package BanlistBlitz

## Quickstart

```csharp

// Initialize BanlistBlitz
IBanlistBlitz banlistblitz = new BanlistBlitz();

// Retrieve banlist by format i.e. Tcg, Ocg
Banlist articles = banlistblitz.LatestBanlist(Format.Tcg);

```

## License
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
