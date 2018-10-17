// ===============================
// ©Copyright VISIONARY SAS, 2018
// ===============================
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;

// NOTE
// For use BinaryFormatter is required a DLL, like this case.
// For integrity

namespace MML.Shared
{
    [MessagePackObject]
    public class OnlineRecord
    {
        [Key(0)]
        public string ID { get; set; }
        [Key(1)]
        public DateTime Time { get; set; }
        [Key(2)]
        public float Depth { get; set; }
        [Key(3)]
        public List<Parameter> Parameters { get; set; }

        public override string ToString()
        {
            return $"{Time.ToString("HH:mm:ss")}: {ID} {Depth.ToString("0.00")} | {Count()}";
        }

        public int Count()
        {
            return Parameters != null ? Parameters.Count : 0;
        }

        public float ValueOf(string mnemonic)
        {
            var y = Parameters?.FirstOrDefault(x => x.Mnemonic == mnemonic)?.Value;
            return y == null ? -999.25F : y.Value;
        }
    }

    [MessagePackObject]
    public class Parameter
    {
        [Key(0)]
        public string Mnemonic { get; set; }
        [Key(1)]
        public float Value { get; set; }

        public override string ToString() => $"{Mnemonic}={Value};";
    }
}
