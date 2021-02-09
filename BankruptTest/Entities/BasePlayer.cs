using BankruptTest.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankruptTest.Entities
{
    class BasePlayer
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int CurrentCredit { get; set; }
        public int CurrentPlaceOnBoard { get; set; } = 1;
        public bool Bankrupt { get; set; }
        public IList<Propriedade> Properties { get; set; }
        public BehaviorList Behavior { get; set; }

        public BasePlayer(int id, BehaviorList behavior)
        {
            this.Id = id;
            this.Behavior = behavior;
            this.Bankrupt = false;
            this.CurrentCredit = 200;
            Properties = new List<Propriedade>();
        }

        public void Move(int spots)
        {
            Console.WriteLine($"Player {this.Id} rolled a {spots}!");
            if (this.CurrentPlaceOnBoard + spots > 20)
            {
                int overflow = (this.CurrentPlaceOnBoard += spots) - 20;
                Console.WriteLine("You gained 100 coins");
                this.CurrentCredit += 100;
                this.CurrentPlaceOnBoard = overflow;
                Console.WriteLine($"Player {this.Id} is now at Property {this.CurrentPlaceOnBoard}.");
                return;
            }

            this.CurrentPlaceOnBoard += spots;
            Console.WriteLine($"Player {this.Id} is now at Property {this.CurrentPlaceOnBoard}.");

        }

        public void CheckBehavior(Propriedade property)
        {
            switch (this.Behavior)
            {
                case BehaviorList.Impulsive:
                    if (property.CurrentOwnerId == 0)
                    {
                        this.BuyProperty(property);
                    }
                    break;
                case BehaviorList.Demanding:
                    if (property.CurrentOwnerId == 0 && property.Rent > 50)
                    {
                        this.BuyProperty(property);
                    }
                    break;
                case BehaviorList.Cautious:
                    if (property.CurrentOwnerId == 0)
                    {
                        if (this.CurrentCredit - property.Cost >= 80)
                        {
                            this.BuyProperty(property);
                        }
                    }
                    break;
                case BehaviorList.Unpredictable:
                    if (CoinFlip())
                    {
                        this.BuyProperty(property);
                    }
                    break;
            }
        }
        public void BuyProperty(Propriedade property)
        {
            if (this.CurrentCredit < property.Cost)
            {
                return;
            }

            this.CurrentCredit -= property.Cost;
            this.Properties.Add(property);
            property.CurrentOwnerId = this.Id;
            Console.WriteLine($"Player {this.Id} now owns Property {property.Id}.");
        }

        public void PayRent(Propriedade property, BasePlayer owner)
        {

            if (property.Rent >= this.CurrentCredit)
            {
                int paycheck = this.CurrentCredit;
                owner.CurrentCredit += paycheck;
                CurrentCredit = 0;
                this.Bankrupt = true;
                Console.WriteLine($"Player {this.Id} paid {paycheck} credits to Player {owner.Id}.");
            }
            else
            {
                this.CurrentCredit -= property.Rent;
                owner.CurrentCredit += property.Rent;
                Console.WriteLine($"Player {this.Id} paid {property.Rent} credits to Player {owner.Id}.");

            }


        }

        public override string ToString()
        {
            return $"This is Player {this.Id}";
        }

        public void CheckBankrupt()
        {
            if (this.CurrentCredit == 0)
            {
                this.Bankrupt = true;
                foreach (var prop in this.Properties)
                {
                    prop.CurrentOwnerId = 0;
                }
            }
        }

        public static bool CoinFlip()
        {
            bool result = false;
            if (ThreadSafeRandom.ThisThreadsRandom.Next(0, 2) != 0)
                result = true;
            return result;


        }
    }
}
