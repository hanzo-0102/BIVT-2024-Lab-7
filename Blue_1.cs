﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_1;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string name;
            protected int votes;
            public string Name => name;
            public int Votes => votes;

            public Response(string _name)
            {
                this.name = name;
                this.votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null ||  responses.Length <= 0) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == this.Name)
                    {
                        count++;
                    }
                }
                this.votes = count;
                return this.votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"Имя - {Name},\nГолоса - {Votes};");
            }
        }

        public class HumanResponse : Response
        {
            private string surName;

            public string Surname => surName;

            public HumanResponse(string name, string lastName) : base(name)
            {
                this.surName = lastName;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length <= 0) return 0;
                int count = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse humanResponse = responses[i] as HumanResponse;
                    if (humanResponse != null && humanResponse.Name == this.Name && humanResponse.Surname == this.surName)
                    {
                        count++;
                    }
                }
                this.votes = count;
                return this.votes;
            }

            public override void Print()
            {
                Console.WriteLine($"Имя - {Name},\nИмя - {Surname},\nГолоса - {Votes};");
            }
        }

    }
}