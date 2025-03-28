using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_5;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string name;
            private string surname;
            private int place;
            private bool placeSet;

            public string Name => name;
            public string Surname => surname;
            public int Place => place;

            public Sportsman(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.place = 0;
                this.placeSet = false;
            }

            public void SetPlace(int place)
            {
                if (placeSet)
                {
                    return;
                }

                if (place < 1 || place > 18)
                {
                    return;
                }

                this.place = place;
                this.placeSet = true;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}, занял(а) {Place} место");
            }
        }

        public abstract class Team
        {
            private string name;
            private Sportsman[] sportsmen;
            private int sportsmanCount;

            public string Name => name;
            public Sportsman[] Sportsmen => sportsmen;

            public int SummaryScore
            {
                get
                {
                    if (sportsmanCount == 0 || sportsmen == null) return 0;
                    int totalScore = 0;
                    for (int i = 0; i < sportsmanCount; i++)
                    {
                        int place = sportsmen[i].Place;
                        if (place == 1) totalScore += 5;
                        else if (place == 2) totalScore += 4;
                        else if (place == 3) totalScore += 3;
                        else if (place == 4) totalScore += 2;
                        else if (place == 5) totalScore += 1;
                    }
                    return totalScore;
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                Team best = null;
                foreach (Team team in teams)
                {
                    if (best == null || best.GetTeamStrength() < team.GetTeamStrength())
                    {
                        best = team;
                    }
                }
                return best;
            }
            public int TopPlace
            {
                get
                {
                    if (sportsmen == null) return 0;
                    int topPlace = 18;
                    for (int i = 0; i < sportsmanCount; i++)
                    {
                        if (sportsmen[i].Place < topPlace)
                        {
                            topPlace = sportsmen[i].Place;
                        }
                    }
                    return topPlace;
                }
            }
            public Team(string name)
            {
                this.name = name;
                this.sportsmen = new Sportsman[6];
                this.sportsmanCount = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (sportsmen == null)
                {
                    sportsmen = new Sportsman[6];
                }
                if (sportsmanCount < 6)
                {
                    sportsmen[sportsmanCount] = sportsman;
                    sportsmanCount++;
                }
            }

            public void Add(Sportsman[] newSportsmen)
            {
                foreach (var sportsman in newSportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Команда {Name} заработала в сумме: {SummaryScore}. Высшая позиция в команде: {TopPlace}");
                for (int i = 0; i < sportsmanCount; i++)
                {
                    sportsmen[i].Print();
                }
            }
        }

        public abstract class ManTeam: Team
        {
            public ManTeam(string name): base(name) { }

            protected override double GetTeamStrength()
            {
                double strength = 0;
                foreach (var sportsman in Sportsmen)
                {
                    strength += sportsman.Place;
                }
                strength /= Sportsmen.Length;
                return 100 / strength;
            }
        }

        public abstract class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double strength = 0;
                double proizvedenie = 1;
                foreach (var sportsman in Sportsmen)
                {
                    strength += sportsman.Place;
                    proizvedenie *= sportsman.Place;
                }
                strength *= 100;
                return strength * Sportsmen.Length / proizvedenie;
            }
        }
    }
}