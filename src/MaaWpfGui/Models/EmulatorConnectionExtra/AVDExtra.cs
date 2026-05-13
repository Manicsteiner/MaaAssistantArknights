// <copyright file="AVDExtra.cs" company="MaaAssistantArknights">
// Part of the MaaWpfGui project, maintained by the MaaAssistantArknights team (Maa Team)
// Copyright (C) 2021-2025 MaaAssistantArknights Contributors
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License v3.0 only as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY
// </copyright>

#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json.Serialization;
using MaaWpfGui.Helper;
using MaaWpfGui.ViewModels.UI;
using MaaWpfGui.ViewModels.UserControl.Settings;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using Serilog;

namespace MaaWpfGui.Models.EmulatorConnectionExtra;

public class AVDExtra() : ExtraConfig, IJsonOnDeserialized
{
    private static readonly ILogger _logger = Log.ForContext<AVDExtra>();

    public AVDExtra(bool isEnabled)
        : this()
    {
        _isEnabled = isEnabled;
    }

    [JsonInclude]
    [JsonPropertyName("IsEnabled")]
    private bool _isEnabled;

    [JsonIgnore]
    public bool Enable
    {
        get => _isEnabled;
        set {
            if (!SetAndNotify(ref _isEnabled, value))
            {
                return;
            }

            if (value)
            {
                //DO NOTHING
                //AutoDetectEmulatorPath();
            }

            Instances.AsstProxy.Connected = false;
        }
    }

    [JsonIgnore]
    public string Config
    {
        get {
            if (!Enable)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new JObject());
            }

            int index;
            if (ManualSetIndex)
            {
                index = InstanceIndex;
            }
            else
            {
                index = GetEmulatorIndex(SettingsViewModel.ConnectSettings.ConnectAddress);
            }

            var configObject = new JObject {
                ["path"] = EmulatorPath,
                ["index"] = index,
                ["pid"] = GetEmulatorPid(index),
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(configObject);
        }
    }
}