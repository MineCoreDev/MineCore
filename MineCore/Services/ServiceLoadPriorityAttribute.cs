using System;

namespace MineCore.Services
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceLoadPriorityAttribute : Attribute
    {
        public ServiceLoadPriority Priority { get; }

        public ServiceLoadPriorityAttribute(ServiceLoadPriority priority)
        {
            Priority = priority;
        }
    }
}