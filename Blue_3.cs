using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string name;
            private string surname;
            protected int[] penaltyTimes;
            private int matchCount;

            public string Name => name;
            public string Surname => surname;
            public int[] Penalties
            {
                get
                {
                    if (penaltyTimes == null) return null;
                    int[] NPenaltyTimes = new int[penaltyTimes.Length];
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        NPenaltyTimes[i] = penaltyTimes[i];
                    }
                    return NPenaltyTimes;

                }
            }

            public int Total
            {
                get
                {
                    if (penaltyTimes == null) return 0;
                    int total = 0;
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        total += penaltyTimes[i];
                    }
                    return total;
                }
            }

            public virtual bool IsExpelled
            {
                get
                {
                    if (penaltyTimes == null) return false;
                    for (int i = 0; i < penaltyTimes.Length; i++)
                    {
                        if (penaltyTimes[i] == 10)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.penaltyTimes = new int[0];
                this.matchCount = 0;
            }

            public virtual void PlayMatch(int time)
            {
                if ((time != 0 && time != 2 && time != 5 && time != 10) || penaltyTimes == null || matchCount < 0)
                {
                    return;
                }
                int[] newPenaltyTimes = new int[penaltyTimes.Length + 1];
                for (int i = 0; i < penaltyTimes.Length; i++)
                {
                    newPenaltyTimes[i] = penaltyTimes[i];
                }
                penaltyTimes = newPenaltyTimes;
                penaltyTimes[penaltyTimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                string status = "";
                if (IsExpelled)
                {
                    status = "Исключён";
                }
                else
                {
                    status = "Остаётся в составе";
                }
                Console.WriteLine($"Имя - {Name},\nФамилия - {Surname},\nВсего времени в 'бане' - {Total},\n{status}");
            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
            }

            public override bool IsExpelled
            {
                get
                {
                    int matches = penaltyTimes.Length;
                    int bans = 0;
                    for (int i = 0; i < matches; i++)
                    {
                        bans += penaltyTimes[i];
                    }

                    return matches > 0 && (penaltyTimes.Count(time => time == 5) > matches * 0.1 || bans > matches * 2);
                }
            }

            public override void PlayMatch(int bans)
            {
                if (bans < 0 || bans > 5) return;

                int[] newPenaltyTimes = new int[penaltyTimes.Length + 1];

                penaltyTimes.CopyTo(newPenaltyTimes, 0);
                newPenaltyTimes[penaltyTimes.Length] = bans;

                penaltyTimes = newPenaltyTimes;
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int totalPenaltyTime;
            private static int totalPlayers;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                totalPlayers++;
            }

            public override bool IsExpelled
            {
                get
                {
                    double avg = totalPlayers > 0 ? totalPenaltyTime / (double)totalPlayers : 0;
                    return penaltyTimes.Any(time => time == 10) || Total > (avg / 10);
                }
            }

            public override void PlayMatch(int time)
            {
                if ((time != 0 && time != 2 && time != 5 && time != 10) || penaltyTimes == null)
                {
                    return;
                }

                base.PlayMatch(time);
                totalPenaltyTime += time;
            }
        }
    }
}

