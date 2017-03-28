using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCodeGen
{
    //public sealed class CGMethodArgument<ArgType>
    public sealed class CGMethodArgument
    {
        Type mArgumentType { get; set; }
        //public Type ArgumentType => mArgumentType;
        string mCustomArgumentType { get; set; }
        public string ArgumentTypeName
        {
            get
            {
                if (String.IsNullOrEmpty(mCustomArgumentType))
                {
                    return mArgumentType.Name;
                }
                else
                {
                    return mCustomArgumentType;
                }
            }
        }

        string mArgumentName { get; set; }
        public string ArgumentName => mArgumentName;

        //ArgType mDefaultValue { get; set; }
        //public ArgType DefaultValue => mDefaultValue;
        bool mDefaultIsSet { get; set; } = false;

        object mDefaultValue { get; set; }
        public object DefaultValue => mDefaultValue;

        public CGMethodArgument(Type argumentType, string argumentName)
        {
            mArgumentType = argumentType;
            mArgumentName = argumentName;
        }
        public CGMethodArgument(Type argumentType, string argumentName, object defaultValue)
        {
            mArgumentType = argumentType;
            mArgumentName = argumentName;
            mDefaultIsSet = true;
            mDefaultValue = defaultValue;
        }
        public CGMethodArgument(string customArgumentType, string argumentName)
        {
            mCustomArgumentType = customArgumentType;
            mArgumentName = argumentName;
        }
        public CGMethodArgument(string customArgumentType, string argumentName, object defaultValue)
        {
            mCustomArgumentType = customArgumentType;
            mArgumentName = argumentName;
            mDefaultIsSet = true;
            mDefaultValue = defaultValue;
        }

        //public CGMethodArgument(string argumentName)
        //{
        //    mArgumentName = argumentName;
        //    mArgumentType = typeof(ArgType);
        //}

        //public CGMethodArgument(string argumentName, ArgType defaultValue)
        //{
        //    mArgumentName = argumentName;
        //    mArgumentType = typeof(ArgType);
        //    mDefaultValue = defaultValue;
        //}

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!mDefaultIsSet)
            {
                sb.Append($"{ArgumentTypeName} {ArgumentName}");
            }
            else
            {
                string defaultValue;
                if (mDefaultValue == null)
                {
                    defaultValue = "null";
                }
                else
                {
                    defaultValue = mDefaultValue.ToString();
                }
                sb.Append($"{ArgumentTypeName} {ArgumentName} = {defaultValue}");
            }
            return sb.ToString();
        }
    }
}
