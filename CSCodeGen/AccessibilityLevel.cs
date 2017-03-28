using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZESoft.Common;

namespace CSCodeGen
{
    public sealed class AccessibilityLevel : TypeSafeEnum
    {
        private AccessibilityLevel(int value, string name) : base(value, name) { }

        public static readonly AccessibilityLevel Public = new AccessibilityLevel(AccessibilityLevels.Public, nameof(AccessibilityLevels.Public));
        public static readonly AccessibilityLevel Private = new AccessibilityLevel(AccessibilityLevels.Private, nameof(AccessibilityLevels.Private));
        public static readonly AccessibilityLevel Protected = new AccessibilityLevel(AccessibilityLevels.Protected, nameof(AccessibilityLevels.Protected));
        public static readonly AccessibilityLevel Internal = new AccessibilityLevel(AccessibilityLevels.Internal, nameof(AccessibilityLevels.Internal));

        public class AccessibilityLevels
        {
            public const int Public = 1;
            public const int Private = 2;
            public const int Protected = 3;
            public const int Internal = 4;
        }
    }
}
