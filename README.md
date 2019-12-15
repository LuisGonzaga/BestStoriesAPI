# BestStoriesAPI
API project build for SGT

To test/validate this project:
  1 - Make a local copy of this repository.
  2 - Open it with Visual Studio 2019 
  3 - execute it with IIS Express

This project was built with Visual Studio 2019 using C# and .Net Core 2.2

The application first validates if there is already a Top twenty stories list (ordered by descending score) stored in cache. 
If it doesn't exist it will use hacker-news api to retrive a list of best stories id's, order them and store the first 20 records with descending order in Cache. The stored records will be cleaned every 24 hours.
If the list was already stored in Cache it will return it, also being much faster.


Additional references and tools:
  http://json2csharp.com/ => To build a c# class from a JSON file.   
  https://www.codementor.io/@andrewbuchan/how-to-parse-json-into-a-c-object-4ui1o0bx8 => To Parse Json into C# Object
  https://michaelscodingspot.com/cache-implementations-in-csharp-net/ => Implementing Cache


The biggest difficulty was probably the slowness of hacker-news responses when retrieving the details of each of the 200 stories id's previously returned. This operation resembled like something wasn't running as expected - but it was only being slow. 

I've took 11 hours for the all project (including repository and these notes), but as you can see on my resume my previous project was building an API .Net Core connecting LuzSaúde counters and dispensers (using OutSystems and WPF respectively) with NewVision services provided using Websockets involving the use of several different services (like GetSettings, DispenserGetTicket or CounterCallEndAndGetNextTicket)

Future expansions for this API would have to take into account conditions and requirements not present in this project.
