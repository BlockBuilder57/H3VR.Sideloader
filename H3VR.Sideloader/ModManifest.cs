﻿using System;
using System.Collections.Generic;

namespace H3VR.Sideloader
{
    internal enum AssetType
    {
        Texture,
        Prefab,
        Mesh,
        Material
    }

    internal class AssetMapping
    {
        public string GamePath { get; set; }
        public string ModPath { get; set; }
        public AssetType Type { get; set; }
    }

    internal class ModManifest
    {
        private const string MANIFEST_REVISION = "1";
        public AssetMapping[] AssetMappings { get; set; }
        public string Guid { get; set; }
        public string ManifestRevision { get; set; }
        public string Name { get; set; }
        public Version Version { get; set; }

        public bool Verify(out string[] errors)
        {
            var errs = new List<string>();

            void Missing(string field)
            {
                errs.Add($"Missing required property `{field}`.");
            }

            if (ManifestRevision == null)
                Missing(nameof(ManifestRevision));
            if (ManifestRevision != MANIFEST_REVISION)
                errs.Add($"Invalid manifest revision. Supported values are: `{MANIFEST_REVISION}`.");
            if (Guid == null)
                Missing(nameof(Guid));
            if (Name == null)
                Missing(nameof(Name));
            if (Version == null)
                errs.Add("Missing or invalid `version`. Version must be of form `X.X.X.X`.");

            errors = errs.ToArray();
            return errors.Length != 0;
        }
    }
}