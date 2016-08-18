using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace color_map
{
  class Program
  {
    static void Main( string[] args )
    {
      var map = new Dictionary<uint,uint>();

      var data = File.ReadAllBytes("hashed-color-map.dat");
      for ( int i = 0 ; i < data.Length ; i += 7 )
      {
        uint hash = BitConverter.ToUInt32(data,i);
        uint rgb   = (uint)( data[i+4] << 16 | data[i+5] << 8 | data[i+6] << 0 );
        map.Add(hash,rgb);
      }

      foreach (var cn in args )
      {
        uint rgb;
        var hit = map.TryGetValue(Encoding.ASCII.GetBytes(cn).Aggregate(0x811c9dc5u, (h,b)=> h=(h^b)*0x01000193u ), out rgb);
        var c = hit ? String.Format("#{0:X6}",rgb) : "NOT FOUND";
        Console.WriteLine("{0}: {1}", c , cn);
      }

    }
  }
}
