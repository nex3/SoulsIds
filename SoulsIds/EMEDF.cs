using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SoulsIds
{
    public class EMEDF
    {
        public ClassDoc this[int classIndex] => Classes.Find(c => c.Index == classIndex);

        [JsonProperty(PropertyName = "unknown")]
        public long UNK;

        [JsonProperty(PropertyName = "main_classes")]
        public List<ClassDoc> Classes;

        [JsonProperty(PropertyName = "enums")]
        public EnumDoc[] Enums;

        public static EMEDF ReadText(string input)
        {
            return JsonConvert.DeserializeObject<EMEDF>(input);
        }

        public static EMEDF ReadFile(string path)
        {
            string input = File.ReadAllText(path);
            return ReadText(input);
        }

        public class ClassDoc
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "index")]
            public long Index { get; set; }

            [JsonProperty(PropertyName = "instrs")]
            public List<InstrDoc> Instructions { get; set; }

            public InstrDoc this[int instructionIndex] => Instructions.Find(ins => ins.Index == instructionIndex);
        }

        public class InstrDoc
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "index")]
            public long Index { get; set; }

            [JsonProperty(PropertyName = "args")]
            public ArgDoc[] Arguments { get; set; }

            public ArgDoc this[uint i] => Arguments[i];
        }

        public class ArgDoc
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "type")]
            public long Type { get; set; }

            [JsonProperty(PropertyName = "enum_name")]
            public string EnumName { get; set; }

            [JsonProperty(PropertyName = "default")]
            public long Default { get; set; }

            [JsonProperty(PropertyName = "min")]
            public long Min { get; set; }

            [JsonProperty(PropertyName = "max")]
            public long Max { get; set; }

            [JsonProperty(PropertyName = "increment")]
            public long Increment { get; set; }

            [JsonProperty(PropertyName = "format_string")]
            public string FormatString { get; set; }

            [JsonProperty(PropertyName = "unk1")]
            private long UNK1 { get; set; }

            [JsonProperty(PropertyName = "unk2")]
            private long UNK2 { get; set; }

            [JsonProperty(PropertyName = "unk3")]
            private long UNK3 { get; set; }

            [JsonProperty(PropertyName = "unk4")]
            private long UNK4 { get; set; }

            // Calculated values

            public int Offset { get; set; }

            public EnumDoc EnumDoc { get; set; }

            public object GetDisplayValue(object val) => EnumDoc == null ? val : EnumDoc.GetDisplayValue(val);
        }

        public class EnumDoc
        {
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "values")]
            public Dictionary<string, string> Values { get; set; }

            // Calculated values

            public string DisplayName { get; set; }

            public Dictionary<string, string> DisplayValues { get; set; }

            public object GetDisplayValue(object val) => DisplayValues.TryGetValue(val.ToString(), out string reval) ? reval : val;
        }
    }
}
