# World Cup Scoreboard 

The task requirements are simple and straightforward so it's hard not to overengineer. I tried to keep it simple but have a decent OOP structure. There are plenty of ways to develop solutions to a production grade such as abstracting the storage of matches to keep them in external storage, handling multithreading, and making the code extensible to accommodate different types of matches.

The workflow I had in mind while implementing:
- The match is created by the library user and should be registered in Competition(World Cup)
- user keeps the ID of the match or keeps the instance of Match object to update the score
- The Scoreboard class represents the summary of matches and encapsulates the "display" logic

```
    var argFra = new Match(new Team("Argentina"), new Team("France"));
    scoreboard.RegisterMatch(argFra);

    var braCro = new Match(new Team("Brasil"), new Team("Croatia"));
    scoreboard.RegisterMatch(braCro); 

    argFra.UpdateScore(new Score(1, 1));

    argFra.Start();
    braCro.Start();

    argFra.SetScore(3, 3);
    braCro.SetScore(2, 0);

    summary = scoreboard.GetScoreboard();

    argFra.Finish();  
    braCro.Finish();    
```


## How to run tests

You will need [net SDK 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

Run `dotnet test`

