using System;
using System.Collections.Generic;
using System.Linq;
using ScrabbleScoreAPI.Models;

public static class ScrabbleScore
{
    public static int Score(string input, ScoringValueSet scoringValueSet = null)
    {
        Dictionary<char, int> values = null;
        Dictionary<int, string> valuesInitial;
        
        if (scoringValueSet == null)
        {
            valuesInitial = new Dictionary<int, string>()
            {
                {1, "aeioulnrst"},
                {2, "dg"},
                {3, "bcmp"},
                {4, "fhvwy"},
                {5, "k"},
                {8, "jx"},
                {10, "qz"}
            };
        } else {
            valuesInitial = new Dictionary<int, string>();
            
            string[] valueSetArr = scoringValueSet.ScoringValuesSet.ToLower().Split(",");

            foreach (string item in valueSetArr)
            {
                string[] parts = item.Split(":");
                valuesInitial.Add(Int32.Parse(parts[0]), parts[1]); 
            }
        };

        values = valuesInitial.SelectMany(kv => kv.Value.Select(c => (c, kv.Key)))
                .ToDictionary(kv => kv.c, kv => kv.Key);

        string word = input.ToLower();
        int score = 0;
        int ind = 0;
        List<int> scores = new List<int>{1, 3, 8, 14, 20, 34, 50, 120};
        foreach (char letter in word)
        {
            ind +=1;
            // score += values[letter];
            score += scores[ind];

        }
        return score;
    }
}