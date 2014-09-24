using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComponentB
{
    public interface IComponentB
    {
        void Process();
    }

    public class ComponentB : IComponentB
    {
        public void Process()
        {
            Console.WriteLine("This comes from Component B.");
        }
    }
}
