using System;
using System.Collections.Generic;

class TuringMachine
{
    public string Tape;
    public int Head;
    public string State;

    private Dictionary<(string, char), (string, char, int)> transitions =
        new Dictionary<(string, char), (string, char, int)>();

    private const char Blank = '_';

    public TuringMachine(string input)
    {
        Tape = input + "_____";
        Head = 0;
        State = "q0";
        BuildTransitions();
    }

    void BuildTransitions()
    {
        // q0: find leftmost unmatched 0
        transitions.Add(("q0", '0'), ("q1", 'X', +1));  // mark left 0
        transitions.Add(("q0", 'X'), ("q0", 'X', +1));  // skip marked
        transitions.Add(("q0", '#'), ("q4", '#', +1));  // go validate right side
        transitions.Add(("q0", '_'), ("q4", '_', +1));  // edge case: "#"

        // q1: move right to #
        transitions.Add(("q1", '0'), ("q1", '0', +1));
        transitions.Add(("q1", 'X'), ("q1", 'X', +1));
        transitions.Add(("q1", '#'), ("q2", '#', +1));

        // q2: find matching rightmost 0
        transitions.Add(("q2", 'Y'), ("q2", 'Y', +1));   // skip already matched
        transitions.Add(("q2", '0'), ("q3", 'Y', -1));   // found match
        transitions.Add(("q2", '_'), ("qreject", '_', 0));

        // q3: return left until reaching X or start
        transitions.Add(("q3", 'Y'), ("q3", 'Y', -1));
        transitions.Add(("q3", '0'), ("q3", '0', -1));
        transitions.Add(("q3", '#'), ("q3", '#', -1));
        transitions.Add(("q3", 'X'), ("q0", 'X', +1));   // begin next cycle
        transitions.Add(("q3", '_'), ("q0", '_', +1));   // reached start

        // q4: verify right side has only Y or blanks
        transitions.Add(("q4", 'Y'), ("q4", 'Y', +1));
        transitions.Add(("q4", '#'), ("q4", '#', +1));
        transitions.Add(("q4", '_'), ("qaccept", '_', 0));

        // any 0 on right side means mismatch
        transitions.Add(("q4", '0'), ("qreject", '0', 0));
    }

    public void Run()
    {
        while (State != "qaccept" && State != "qreject")
        {
            PrintStep();

            char current = Tape[Head];
            if (transitions.TryGetValue((State, current), out var t))
            {
                State = t.Item1;
                Tape = Tape.Remove(Head, 1).Insert(Head, t.Item2.ToString());
                Head += t.Item3;
                if (Head < 0) Head = 0;
            }
            else
            {
                State = "qreject";
            }
        }

        PrintStep();
        Console.WriteLine($"RESULT: {State.ToUpper()}");
    }

    void PrintStep()
    {
        Console.WriteLine($"STATE: {State}");
        Console.WriteLine($"TAPE : {Tape}");
        Console.WriteLine($"HEAD : {new string(' ', Head)}^");
        Console.WriteLine("----------------------------------------");
    }
}

class Program
{
    static void Main()
    {
        string[] tests = {
            "0#",
            "01#10",
            "#10",
            "0100#0010",
            "010#010"
        };

        foreach (var t in tests)
        {
            Console.WriteLine($"\n=== RUNNING INPUT: {t} ===\n");
            var tm = new TuringMachine(t);
            tm.Run();
        }
    }
}
