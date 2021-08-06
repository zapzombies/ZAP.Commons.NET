using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAP.Commons
{
    public static class Validate
    {
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
                throw new ValidationException(message);
        }

        public static void IsFalse(bool condition, string message) => IsTrue(!condition, message);

        public static void NotNull(object objectToTest, string message) => IsTrue(objectToTest != null, message);

        public static void NotNullOrEmpty(string target, string message) => IsFalse(string.IsNullOrEmpty(target), message);

        public static T IsType<T>(object target, string message)
        {
            IsTrue(target is T, message);
            return (T)target;
        }
    }
}
