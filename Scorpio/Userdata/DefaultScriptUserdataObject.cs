﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Scorpio;
using Scorpio.Variable;
using Scorpio.Exception;
namespace Scorpio.Userdata
{
    /// <summary> 普通Object类型 </summary>
    public class DefaultScriptUserdataObject : ScriptUserdata
    {
        private UserdataType m_Type;
        public DefaultScriptUserdataObject(Script script, object value, UserdataType type) : base(script)
        {
            this.Value = value;
            this.ValueType = value.GetType();
            this.m_Type = type;
        }
        public override ScriptObject GetValue(string strName)
        {
            object ret = m_Type.GetValue(Value, strName);
            if (ret is UserdataMethod) {
                UserdataMethod method = ret as UserdataMethod;
                if (method.IsStatic)
                    return Script.CreateFunction(new ScorpioStaticMethod(strName, method));
                else
                    return Script.CreateFunction(new ScorpioObjectMethod(Value, strName, method));
            }
            return Script.CreateObject(ret);
        }
        public override void SetValue(string strName, ScriptObject value)
        {
            m_Type.SetValue(Value, strName, value);
        }
    }
}
