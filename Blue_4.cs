using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string name;
            private int[] scores;


            public string Name { get { return name; } }
            public int[] Scores
            {
                get
                {
                    if (scores == null) return null;
                    int[] NScores = new int[scores.Length];
                    for (int i = 0; i < NScores.Length; i++) { NScores[i] = scores[i]; }
                    return NScores;
                }
            }

            public Team(string newname)
            {
                name = newname;
                scores = new int[0];
            }

            public int TotalScore
            {
                get
                {
                    if (scores == null) return 0;
                    int ts = 0;
                    for (int i = 0; i < scores.Length; i++) { ts += scores[i]; }
                    return ts;
                }
            }

            public void PlayMatch(int result)
            {
                int[] NScores = new int[scores.Length + 1];
                for (int i = 0; i < scores.Length; i++) { NScores[i] = scores[i]; }
                NScores[NScores.Length - 1] = result;
                scores = NScores;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} набрала {TotalScore}.");
            }

        }

        public class Group
        {
            private string name;
            private ManTeam[] manteams;
            private WomanTeam[] womanteams;
            private int curMteams;
            private int curWteams;

            public string Name { get { return name; } }
            public Team[] ManTeams { get { return manteams; } }
            public Team[] WomanTeams { get { return womanteams; } }

            public Group(string newname)
            {
                name = newname;
                manteams = new ManTeam[12];
                womanteams = new WomanTeam[12];
                curMteams = 0;
                curWteams = 0;
            }
            public void Add(Team team)
            {
                if (team is ManTeam manTeam && curMteams < manteams.Length)
                {
                    manteams[curMteams] = (ManTeam) team;
                    curMteams++;
                } else if (team is WomanTeam womanTeam && curWteams < womanteams.Length)
                {
                    womanteams[curWteams] = (WomanTeam) team;
                    curWteams++;
                }
            }
            public void Add(Team[] newteams)
            {
                if (manteams == null || womanteams == null || newteams.Length == 0 || newteams == null) return;

                for (int i = 0; i < newteams.Length; i++)
                {
                    Add(newteams[i]);
                }
            }
            public void Sort()
            {
                if (manteams == null || manteams.Length == 0 || womanteams == null || womanteams.Length == 0) return;
                for (int i = 0; i < manteams.Length - 1; i++)
                {
                    for (int j = 0; j < manteams.Length - i - 1; j++)
                    {
                        if (manteams[j].TotalScore < manteams[j + 1].TotalScore)
                        {
                            ManTeam temp = manteams[j];
                            manteams[j] = manteams[j + 1];
                            manteams[j + 1] = temp;
                        }
                        if (womanteams[j].TotalScore < womanteams[j + 1].TotalScore)
                        {
                            WomanTeam temp = womanteams[j];
                            womanteams[j] = womanteams[j + 1];
                            womanteams[j + 1] = temp;
                        }
                    }
                }
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group ngroup = new Group("Финалисты");

                if (size != 12) return ngroup;

                group1.Sort();
                group2.Sort();


                for (int i = 0; i < size / 2; i++)
                {
                    ngroup.Add(group1.ManTeams[i]);
                    ngroup.Add(group1.WomanTeams[i]);
                }

                for (int i = 0; i < size / 2; i++)
                {
                    ngroup.Add(group2.ManTeams[i]);
                    ngroup.Add(group2.WomanTeams[i]);
                }

                ngroup.Sort();

                return ngroup;
            }

            public void Print()
            {
                Console.WriteLine($"Группа {name}");
                Console.WriteLine("Мужчины:");
                for (int i = 0; i < manteams.Length; i++)
                {
                    manteams[i].Print();
                }
                Console.WriteLine("Женщины:");
                for (int i = 0; i < womanteams.Length; i++)
                {
                    womanteams[i].Print();
                }

            }

        }

        public class ManTeam: Team
        {
            public ManTeam(string name): base(name) { }
        }

        public class WomanTeam: Team
        {
            public WomanTeam(string name) : base(name) { }
        }
    }

}