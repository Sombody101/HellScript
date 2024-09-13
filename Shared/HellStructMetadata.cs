using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace HellScriptShared.Bytecode;

public class HellStructMetadata
{
    public required string Name { get; init; }

    public required int MemberCount { get; init; }

    public int[] TypeRequirement { get; init; }
    public string[] MemberNames { get; init; }

    public void SerializeToStream(BinaryWriter bw)
    {
        // Name
        bw.Write(Name);

        // Member count
        bw.Write(MemberCount);

        // Member names (count/data)
        bw.Write(MemberNames.Length);

        foreach (var str in MemberNames)
            bw.Write(str);
    }

    public static HellStructMetadata[] ReadAllFromStream(BinaryReader stream)
    {
        int length = stream.ReadInt32();
        HellStructMetadata[] buffer = new HellStructMetadata[length];

        for (int i = 0; i < length; ++i)
        {
            string name = stream.ReadString();

            int memberCount = stream.ReadInt32();

            int memberNamesCount = stream.ReadInt32();
            string[] memberNames = new string[memberNamesCount];

            for (int x = 0; x < memberNamesCount; ++x)
            {
                memberNames[x] = stream.ReadString();
            }

            buffer[i] = new()
            {
                Name = name,
                MemberCount = memberCount,
                MemberNames = memberNames
            };
        }

        return buffer;
    }

    public override string ToString()
    {
        string names = MemberNames.Length is 0
            ? "<empty>"
            : string.Join(", ", MemberNames);

        return $"[ {Name}, [ {MemberCount}, {names} ]]";
    }
}