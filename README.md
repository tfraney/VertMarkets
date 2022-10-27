# VertMarkets

Title : VertMarkets Magazine Store Subscriber Task
Author: Thomas Franey 10/26/2022 
Technology: .NET 6.0, c# 8.0 

Requirements for Project solution:
 1) VS 2022 (enterprise or Business) - updated 
 2) .NET 6.0 framework SDK 
 3) turn off console automatic break in Tools => options => debugging

Requirements to run EXE in BIN
1) install .net 6.0 SDK into PC 
2) Windows 10 
3) preferred - Administrative elevatin rights 

Summary: To list all customers that have a subscription for any Magazine in all categories based on Token.

Design: abstracted in 3 tier service console project (Data/Business/Main app). 
Notes: Business Layer uses internal functions to hide API calls to VertMarket from other projects.
       Newtonsoft json used for entity serializations Tasks for employed to further parallel calls during the loading of data, after token is called. 
       Average time: 3.5 to 5.5 seconds every loop. 

To Run Project:
 1) Unzip into source folder as (VertMarkets\MagazineSToreApp) 
 2) Open VertMarkets.sln
 3) Go to Nuget Manager to check for NUGET restore
 4) As debug/AnyCPU: Set MagazineStoreApp.Main project as starting project. 
 5) Compile and Build 
 6) Run app => Console app will display title and run first time, and ask to repeat by key press unless 'X' is hit. 
 7) output => status, time, or any errors caught.

 to Run EXE: n/a
 
