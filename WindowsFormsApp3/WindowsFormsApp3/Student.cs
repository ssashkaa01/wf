using System;
using System.Collections.Generic;

namespace WindowsFormsApp3
{
    public partial class Form1
    {
        [Serializable]
        class Student
        {

            public string Name { get; set; }
            public string Surname { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public DateTime Birthday { get; set; }
            public bool IsMale { get; set; }
            public List<string> Techlogies { get; set; }

        }
    }
}
