using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComponentA
{
    public interface IComponentA
    {
        void Process();
    }

    public class ComponentA : IComponentA
    {
        public void Process()
        {
            Console.WriteLine("This comes from Component A.");
        }
    }
}
