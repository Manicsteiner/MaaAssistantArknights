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
using MaaWpfGui.Configuration.Factory;
using MaaWpfGui.Helper;
using Newtonsoft.Json.Linq;
using Serilog;

namespace MaaWpfGui.Models.EmulatorConnectionExtra;

public class AVDExtra() : ExtraConfig
{
    private static readonly ILogger _logger = Log.ForContext<AVDExtra>();

    public bool Enable
    {
        get; set {
            if (!SetAndNotify(ref field, value))
            {
                return;
            }

            ConfigFactory.CurrentConfig.Gui.ConnectSettings.Extras.AVDExtra.IsEnabled = value;

            // if (value)
            // {
            //    //DO NOTHING
            //    //AutoDetectEmulatorPath();
            // }
            Instances.AsstProxy.Connected = false;
        }
    } = ConfigFactory.CurrentConfig.Gui.ConnectSettings.Extras.AVDExtra.IsEnabled;

    public string Config
    {
        get {
            if (!Enable)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new JObject());
            }

            var configObject = new JObject {
                ["mode"] = "shm",
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(configObject);
        }
    }
}
