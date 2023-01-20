using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Globalization;

/* 
    This script is courtesy of https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
    All rights belong to the writer.
*/

public class DatasetReader : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, float>> Read(string file)
    {
        TextAsset data = Resources.Load(file) as TextAsset;

        var list = new List<Dictionary<string, float>>();
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;
        
        var header = Regex.Split(lines[0], SPLIT_RE);

        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, float>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                float finalvalue = 0;
                float f;

                if (float.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out f))
                {
                    finalvalue = f;
                }

                entry[header[j]] = finalvalue;
            }

            list.Add(entry);
        }

        return list;
    }
}