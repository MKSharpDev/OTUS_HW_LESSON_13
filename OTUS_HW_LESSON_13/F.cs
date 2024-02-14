using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTUS_HW_LESSON_13
{
    public class F
    {
        public int I1, I2, I3, I4, I5;
        Random random = new Random();

        public F(int i1, int i2, int i3, int i4, int i5) 
        { 
            I1 = i1;
            I2 = i2;
            I3 = i3;    
            I4 = i4;
            I5 = i5;
        }

        public F()
        {
            I1 = random.Next(1000);
            I2 = random.Next(1000);
            I3 = random.Next(1000);
            I4 = random.Next(1000);
            I5 = random.Next(1000);
        }
    }
}
