﻿using RenderWareNET.Enums;
using RenderWareNET.Plugins.Base;
using RenderWareNET.Plugins.Structs;

namespace RenderWareNET.Plugins
{
    public sealed class MaterialList : RWPlugin
    {
        public readonly RWMaterialList List = new();
        public readonly List<Material> Materials = new();

        protected override void ReadData(Stream stream)
        {
            long sectionEnd = stream.Position + Header.SectionSize;
            List.Read(stream);
            Materials.Clear();
            Materials.Capacity = List.Count;

            while (stream.Position < sectionEnd)
            {
                Materials.Add(new Material(stream));
            }
        }

        protected override void WriteData(Stream stream)
        {
            List.Write(stream);
            foreach (Material material in Materials)
            {
                material.Write(stream);
            }
        }

        protected override PluginID GetExpectedIdentifier()
            => PluginID.MaterialList;
    }
}
