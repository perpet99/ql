using System;
using System.Collections.Generic;
using QLearningFramework;

namespace ConsoleQLearning
{
    class RockPaperScissorsDemo
    {
        // ----------- Insert the state names here -----------
        internal enum StateNameEnum
        {
            Begin, Rock, Paper, Scissor
        }
        // ----------- End Insert the state names here -------

        //static void Main(string[] args)
        //{
        //    DateTime starttime = DateTime.Now;

        //    RockPaperScissors();

        //    double timespend = DateTime.Now.Subtract(starttime).TotalSeconds;
        //    Console.WriteLine("\n{0} sec. press a key ...", timespend.Pretty()); Console.ReadKey();
        //}

        static void RockPaperScissors()
        {
            QLearning q = new QLearning
            {
                Episodes = 1000,
                Alpha = 0.1,
                Gamma = 0.9,
                MaxExploreStepsWithinOneEpisode = 1000
            };

            var opponentStyles = new List<double[]>();
            // rock paper scissor probability styles
            opponentStyles.Add(new[] { 0.33, 0.33, 0.33 });
            opponentStyles.Add(new[] { 0.5, 0.3, 0.2 });
            opponentStyles.Add(new[] { 0.2, 0.5, 0.3 });
            opponentStyles.Add(new[] { 0.1, 0.1, 0.8 });
            int index = new Random().Next(opponentStyles.Count);
            var opponent = opponentStyles[index];

            // opponent probability pick 
            double rockOpponent = opponent[0];
            double paperOpponent = opponent[1];
            double scissorOpponent = opponent[2];

            QAction fromTo;
            QState state;
            string stateName;
            string stateNameNext;

            // ----------- Begin Insert the path setup here -----------

            // insert the end states here
            q.EndStates.Add(StateNameEnum.Rock.EnumToString());
            q.EndStates.Add(StateNameEnum.Paper.EnumToString());
            q.EndStates.Add(StateNameEnum.Scissor.EnumToString());

            // State Begin
            stateName = StateNameEnum.Begin.EnumToString();
            q.AddState(state = new QState(stateName, q));

            // action Rock
            stateNameNext = StateNameEnum.Rock.EnumToString();
            state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
            // action outcome probability
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Rock.EnumToString(),
                rockOpponent)
            { Reward = 0 });
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Paper.EnumToString(),
                paperOpponent)
            { Reward = -10 });
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Scissor.EnumToString(),
                scissorOpponent)
            { Reward = 100 });

            // action paper
            stateNameNext = StateNameEnum.Paper.EnumToString();
            state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
            // action outcome probability
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Rock.EnumToString(),
                rockOpponent)
            { Reward = 100 });
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Paper.EnumToString(),
                paperOpponent)
            { Reward = 0 });
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Scissor.EnumToString(),
                scissorOpponent)
            { Reward = -10 });

            // action scissor
            stateNameNext = StateNameEnum.Scissor.EnumToString();
            state.AddAction(fromTo = new QAction(stateName, new QActionName(stateName, stateNameNext)));
            // action outcome probability
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Rock.EnumToString(),
                rockOpponent)
            { Reward = -10 });
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Paper.EnumToString(),
                paperOpponent)
            { Reward = 100 });
            fromTo.AddActionResult(new QActionResult(fromTo, StateNameEnum.Scissor.EnumToString(),
                scissorOpponent)
            { Reward = 0 });
            // ----------- End Insert the path setup here -----------

            q.RunTraining();
            q.PrintQLearningStructure();
            q.ShowPolicy();

            Console.WriteLine("\n** Opponent style **");
            Console.WriteLine(string.Format("style is rock {0} paper {1} scissor {2}",
                opponent[0].Pretty(), opponent[1].Pretty(), opponent[2].Pretty()));
        }
    }
}