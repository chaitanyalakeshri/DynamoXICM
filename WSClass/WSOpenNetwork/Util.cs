using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace WSClass.OpenNetwork
{
    public static class NameConverter
    {
        private static readonly Dictionary<string, Dictionary<string, string>> RubyToCSharpField;
        private static readonly Dictionary<string, Dictionary<string, string>> CSharpToRubyField;
        private static readonly Dictionary<string, string> RubyToCSharpClass;
        private static readonly Dictionary<string, string> CSharpToRubyClass;

        static NameConverter()
        {
            // Load the JSON files once at startup
            RubyToCSharpField = LoadFieldMapping("WSClass.WSOpenNetwork.ruby_to_csharp_field.json");
            CSharpToRubyField = LoadFieldMapping("WSClass.WSOpenNetwork.csharp_to_ruby_field.json");

            RubyToCSharpClass = LoadClassdMapping("WSClass.WSOpenNetwork.ruby_class_to_csharp_field.json");
            CSharpToRubyClass = LoadClassdMapping("WSClass.WSOpenNetwork.csharp_class_to_ruby_field.json");
        }

        private static Dictionary<string, Dictionary<string, string>> LoadFieldMapping(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"Resource '{resourceName}' not found.");
            using StreamReader reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
        }
        private static Dictionary<string, string> LoadClassdMapping(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new FileNotFoundException($"Resource '{resourceName}' not found.");
            using StreamReader reader = new StreamReader(stream);
            var json = reader.ReadToEnd();
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }

        public static string RubyFieldToCSharp(string className, string rubyField)
        {
            if (string.IsNullOrWhiteSpace(className))
                throw new ArgumentException("Class name cannot be null or empty.", nameof(className));
            if (string.IsNullOrWhiteSpace(rubyField))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(rubyField));

            if (RubyToCSharpField.TryGetValue(className, out var classMapping))
            {
                if (classMapping.TryGetValue(rubyField, out var csharpField))
                {
                    return csharpField;
                }
                throw new KeyNotFoundException($"Field '{rubyField}' not found in Ruby to C# field mapping for class '{className}'.");
            }
            throw new KeyNotFoundException($"Class '{className}' not found in Ruby to C# field mapping.");
        }

        public static string CSharpFieldToRuby(string className, string csharpField)
        {
            if (string.IsNullOrWhiteSpace(className))
                throw new ArgumentException("Class name cannot be null or empty.", nameof(className));
            if (string.IsNullOrWhiteSpace(csharpField))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(csharpField));
            if (CSharpToRubyField.TryGetValue(className, out var classMapping))
            {
                if (classMapping.TryGetValue(csharpField, out var rubyField))
                {
                    return rubyField;
                }
                throw new KeyNotFoundException($"Field '{csharpField}' not found in C# to Ruby field mapping for class '{className}'.");
            }
            throw new KeyNotFoundException($"Class '{className}' not found in C# to Ruby field mapping.");
        }

        public static string RubyClassToCSharp(string className)
        {
            return RubyToCSharpClass.TryGetValue(className, out var csharpClass)
                ? csharpClass
                : throw new KeyNotFoundException($"Class '{className}' not found in Ruby to C# class mapping.");
        }

        public static string CSharpClassToRuby(string className)
        {
            return CSharpToRubyClass.TryGetValue(className, out var rubyClass)
                ? rubyClass
                : throw new KeyNotFoundException($"Class '{className}' not found in C# to Ruby class mapping.");
        }
    }
}
