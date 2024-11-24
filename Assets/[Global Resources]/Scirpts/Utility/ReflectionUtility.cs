/*using System;
using System.Reflection;

namespace FSF.coll{
public static class ReflectionUtility
{
    /// <summary>
    /// T is target class, struct and so on, 
    /// U is return type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="target"></param>
    /// <param name="FieldName"></param>
    /// <returns></returns>
    public static U GetFieldValue<T, U>(T target, string FieldName){
        Type type = typeof(T);
        FieldInfo field = type.GetField
        (FieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        return (U)field.GetValue(target);
    }

    /// <summary>
    /// T is target class, struct and so on, 
    /// U is value type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="target"></param>
    /// <param name="FieldName"></param>
    /// <param name="endValue"></param>
    public static void SetFieldValue<T, U>(string FieldName, U endValue){
        Type type = typeof(T);
        FieldInfo field = type.GetField
        (FieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        field.SetValue(field, endValue);
    }

    /// <summary>
    /// T is target class, struct and so on, 
    /// U is return type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="target"></param>
    /// <param name="PropertyName"></param>
    /// <returns></returns>
    public static U GetPropertyValue<T, U>(T target, string PropertyName){
        Type type = typeof(T);
        PropertyInfo property = type.GetProperty
        (PropertyName, BindingFlags.Static | BindingFlags.NonPublic);
        return (U)property.GetValue(target);
    }

    /// <summary>
    /// T is target class, struct and so on, 
    /// U is value type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="target"></param>
    /// <param name="FieldName"></param>
    /// <param name="endValue"></param>
    public static void SetPropertyValue<T, U>(string PropertyName, U endValue){
        Type type = typeof(T);
        PropertyInfo property = type.GetProperty
        (PropertyName, BindingFlags.Static | BindingFlags.NonPublic);
        property.SetValue(property, endValue);
    }
}
}*/
