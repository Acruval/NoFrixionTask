# NoFrixionTest

Test for NoFrixion Antonio de la Cruz

https://github.com/Acruval/NoFrixionTask

## Index
- [Task to do](#Task-to-do)
- [Notes](#Notes)
- [Architecture](#Architecture)
- [Possible Improvements](#Possible-Improvements)
- [Other technical notes](#Other-technical-notes)
- [What was the default Console App](#What-was-the-default-Console-App)
- [No Nullable Types but Contracts](#No-Nullable-Types-but-Contracts)

## Task to do


It would help us with your application process if you could complete a small programming task. The task is below.

We're not expecting you to spend a huge amount of time on it. If you do find you're stuck after an hour or so, just send in what you have. We use the test as a rough guide and not as a pass/fail.

Write a console application in C# to get the latest Bitcoin price from https://www.coindesk.com/coindesk-api.

- The link to get the price in JSON format is: https://api.coindesk.com/v1/bpi/currentprice.json

- Once the JSON has been retrieved the console application should display the latest BTC price in EUR, e.g: *BTC Price EUR 39,589.5087.*

-If you have a GitHub/GitLab or similar account please create a repository for the example (can be private if you wish) and email me the details. If not, you can zip your sample up and email it to me.


## Notes
 
 - This project is done in NET core 6.0 so you will need VS2022 to open
 - Install [Mark Editor2](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor2). This can be very useful to read MD doc from VS
 (like this document)


## Architecture
 - There is appsettignsfile
 - The is a Dependency injector. (It is overkill for this example, but the purpose is showing a real App)
 - There is a file log done with NLog. 
 - Services
   - ***CoinDeskService***. Bring data from DeskCoin.
   - ***JsonSerialize***. Deserialize Json. Use stream instead.
 - ***BusinessLayer**. At the moment it is doing just a small transformation of data. It is good to have your own Ticker class, Candle Class, Time Interval Enum, to isolate types of your exchange provider.


## Possible Improvements
  - **CoinDeskService**. 
      - [ ] We are not returning a collection. But if that would be the case, use [IEnumerableTask](https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/november/csharp-iterating-with-async-enumerables-in-csharp-8)
      - [ ] It could be nice to have a socket service in order to get price change notifications instead has to call it for updating
      - [ ] Is is not necessary here but manage Authentication/Authorization for example by ***Bearing Token***
  - **JsonSerialize**.
    - [ ] Investigate even better serialization/Deserialization for .Net. I guest This can be more async
  - ***BusinesLayer***. 
    - [ ] At the moment we have a console App that starts and finishes. but could be more complex business logic or as an example use the MaxAge to keep a cache, and avoid deserializing despite the HTTP cache
  - [ ] Unit test. 
  - [ ] [BenchMark Test](https://www.infoworld.com/article/3573782/how-to-benchmark-csharp-code-using-benchmarkdotnet.html). We need to check preformance in **High frecuency trading**

 ## Other technical notes
 _ Nullable Types Diable. Own library used. See [No Nullable Types but Contracts].
 - Using HttpFactory with named factories. https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/
 - All classes are sealed by default to avoid checking MSIL pointer for virtual calls. More detail https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-6/#peanut-butter
 - DeserializeJson using stream for better performance.
 - Use Inlining (https://www.dotnetperls.com/aggressiveinlining) to about MSIL jumps to small unique called methods.


## What was the default Console App
- Mind this is the new console style.
```csharp
  // See https://aka.ms/new-console-template for more information
  Console.WriteLine("Hello, World!");
```

## No Nullable Types but Contracts
There is a new feature in NET6.0 enable by default. 
```xml
    <Nullable>disable</Nullable>
```
that it is useful to avoid calling null references. [The million-dollar mistake](https://www.infoq.com/presentations/Null-References-The-Billion-Dollar-Mistake-Tony-Hoare/)

so  if we have 
```csharp
   string str; //Error must be initialized.
```
this must be initialized unless we have
```csharp
   string? str;
```

At the moment I am using for it  My own Contract library in this way.

```csharp
  _httpClientFactory = httpClientFactory;
  CoreContracts.Postcondition(_httpClientFactory != null);
```

This this a redux implementation equivalent the deprecated [CodeContracts](https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts)
(there is no invariants, for example)
but in my case, because a popup in a WebApi app has no too much sense, it only stops the debugger in the ContractAssertion Fail when you are in DEBUG mode.
No check and no performance penalty in RELEASE mode.


***Still it is more powerfull than Nullable Enabed****, because you can use thins like
 ```csharp
   CoreContracts.Preconditocondition(sqrtInput > 0); 
```


