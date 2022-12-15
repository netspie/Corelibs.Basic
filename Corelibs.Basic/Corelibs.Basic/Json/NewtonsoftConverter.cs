﻿using Newtonsoft.Json;
using System;

namespace Common.Basic.Json
{
    public class NewtonsoftJsonConverter : IJsonConverter
    {

        T IJsonConverter.Deserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default;
            }
        }

        string IJsonConverter.Serialize<T>(T obj)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            try
            {
                return JsonConvert.SerializeObject(obj, jsonSettings);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return default;
            }
        }
    }
}
