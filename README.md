# Ratr

[![Ratr](./.static/icon.svg)](#)

Router Config Extractor (GUI).  

* Extract **PPP** hidden password to be used in personal router (Home gateway).
* Security: Avoiding default router from your ISP & Possible malwares in custom firmwares.
* Bypass **TR069** management parameters.
* Automated **config file** decoder (No python scripting required!).
* Educational purposes (Analyse parameters).

## Requirements:

* [**.NET Desktop Runtime**](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.23-windows-x64-installer) ^6.x

Used to build Ratr:

* **Visual Studio 2022** ^17.x

## Teseted:

* [Huawei DG8245V-10 ^1.x](https://www.manualslib.com/manual/2439878/Huawei-Dg8245v.html)
* [ZTE ZXHN H267N ^1.x](https://www.manualslib.com/products/Zte-Zxhn-H267n-6917102.html)

## Decoders:

* [Hwdecode](https://github.com/Jakiboy/Hwdecode) (Huawei)
* [Ztedecode](https://github.com/Jakiboy/Ztedecode) ([ZTE config utility](https://github.com/mkst/zte-config-utility))
* [AES encryption](https://pypi.org/project/pycryptodomex/)

## Download:

**Windows only** (No setup)

* Download from: [Releases](https://github.com/Jakiboy/Ratr/releases)
* [Huawei config example](https://github.com/Jakiboy/Ratr/raw/refs/heads/main/.static/huawei.example.xml)
* [ZTE config example](https://github.com/Jakiboy/Ratr/raw/refs/heads/main/.static/zte.example.bin)

## Docs:

* [How to export "Huawei DG8245V-10" router data (config)?](./docs/Huawei-DG8245V-10-Export.md)
* [How to fix "Huawei DG8245V-10" router configuration (DSL)?](./docs/Huawei-DG8245V-10-Config.md)
* [How to export "ZTE ZXHN H267N" router data (config)?](./docs/ZTE-ZXHN-H267N-Export.md)

## Screenshot:

[![screenshot](./.static/screenshot.jpg)](#)

---

> [!CAUTION]
> Decoders have been flagged as a potential threat by your antivirus software. This is a false positive, and the files are safe to use. You can build them from the source.
