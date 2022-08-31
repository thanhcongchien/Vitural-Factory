using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Utils
{
    public static Vector3 Round(Vector3 d, int digits)
    {
        Vector3 result = new Vector3((float)System.Math.Round(d.x, digits),
                                     (float)System.Math.Round(d.y, digits),
                                     (float)System.Math.Round(d.z, digits));
        return result;
    }

    public static Vector2 Round(Vector2 d, int digits)
    {
        Vector2 result = new Vector2((float)System.Math.Round(d.x, digits),
                                     (float)System.Math.Round(d.y, digits));
        return result;
    }

    public static string[] ToString(Vector3 v, string format)
    {
        string[] result = { ToString(v.x, format),
                            ToString(v.y, format),
                            ToString(v.z, format) };

        return result;
    }

    public static string ToString(float d, string format)
    {
        return d.ToString(format, CultureInfo.InvariantCulture);
    }

    public static float Parse(string str)
    {
        return float.Parse(str, CultureInfo.InvariantCulture);
    }

    public static string ShowPrice(float data) {
        if (data < 0f) {
            return "X";
        }
        if (data == 0) {
            return "Free";
        }
        return ConvertNumbertoString(data);
    }

    public static string ConvertNumbertoString(float data)
    {
        float value;
        string result = "" + data;

        if(data >= 1000000000)
        {
            value = data / 1000000000;    
            result = value.ToString("F2") + "B";
        }
        else if(data >= 1000000)
        {
            value = data / 1000000;
            result = value.ToString("F2") + "M";
        }
       
        return result;
    }

    public static Vector3 Parse(string[] strV)
    {
        Vector3 result = new Vector3(Parse(strV[0]), Parse(strV[1]), Parse(strV[2]));
        return result;
    }

    #region Angle From/To Vector
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree, float length)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad) * length;
    }

    public static float Vector2ToDegree(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    public static float Vector2ToRadian(Vector2 v)
    {
        return Mathf.Atan2(v.y, v.x);
    }
    #endregion

    public static float RoundToZero(float value, int digits)
    {
        string sValue = value.ToString("F" + digits.ToString());
        float fValue = float.Parse(sValue);
        return fValue;
    }

    public static Vector2 RoundToZero(Vector2 value, int digits)
    {
        return new Vector2(RoundToZero(value.x, digits), RoundToZero(value.y, digits));

    }
    public static double GetCurrentXp(int currentLevel) {
        return 70 * currentLevel * currentLevel * currentLevel + 20 * currentLevel * currentLevel + 50 * currentLevel;
    }
}
