using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._11._2019
{
    [Serializable]
    class Order
    {
        private List<string> goods;
        private int sum;
        private int ID { get; set; }
        private DateTime time { get; set; }

        public Order(int id)
        {
            goods = new List<string>();
            ID = id;
            time = DateTime.Now;
        }

        public void Add(string good, int sumGood)
        {
            goods.Add(good);
            sum += sumGood;
        }

        public int GetID()
        {
            return ID;
        }

        public string GetOrder()
        {
            string order = "";

            foreach (string good in goods)
            {
                order += $"{good}\n";
            }

            return order;
        }

        public DateTime GetTime()
        {
            return time;
        }

        public string GetDescription()
        {
            return $"#{GetID()} :: {GetTime()}";
        }
        public int GetSum()
        {
            return sum;
        }
    }
}
