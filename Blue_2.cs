using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string name;
            private string surname;
            private int[,] marks;

            public string Name => name;
            public string Surname => surname;
            public int[,] Marks
            {
                get
                {
                    if (marks == null || marks.GetLength(0) < 1 || marks.GetLength(1) < 1) return null;
                    int[,] NMarks = new int[marks.GetLength(0), marks.GetLength(1)];
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            NMarks[i, j] = marks[i, j];
                        }
                    }
                    return NMarks;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (marks == null || marks.GetLength(0) < 1 || marks.GetLength(1) < 1) return 0;
                    int total = 0;
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            total += marks[i, j];
                        }
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                this.marks = new int[2, 5];
            }

            public void Jump(int[] result)
            {
                if (result == null || marks == null)
                {
                    return;
                }

                for (int i = 0; i < marks.GetLength(0); i++)
                {
                    bool isNull = true;
                    for (int j = 0; j < marks.GetLength(1); j++)
                    {
                        if (marks[i, j] != 0)
                        {
                            isNull = false;
                            break;
                        }
                    }

                    if (isNull)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
                        {
                            marks[i, j] = result[j];
                        }
                        return;
                    }
                }

                return;
            }

            public static void Sort(Participant[] array)
            {
                if (array.Length < 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"Имя участника - {Name},\nФамилия участника - {Surname},\nСумма баллов - {TotalScore}");
                Console.WriteLine("Оценки судей:");
                for (int jumpIndex = 0; jumpIndex < marks.GetLength(0); jumpIndex++)
                {
                    Console.Write("Прыжок №" + (jumpIndex + 1) + ": ");
                    for (int judgeIndex = 0; judgeIndex < marks.GetLength(1); judgeIndex++)
                    {
                        Console.Write($"Судья {judgeIndex + 1}: {marks[jumpIndex, judgeIndex]}, ");
                    }
                }
            }
        }
        
        public abstract class WaterJump
        {
            private string name;
            private double bank;
            private Participant[] participants;

            public string Name => name;
            public double Bank => bank;
            public Participant[] Participants => participants;

            public WaterJump(string name, double bank)
            {
                this.name = name;
                this.bank = bank;
                this.participants = new Participant[0];
            }

            public abstract double[] Prize { get; }

            public void Add(Participant participant)
            {
                Participant[] newparticipants = new Participant[this.participants.Length + 1];
                for (int i = 0; i < this.participants.Length; i++)
                {
                    newparticipants[i] = this.participants[i];
                }
                newparticipants[newparticipants.Length - 1] = participant;
                this.participants = newparticipants;
            }

            public void Add(Participant[] participants)
            {
                for (int i = 0; i < this.participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, double bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;

                    double[] prize = new double[3];

                    prize[0] = 0.5 * Bank;
                    prize[1] = 0.3 * Bank;
                    prize[2] = 0.2 * Bank;

                    return prize;
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;

                    int n = Participants.Length / 2;

                    if (n > 10) n = 10;
                    else if (n < 3) return null;

                    double[] prize = new double[n];
                    double N = (0.2 * Bank) / n;

                    prize[0] = 0.4 * Bank + N;
                    prize[1] = 0.25 * Bank + N;
                    prize[2] = 0.15 * Bank + N;


                    for (int i = 3; i < n; i++)
                    {
                        prize[i] = N;
                    }

                    return prize;
                }
            }
        }
    }
}