/*
 * CSE 212 Lesson 6C 
 * 
 * This code will analyze the NBA basketball data and create a table showing
 * the players with the top 10 career points.
 * 
 * Note about columns:
 * - Player ID is in column 0
 * - Points is in column 8
 * 
 * Each row represents the player's stats for a single season with a single team.
 */

using Microsoft.VisualBasic.FileIO;

public class Basketball
{
    public static void Run()
    {
        var players = new Dictionary<string, int>();

        using var reader = new TextFieldParser("basketball.csv");
        reader.TextFieldType = FieldType.Delimited;
        reader.SetDelimiters(",");
        reader.ReadFields(); // ignore header row
        while (!reader.EndOfData)
        {
            var fields = reader.ReadFields()!;
            var playerId = fields[0];
            var points = int.Parse(fields[8]);
            //if player is not in the dictionary by their id
            if (!players.ContainsKey(playerId))
            {
                //then add that player
                players.Add(playerId, points);
            }
        }
        Console.WriteLine($"Players: {{{string.Join(", ", players)}}}");
        //convert the dictionary to an array
        //this returns data in this format ["Playername", score]
        KeyValuePair<string, int>[] arrayOfPlayerIdAndPoint = players.ToArray();

        //get top ten players with highest score from the array
        var topPlayers = players
            .OrderByDescending(player => player.Value)  // sort by score
            .Take(10)                                   // take only top 10
            .Select(player => new string[] { player.Key, player.Value.ToString() })// get the player name and their score
            .ToArray();                                 // convert to string[]

        //print player names to screen                                           
        foreach (var player in topPlayers)
        {
            Console.WriteLine(player);
        }
    }
}